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
            Speed = 140;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunRight, sprites.GetSprite(SpriteKeys.Quote.RunRight), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.RunLeft, sprites.GetSprite(SpriteKeys.Quote.RunLeft), 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandRight, sprites.GetSprite(SpriteKeys.Quote.StandRight), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation(SpriteKeys.Quote.StandLeft, sprites.GetSprite(SpriteKeys.Quote.StandLeft), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.Quote.StandRight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _inputHandler.HandleInputs();
            HandleMovement(gameTime);

            SpriteHandler.Update(gameTime);
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
            else
            {
                for (int i = 0; i < _inputHandler.NewEvents.Count; i++)
                {
                    if (_inputHandler.NewEvents[i] is MoveEvent)
                    {
                        Move(gameTime, (MoveEvent)_inputHandler.NewEvents[i]);
                    }
                }
            }
        }

        private void Move(GameTime gameTime, MoveEvent moveEvent)
        {
            switch (moveEvent.Direction)
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
                    throw new Exception("Error: Invalid direction on event");
            }
        }
    }
}
