using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HogiaSpel.Enums;
using HogiaSpel.CollisionDetection;

namespace HogiaSpel.Entities
{
    public class MetalBlock : AbstractEntity, IEntity
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
            SpriteHandler.InitializeAnimation(SpriteKeys.MetalBlock.Stand, sprites.GetSprite(SpriteKeys.MetalBlock.Stand), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.MetalBlock.Stand);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);

            SpriteHandler.Update(gameTime);
        }

        public Vector2 GetPosition()
        {
            return SpriteHandler.Position;
        }
    }
}
