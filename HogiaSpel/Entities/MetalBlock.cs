﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HogiaSpel.Enums;

namespace HogiaSpel.Entities
{
    public class MetalBlock : AbstractEntity, IEntity
    {
        public void Initialize(Vector2 position)
        {
            Id = Guid.NewGuid();
            Active = true;
            Speed = 140;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.MetalBlock.Stand, sprites.GetSprite(SpriteKeys.MetalBlock.Stand), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.MetalBlock.Stand);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            SpriteHandler.Update(gameTime);
        }
    }
}
