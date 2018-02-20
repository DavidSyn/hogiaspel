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
            Position = position;
            Speed = 140;

            var sprites = Sprites.Instance;
            SpriteName = "Sprite";
            Sprite = sprites.GetSprite(SpriteName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
