using HogiaSpel.CollisionDetection;
using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogiaSpel.Entities
{
    public class PowerUp : AbstractEntity, IEntity
    {
        public override void Initialize(Vector2 position)
        {
            Id = Guid.NewGuid();
            Active = true;
            SpeedX = 0;
            BaseSpeedX = 0;
            TopSpeedX = 0;
            Acceleration = 0;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.PowerUp.Stand, sprites.GetSprite(SpriteKeys.PowerUp.Stand), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.PowerUp.Stand);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);

            SpriteHandler.Update(gameTime);
        }

        public override void CheckCollision(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            foreach (var entity in grid.GetEntitiesWithinCell(CollisionCellPositions))
            {
                if (Id != entity.Id)
                {
                    var e = entity;
                }
            }
        }
    }
}
