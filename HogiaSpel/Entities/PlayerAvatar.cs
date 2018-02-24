using HogiaSpel.CollisionDetection;
using HogiaSpel.Enums;
using HogiaSpel.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HogiaSpel.Entities
{
    public class PlayerAvatar : AbstractEntity, IEntity
    {
        private InputHandler _inputHandler;

        public void Initialize(Vector2 position)
        {
            _inputHandler = new InputHandler();
            Id = Guid.NewGuid();
            Active = true;
            Speed = 0;
            BaseSpeed = 140;
            TopSpeed = 400;
            Acceleration = 1.015f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunRight, sprites.GetSprite(SpriteKeys.Quote.RunRight), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunLeft, sprites.GetSprite(SpriteKeys.Quote.RunLeft), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandRight, sprites.GetSprite(SpriteKeys.Quote.StandRight), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandLeft, sprites.GetSprite(SpriteKeys.Quote.StandLeft), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.Quote.StandRight);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _inputHandler.HandleInputs();
            HandleMovement(gameTime);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);

            SpriteHandler.Update(gameTime);
        }

        public Vector2 GetPosition()
        {
            return SpriteHandler.Position;
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
                    SpriteHandler.ChangeState(SpriteKeys.Quote.RunRight);
                    break;
                case DirectionEnum.Left:
                    MoveLeft(gameTime);
                    SpriteHandler.ChangeState(SpriteKeys.Quote.RunLeft);
                    break;
                default:
                    break;
            }
        }
    }
}
