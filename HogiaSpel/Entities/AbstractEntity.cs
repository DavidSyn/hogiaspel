using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HogiaSpel.Entities
{
    public abstract class AbstractEntity
    {
        public Guid Id { get; protected set; }
        public Texture2D Sprite { get; protected set; }
        public string SpriteName { get; protected set; }
        public Vector2 Position { get; set; }
        public float Speed { get; protected set; }

        public int Width { get { return Sprite.Width; } }
        public int Height { get { return Sprite.Height; } }

        protected void MoveUp(GameTime gameTime)
        {
            float x = Position.X;
            float y = Position.Y;
            y = Position.Y - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Position = new Vector2(x, y);
        }

        protected void MoveDown(GameTime gameTime)
        {
            float x = Position.X;
            float y = Position.Y;
            y = Position.Y + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Position = new Vector2(x, y);
        }

        protected void MoveRight(GameTime gameTime)
        {
            float x = Position.X;
            float y = Position.Y;
            x = Position.X + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Position = new Vector2(x, y);
        }

        protected void MoveLeft(GameTime gameTime)
        {
            float x = Position.X;
            float y = Position.Y;
            x = Position.X - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Position = new Vector2(x, y);
        }
    }
}
