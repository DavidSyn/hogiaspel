using HogiaSpel.Enums;
using HogiaSpel.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            SpriteHandler.Initialize("run-right");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var inputs = _inputHandler.HandleInputs();
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] is MoveEvent)
                {
                    Move(gameTime, (MoveEvent)inputs[i]);
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
