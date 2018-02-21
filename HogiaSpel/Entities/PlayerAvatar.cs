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
            SpriteName = "PlayerAvatar";
            SpriteHandler = new SpriteHandler(sprites.GetSprite(SpriteName), position);
            SpriteHandler.InitializeAnimation("run-right", 64, 64, 4, 80, Color.White, 1f, true);
            SpriteHandler.InitializeAnimation("stand-right", 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize("stand-right");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _inputHandler.HandleInputs();
            if (!_inputHandler.NewEvents.Any(x => x is MoveEvent))
            {
                if (_inputHandler.OldEvents.Any(x => x is MoveEvent))
                {
                    SpriteHandler.ChangeState("stand-right");
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
            
            SpriteHandler.Update(gameTime);
        }

        private void Move(GameTime gameTime, MoveEvent moveEvent)
        {
            switch (moveEvent.Direction)
            {
                case DirectionEnum.Up:
                    MoveUp(gameTime);
                    break;
                case DirectionEnum.Down:
                    MoveDown(gameTime);
                    break;
                case DirectionEnum.Right:
                    MoveRight(gameTime);
                    
                    if (_inputHandler.OldEvents.Any(x => x is MoveEvent))
                    {
                        SpriteHandler.ChangeState("run-right");
                    }
                    
                    break;
                case DirectionEnum.Left:
                    MoveLeft(gameTime);
                    break;
                default:
                    throw new Exception("Error: Invalid direction on event");
            }
        }
    }
}
