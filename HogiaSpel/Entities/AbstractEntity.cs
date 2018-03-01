﻿using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HogiaSpel.Entities
{
    public abstract class AbstractEntity : IAbstractEntity
    {
        public Guid Id { get; protected set; }
        public float Speed { get; protected set; }
        public float BaseSpeed { get; protected set; }
        public float TopSpeed { get; protected set; }
        public float Acceleration { get; protected set; }
        public DirectionEnum CurrentAccelerationDirection { get; protected set; }
        public bool Active { get; protected set; }
        public int Width { get { return SpriteHandler.CurrentFrameWidth; } }
        public int Height { get { return SpriteHandler.CurrentFrameHeight; } }
        public SpriteHandler SpriteHandler { get; protected set; }
        public List<Tuple<int, int>> CollisionCellPositions { get; protected set; }

        public void MoveUp(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveDown(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            y = SpriteHandler.Position.Y + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveRight(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X + (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            SpriteHandler.Position = new Vector2(x, y);
        }

        public void MoveLeft(GameTime gameTime)
        {
            float x = SpriteHandler.Position.X;
            float y = SpriteHandler.Position.Y;
            x = SpriteHandler.Position.X - (Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
