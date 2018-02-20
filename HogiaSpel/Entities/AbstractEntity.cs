using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HogiaSpel.Entities
{
    public abstract class AbstractEntity
    {
        public Guid Id { get; protected set; }
        public SpriteHandler SpriteHandler { get; protected set; }
        public string SpriteName { get; protected set; }
        public float Speed { get; protected set; }
        public bool Active { get; set; }

        public int Width { get { return SpriteHandler.CurrentFrameWidth; } }
        public int Height { get { return SpriteHandler.CurrentFrameWidth; } }

        protected void MoveUp(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        protected void MoveDown(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        protected void MoveRight(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        protected void MoveLeft(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }
    }
}
