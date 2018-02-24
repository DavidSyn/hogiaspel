using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HogiaSpel.Enums;

namespace HogiaSpel.Entities
{
    public class DiamondBlock : AbstractEntity, IEntity
    {
        public void Initialize(Vector2 position)
        {
            Id = Guid.NewGuid();
            Active = true;
            Speed = 0;
            BaseSpeed = 0;
            TopSpeed = 0;
            Acceleration = 0;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.DiamondBlock.Stand, sprites.GetSprite(SpriteKeys.DiamondBlock.Stand), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.DiamondBlock.Stand);
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
