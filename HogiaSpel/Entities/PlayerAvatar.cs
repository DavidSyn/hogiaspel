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
        private InputHandler _inputHandler;
        private DirectionEnum _pushDirection;
        private bool _inAir = false;
        private float _airTime = 0F;
        private static float JUMPFORCE_DEFAULT = 200F;

        public override void Initialize(Vector2 position)
        {
            _inputHandler = new InputHandler();
            Id = Guid.NewGuid();
            Active = true;
            Speed = 0;
            JumpForce = JUMPFORCE_DEFAULT;
            BaseSpeed = 140;
            TopSpeed = 500;
            Acceleration = 1.015f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;
            _pushDirection = DirectionEnum.NoDirection;
            _inAir = false;
            _airTime = 0F;

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

            MoveDown(Gravity, gameTime);

            HandleSpriteState();

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
            SpriteHandler.Update(gameTime);
        }

        public override void CheckCollision(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            var anySolidEntities = false;
            foreach (var entity in grid.GetEntitiesWithinCell(CollisionCellPositions))
            {
                if (Id != entity.Id)
                {
                    if (entity is IBlock)
                    {
                        if (HandleBlockCollision(entity, gameTime))
                        {
                            anySolidEntities = true;
                        }
                    }
                }
            }

            if (!anySolidEntities)
            {
                _inAir = true;
            }
        }

        private bool HandleBlockCollision(IEntity entity, GameTime gameTime)
        {
            if (Rectangle.Intersects(entity.Rectangle))
            {
                var collisionDepth = Rectangle.GetIntersectionDirection(entity.Rectangle);
                if (entity is Block)
                {
                    if (Rectangle.CollisionDown(entity.Rectangle))
                    {
                        if (!Rectangle.CollisionLeft(entity.Rectangle) || !Rectangle.CollisionRight(entity.Rectangle))
                        {
                            if (_inAir)
                            {
                                MoveUp((JumpForce + Gravity), gameTime);
                                JumpForce = JUMPFORCE_DEFAULT;
                                _inAir = false;
                                _airTime = 0F;
                            }
                            else
                            {
                                MoveUp(Gravity, gameTime);
                                JumpForce = JUMPFORCE_DEFAULT;
                                _inAir = false;
                                _airTime = 0F;
                            }
                        }
                    }

                    if (Rectangle.CollisionLeft(entity.Rectangle) || Rectangle.CollisionRight(entity.Rectangle))
                    {
                        Speed = BaseSpeed / 2;

                        if (CurrentAccelerationDirection == DirectionEnum.Left)
                        {
                            _pushDirection = DirectionEnum.Left;
                            if (!Rectangle.CollisionDown(entity.Rectangle))
                            {
                                MoveDown(Gravity, gameTime);
                            }
                        }
                        else if (CurrentAccelerationDirection == DirectionEnum.Right)
                        {
                            _pushDirection = DirectionEnum.Right;
                            if (!Rectangle.CollisionDown(entity.Rectangle))
                            {
                                MoveDown(Gravity, gameTime);
                            }
                        }
                        else
                        {
                            _pushDirection = DirectionEnum.NoDirection;
                            if (!Rectangle.CollisionDown(entity.Rectangle))
                            {
                                MoveDown(Gravity, gameTime);
                            }
                        }
                    }
                }
                else
                {
                    if (Rectangle.CollisionDown(entity.Rectangle))
                    {
                        if (_inAir)
                        {
                            MoveUp((JumpForce + Gravity), gameTime);
                            JumpForce = JUMPFORCE_DEFAULT;
                            _inAir = false;
                            _airTime = 0F;
                        }
                        else
                        {
                            MoveUp(Gravity, gameTime);
                            JumpForce = JUMPFORCE_DEFAULT;
                            _inAir = false;
                            _airTime = 0F;
                        }

                        if (!Rectangle.CollisionLeft(entity.Rectangle) || !Rectangle.CollisionRight(entity.Rectangle))
                        {
                            
                        }
                    }
                    if (Rectangle.CollisionRight(entity.Rectangle) || Rectangle.CollisionLeft(entity.Rectangle))
                    {
                        if (CurrentAccelerationDirection == DirectionEnum.Left)
                        {
                            MoveRight(Speed, gameTime);
                            Speed = BaseSpeed;
                            if (!Rectangle.CollisionDown(entity.Rectangle))
                            {
                                MoveDown(Gravity, gameTime);
                            }
                        }
                        else if (CurrentAccelerationDirection == DirectionEnum.Right)
                        {
                            MoveLeft(Speed, gameTime);
                            Speed = BaseSpeed;
                            if (!Rectangle.CollisionDown(entity.Rectangle))
                            {
                                MoveDown(Gravity, gameTime);
                            }
                        }
                    }
                }
                return true;
            }
            return false;
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
                    CalculateSpeed();
                }
            }
            else
            {
                var moveEvent = (MoveEvent)_inputHandler.NewEvents.Where(x => x is MoveEvent).FirstOrDefault();
                CurrentAccelerationDirection = moveEvent.Direction;
                CalculateSpeed();
            }
            if (_inputHandler.NewEvents.Any(x => x is JumpEvent))
            {
                if (!_inAir)
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

            if (_inAir)
            {
                Jump();
            }
            Move(gameTime);
        }

        private void CalculateSpeed()
        {
            if (CurrentAccelerationDirection == DirectionEnum.NoDirection)
            {
                Speed = 0;
            }
            else if (CurrentAccelerationDirection == DirectionEnum.Left || CurrentAccelerationDirection == DirectionEnum.Right)
            {
                if (!_inputHandler.OldEvents.Any(x => x is MoveEvent))
                {
                    Speed = BaseSpeed;
                }
                else
                {
                    Speed = Speed * Acceleration;
                }
            }

            if (Speed > TopSpeed)
            {
                Speed = TopSpeed;
            }
        }

        private void Jump()
        {
            if (CurrentAccelerationDirection == DirectionEnum.Right || CurrentAccelerationDirection == DirectionEnum.UpRight)
            {
                CurrentAccelerationDirection = DirectionEnum.UpRight;
            }
            else if (CurrentAccelerationDirection == DirectionEnum.Left || CurrentAccelerationDirection == DirectionEnum.UpLeft)
            {
                CurrentAccelerationDirection = DirectionEnum.UpLeft;
            }
            else
            {
                CurrentAccelerationDirection = DirectionEnum.Up;
            }
        }

        private void Move(GameTime gameTime)
        {
            float jumpGravity = 0F;
            switch (CurrentAccelerationDirection)
            {
                case DirectionEnum.Up:
                    _airTime = _airTime + (float)gameTime.ElapsedGameTime.TotalSeconds;
                    jumpGravity = _airTime * Gravity;
                    JumpForce = JumpForce - jumpGravity;
                    MoveUp(JumpForce, gameTime);
                    break;
                case DirectionEnum.UpRight:
                    _airTime = _airTime + (float)gameTime.ElapsedGameTime.TotalSeconds;
                    jumpGravity = _airTime * Gravity;
                    JumpForce = JumpForce - jumpGravity;
                    MoveUp(JumpForce, gameTime);
                    MoveRight(Speed, gameTime);
                    break;
                case DirectionEnum.UpLeft:
                    _airTime = _airTime + (float)gameTime.ElapsedGameTime.TotalSeconds;
                    jumpGravity = _airTime * Gravity;
                    JumpForce = JumpForce - jumpGravity;
                    MoveUp(JumpForce, gameTime);
                    MoveLeft(Speed, gameTime);
                    break;
                case DirectionEnum.Right:
                    MoveRight(Speed, gameTime);
                    break;
                case DirectionEnum.Left:
                    MoveLeft(Speed, gameTime);
                    break;
                default:
                    break;
            }
        }
    }
}
