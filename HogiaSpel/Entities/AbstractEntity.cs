using HogiaSpel.CollisionDetection;
using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HogiaSpel.Entities
{
    public abstract class AbstractEntity : IAbstractEntity
    {
        public Guid Id { get; protected set; }
        public float SpeedX { get; protected set; }
        public float BaseSpeedX { get; protected set; }
        public float TopSpeedX { get; protected set; }
        public float Acceleration { get; protected set; }
        public float SpeedY { get; protected set; }
        public float Gravity { get { return 700.0f; } }
        public bool InAir { get; protected set; }
        public float AirTime { get; protected set; }
        public float JumpForce { get; protected set; }
        
        public DirectionEnum CurrentAccelerationDirection { get; protected set; }
        public bool Active { get; protected set; }
        public int Width { get { return SpriteHandler.CurrentFrameWidth; } }
        public int Height { get { return SpriteHandler.CurrentFrameHeight; } }
        public Rectangle Rectangle { get { return new Rectangle((int)SpriteHandler.Position.X, (int)SpriteHandler.Position.Y, Width, Height); } }
        public SpriteHandler SpriteHandler { get; protected set; }
        public List<Tuple<int, int>> CollisionCellPositions { get; protected set; }

        public void MoveUp(float speed, GameTime gameTime = null)
        {
            float deltatime = (gameTime != null) ? (float)gameTime.ElapsedGameTime.TotalSeconds : 1F;

            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y - (speed * deltatime);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveDown(float speed, GameTime gameTime = null)
        {
            float deltatime = (gameTime != null) ? (float)gameTime.ElapsedGameTime.TotalSeconds : 1F;

            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y + (speed * deltatime);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveRight(float speed, GameTime gameTime = null)
        {
            float deltatime = (gameTime != null) ? (float)gameTime.ElapsedGameTime.TotalSeconds : 1F;

            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X + (speed * deltatime);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveLeft(float speed, GameTime gameTime = null)
        {
            float deltatime = (gameTime != null) ? (float)gameTime.ElapsedGameTime.TotalSeconds : 1F;

            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X - (speed * deltatime);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public virtual void Initialize(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void CheckCollision(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
