using HogiaSpel.CollisionDetection;
using HogiaSpel.Entities.Blocks;
using HogiaSpel.Enums;
using HogiaSpel.Events;
using HogiaSpel.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace HogiaSpel.Entities
{
    public class PlayerAvatar : AbstractEntity
    {
        private static float JUMPFORCE_DEFAULT = 600f;
        private InputHandler _inputHandler;
        private DirectionEnum _pushDirection;
        
        

        public override void Initialize(Vector2 position)
        {
            _inputHandler = new InputHandler();
            Id = Guid.NewGuid();
            Active = true;
            SpeedX = 0;
            SpeedY = 0;
            JumpForce = 0;
            BaseSpeedX = 140;
            TopSpeedX = 500;
            Acceleration = 1.015f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;
            InAir = false;
            AirTime = 0f;
            _pushDirection = DirectionEnum.NoDirection;
            

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunRight, sprites.GetSprite(SpriteKeys.Quote.RunRight), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunLeft, sprites.GetSprite(SpriteKeys.Quote.RunLeft), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.PushRight, sprites.GetSprite(SpriteKeys.Quote.PushRight), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.PushLeft, sprites.GetSprite(SpriteKeys.Quote.PushLeft), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandRight, sprites.GetSprite(SpriteKeys.Quote.StandRight), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandLeft, sprites.GetSprite(SpriteKeys.Quote.StandLeft), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.Quote.StandRight);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            _inputHandler.HandleInputs();
            HandleMovement(gameTime);

            if (InAir)
            {
                CalculateSpeedY(JumpForce, gameTime);
                MoveUp(SpeedY, gameTime);
            }

            HandleSpriteState();

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
            SpriteHandler.Update(gameTime);
        }

        public override void CheckCollision(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            var collisionOnGround = false;
            var entitiesWithinCell = grid.GetEntitiesWithinCell(CollisionCellPositions);

            var blocks = entitiesWithinCell.Where(x => x is IBlock);
            if (blocks.Any())
            {
                foreach (var entity in blocks)
                {
                    if (Id != entity.Id)
                    {
                        if (entity is IBlock)
                        {
                            var collision = HandleBlockCollision(entity, gameTime);
                            if (collision)
                            {
                                collisionOnGround = true;
                            }
                        }
                    }
                }
            }
            else
            {
                collisionOnGround = false;
            }

            if (!collisionOnGround)
            {
                InAir = true;
            }
        }

        private bool HandleBlockCollision(IEntity entity, GameTime gameTime)
        {
            if (Rectangle.Intersects(entity.Rectangle))
            {
                if (entity is Block)
                {
                    MovableBlockCollision(entity, gameTime);
                }
                else
                {
                    BlockCollision(entity, gameTime);
                }
                return true;
            }
            return false;
        }

        private void BlockCollision(IEntity entity, GameTime gameTime)
        {
            if (Rectangle.CollisionRight(entity.Rectangle) || Rectangle.CollisionLeft(entity.Rectangle))
            {
                if (CurrentAccelerationDirection == DirectionEnum.Left)
                {
                    MoveRight(SpeedX, gameTime);
                    SpeedX = BaseSpeedX;
                }
                else if (CurrentAccelerationDirection == DirectionEnum.Right)
                {
                    MoveLeft(SpeedX, gameTime);
                    SpeedX = BaseSpeedX;
                }
            }

            if (Rectangle.CollisionDown(entity.Rectangle))
            {
                StayOnGround(entity, gameTime);

                if (!Rectangle.CollisionLeft(entity.Rectangle) || !Rectangle.CollisionRight(entity.Rectangle))
                {

                }
            }
        }

        private void MovableBlockCollision(IEntity entity, GameTime gameTime)
        {
            if (Rectangle.CollisionDown(entity.Rectangle))
            {
                StayOnGround(entity, gameTime);
            }

            if (Rectangle.CollisionLeft(entity.Rectangle) || Rectangle.CollisionRight(entity.Rectangle))
            {
                var collisionDepth = Rectangle.GetIntersectionDirection(entity.Rectangle);
                if (collisionDepth.Y == -63)
                {
                    SpeedX = BaseSpeedX / 2;

                    if (CurrentAccelerationDirection == DirectionEnum.Left)
                    {
                        _pushDirection = DirectionEnum.Left;
                    }
                    else if (CurrentAccelerationDirection == DirectionEnum.Right)
                    {
                        _pushDirection = DirectionEnum.Right;
                    }
                    else
                    {
                        _pushDirection = DirectionEnum.NoDirection;
                    }
                }
                
            }
        }

        private void StayOnGround(IEntity entity, GameTime gameTime)
        {
            SetPosition(SpriteHandler.Position.X, (entity.Rectangle.Top - Rectangle.Height));

            SpeedY = 0f;
            JumpForce = 0f;
            InAir = false;
        }

        private void HandleSpriteState()
        {
            if (CurrentAccelerationDirection == DirectionEnum.NoDirection)
            {
                for (int i = 0; i < _inputHandler.OldEvents.Count; i++)
                {
                    if (_inputHandler.OldEvents[i] is MoveEvent)
                    {
                        var moveEvent = (MoveEvent)_inputHandler.OldEvents[i];
                        if (moveEvent.Direction == DirectionEnum.Right)
                        {
                            SpriteHandler.ChangeState(SpriteKeys.Quote.StandRight);
                            break;
                        }
                        else if (moveEvent.Direction == DirectionEnum.Left)
                        {
                            SpriteHandler.ChangeState(SpriteKeys.Quote.StandLeft);
                            break;
                        }
                    }
                }
                
            }
            else if (CurrentAccelerationDirection == DirectionEnum.Right)
            {
                if (_pushDirection == DirectionEnum.Right)
                {
                    SpriteHandler.ChangeState(SpriteKeys.Quote.PushRight);
                    _pushDirection = DirectionEnum.NoDirection;
                }
                else
                {
                    SpriteHandler.ChangeState(SpriteKeys.Quote.RunRight);
                }
            }
            else if (CurrentAccelerationDirection == DirectionEnum.Left)
            {
                if (_pushDirection == DirectionEnum.Left)
                {
                    SpriteHandler.ChangeState(SpriteKeys.Quote.PushLeft);
                    _pushDirection = DirectionEnum.NoDirection;
                }
                else
                {
                    SpriteHandler.ChangeState(SpriteKeys.Quote.RunLeft);
                }
            }
        }

        private void HandleMovement(GameTime gameTime)
        {
            if (!_inputHandler.NewEvents.Any(x => x is MoveEvent))
            {
                if (_inputHandler.OldEvents.Any(x => x is MoveEvent))
                {
                    CurrentAccelerationDirection = DirectionEnum.NoDirection;
                    CalculateSpeedX();
                }
            }
            else
            {
                var moveEvent = (MoveEvent)_inputHandler.NewEvents.Where(x => x is MoveEvent).FirstOrDefault();
                CurrentAccelerationDirection = moveEvent.Direction;
                CalculateSpeedX();
            }
            if (_inputHandler.NewEvents.Any(x => x is JumpEvent))
            {
                if (!InAir)
                {
                    Jump();
                }
                else
                {
                    if (!_inputHandler.NewEvents.Any(x => x is MoveEvent))
                    {
                        CurrentAccelerationDirection = DirectionEnum.NoDirection;
                    }
                }
            }
            else
            {
                if (!_inputHandler.NewEvents.Any(x => x is MoveEvent))
                {
                    CurrentAccelerationDirection = DirectionEnum.NoDirection;
                }
            }
            Move(gameTime);
        }

        private void CalculateSpeedX()
        {
            if (CurrentAccelerationDirection == DirectionEnum.NoDirection)
            {
                SpeedX = 0;
            }
            else if (CurrentAccelerationDirection == DirectionEnum.Left || CurrentAccelerationDirection == DirectionEnum.Right)
            {
                if (!_inputHandler.OldEvents.Any(x => x is MoveEvent))
                {
                    SpeedX = BaseSpeedX;
                }
                else
                {
                    SpeedX = SpeedX * Acceleration;
                }
            }

            if (SpeedX > TopSpeedX)
            {
                SpeedX = TopSpeedX;
            }
        }

        private void CalculateSpeedY(float upwardsForce, GameTime gameTime)
        {
            //AirTime = AirTime + (float)gameTime.ElapsedGameTime.TotalSeconds;
            //var jumpGravity = AirTime * Gravity;
            //SpeedY = upwardsForce - jumpGravity;

            SpeedY = SpeedY - Gravity;
        }

        private void Jump()
        {
            InAir = true;
            SpeedY = JUMPFORCE_DEFAULT;
        }

        private void Move(GameTime gameTime)
        {
            switch (CurrentAccelerationDirection)
            {
                case DirectionEnum.Right:
                    MoveRight(SpeedX, gameTime);
                    break;
                case DirectionEnum.Left:
                    MoveLeft(SpeedX, gameTime);
                    break;
                default:
                    break;
            }
        }
    }
}
