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

        public override void Initialize(Vector2 position)
        {
            _inputHandler = new InputHandler();
            Id = Guid.NewGuid();
            Active = true;
            Speed = 0;
            BaseSpeed = 140;
            TopSpeed = 400;
            Acceleration = 1.015f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;
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

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
            SpriteHandler.Update(gameTime);
        }

        public override void CheckCollision(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            foreach (var entity in grid.GetEntitiesWithinCell(CollisionCellPositions))
            {
                if (Id != entity.Id)
                {
                    if (entity is IBlock)
                    {
                        HandleBlockCollision(entity);
                    }
                }
            }
        }

        private void HandleBlockCollision(IEntity entity)
        {
            if (Rectangle.Intersects(entity.Rectangle))
            {
                var collisionDepth = Rectangle.GetIntersectionDirection(entity.Rectangle);
                if (entity is Block)
                {
                    if ((collisionDepth.Y == entity.Rectangle.Height) || (collisionDepth.Y == (entity.Rectangle.Height * -1)))
                    {
                        Speed = BaseSpeed / 2;

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
                    else if ((collisionDepth.X == entity.Rectangle.Width) || (collisionDepth.X == (entity.Rectangle.Width * -1)))
                    {
                        float x = SpriteHandler.Position.X;
                        float y = SpriteHandler.Position.Y;
                        y = SpriteHandler.Position.Y + collisionDepth.Y;
                        SpriteHandler.Position = new Vector2(x, y);
                        Speed = BaseSpeed;
                    }
                    else
                    {
                        float x = SpriteHandler.Position.X;
                        float y = SpriteHandler.Position.Y;
                        y = SpriteHandler.Position.Y + collisionDepth.Y;
                        x = SpriteHandler.Position.X + collisionDepth.X;
                        SpriteHandler.Position = new Vector2(x, y);
                        Speed = BaseSpeed;
                    }
                }
                else
                {
                    if ((collisionDepth.Y == entity.Rectangle.Height) || (collisionDepth.Y == (entity.Rectangle.Height * -1)))
                    {
                        float x = SpriteHandler.Position.X;
                        float y = SpriteHandler.Position.Y;
                        x = SpriteHandler.Position.X + collisionDepth.X;
                        SpriteHandler.Position = new Vector2(x, y);
                        Speed = BaseSpeed;
                    }
                    else if ((collisionDepth.X == entity.Rectangle.Width) || (collisionDepth.X == (entity.Rectangle.Width * -1)))
                    {
                        float x = SpriteHandler.Position.X;
                        float y = SpriteHandler.Position.Y;
                        y = SpriteHandler.Position.Y + collisionDepth.Y;
                        SpriteHandler.Position = new Vector2(x, y);
                        Speed = BaseSpeed;
                    }
                    else
                    {
                        float x = SpriteHandler.Position.X;
                        float y = SpriteHandler.Position.Y;
                        y = SpriteHandler.Position.Y + collisionDepth.Y;
                        x = SpriteHandler.Position.X + collisionDepth.X;
                        SpriteHandler.Position = new Vector2(x, y);
                        Speed = BaseSpeed;
                    }
                }
            }
        }

        private void HandleMovement(GameTime gameTime)
        {
            if (!_inputHandler.NewEvents.Any(x => x is MoveEvent))
            {
                if (_inputHandler.OldEvents.Any(x => x is MoveEvent))
                {
                    for (int i = 0; i < _inputHandler.OldEvents.Count; i++)
                    {
                        var moveEvent = (MoveEvent)_inputHandler.OldEvents[i];
                        if (moveEvent.Direction == DirectionEnum.Right)
                        {
                            SpriteHandler.ChangeState(SpriteKeys.Quote.StandRight);
                            CurrentAccelerationDirection = DirectionEnum.NoDirection;
                            CalculateSpeed();
                            break;
                        }
                        else if (moveEvent.Direction == DirectionEnum.Left)
                        {
                            SpriteHandler.ChangeState(SpriteKeys.Quote.StandLeft);
                            CurrentAccelerationDirection = DirectionEnum.NoDirection;
                            CalculateSpeed();
                            break;
                        }
                    }

                }
            }
            else
            {
                var moveEvent = (MoveEvent)_inputHandler.NewEvents.Where(x => x is MoveEvent).FirstOrDefault();
                CurrentAccelerationDirection = moveEvent.Direction;
                CalculateSpeed();
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

        private void Move(GameTime gameTime)
        {
            switch (CurrentAccelerationDirection)
            {
                //case DirectionEnum.Up:
                //    MoveUp(gameTime);
                //    break;
                //case DirectionEnum.Down:
                //    MoveDown(gameTime);
                //    break;
                case DirectionEnum.Right:
                    MoveRight(gameTime);
                    if (_pushDirection == DirectionEnum.Right)
                    {
                        SpriteHandler.ChangeState(SpriteKeys.Quote.PushRight);
                        _pushDirection = DirectionEnum.NoDirection;
                    }
                    else
                    {
                        SpriteHandler.ChangeState(SpriteKeys.Quote.RunRight);
                    }
                    break;
                case DirectionEnum.Left:
                    MoveLeft(gameTime);
                    if (_pushDirection == DirectionEnum.Left)
                    {
                        SpriteHandler.ChangeState(SpriteKeys.Quote.PushLeft);
                        _pushDirection = DirectionEnum.NoDirection;
                    }
                    else
                    {
                        SpriteHandler.ChangeState(SpriteKeys.Quote.RunLeft);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
