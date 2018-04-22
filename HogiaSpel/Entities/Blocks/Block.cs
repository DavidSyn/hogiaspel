using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HogiaSpel.Enums;
using HogiaSpel.CollisionDetection;
using HogiaSpel.Extensions;
using System.Linq;

namespace HogiaSpel.Entities.Blocks
{
    public class Block : AbstractEntity, IBlock
    {
        public override void Initialize(Vector2 position)
        {
            Id = Guid.NewGuid();
            Active = true;
            SpeedX = 0;
            SpeedY = 0;
            JumpForce = 0;
            BaseSpeedX = 140;
            TopSpeedX = 500;
            Acceleration = 1.015f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;
            InAir = false;
            AirTime = 0f;

            var sprites = Sprites.Instance;
            SpriteHandler = new SpriteHandler(position);
            SpriteHandler.InitializeAnimation(SpriteKeys.Block.Stand, sprites.GetSprite(SpriteKeys.Block.Stand), 64, 64, 1, 80, Color.White, 1f, true);
            SpriteHandler.Initialize(SpriteKeys.Block.Stand);

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteHandler.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (InAir)
            {
                CalculateSpeedY(JumpForce, gameTime);
                MoveUp(SpeedY, gameTime);
            }

            var grid = CollisionGrid.Instance;
            CollisionCellPositions = grid.UpdateCellPosition(this);
            SpriteHandler.Update(gameTime);
        }

        private void CalculateSpeedY(float upwardsForce, GameTime gameTime)
        {
            AirTime = AirTime + (float)gameTime.ElapsedGameTime.TotalSeconds;
            var jumpGravity = AirTime * Gravity;
            SpeedY = upwardsForce - jumpGravity;
        }

        private void StayOnGround(GameTime gameTime)
        {
            if (SpeedY < 0)
            {
                float speed = SpeedY * -1f;
                MoveUp(speed, gameTime);
            }
            else
            {
                MoveUp(SpeedY, gameTime);
            }
            SpeedY = 0f;
            JumpForce = 0f;
            InAir = false;
            AirTime = 0f;
            CurrentAccelerationDirection = DirectionEnum.NoDirection;
        }

        public override void CheckCollision(GameTime gameTime)
        {
            var grid = CollisionGrid.Instance;
            var collisionOnGround = false;
            var entitiesWithinCell = grid.GetEntitiesWithinCell(CollisionCellPositions);

            var player = entitiesWithinCell.Where(x => x is PlayerAvatar);
            if (player.Any())
            {
                foreach (var entity in player)
                {
                    if (Id != entity.Id)
                    {
                        HandlePlayerAvatarCollision(gameTime, entity);
                    }
                }
            }

            var blocks = entitiesWithinCell.Where(x => x is IBlock);
            if (blocks.Any())
            {
                foreach (var entity in blocks)
                {
                    if (Id != entity.Id)
                    {
                        if (entity is IBlock)
                        {
                            HandleBlockCollision(gameTime, entity);
                        }
                    }
                }
            }
            else
            {
                collisionOnGround = false;
            }

            if (!collisionOnGround)
            {
                InAir = true;
            }
        }

        private bool HandleBlockCollision(GameTime gameTime, IEntity entity)
        {
            if (Rectangle.Intersects(entity.Rectangle))
            {
                if (Rectangle.CollisionDown(entity.Rectangle))
                {
                    StayOnGround(gameTime);

                    if (!Rectangle.CollisionLeft(entity.Rectangle) || !Rectangle.CollisionRight(entity.Rectangle))
                    {

                    }
                }
                return true;
            }
            return false;
        }

        private void HandlePlayerAvatarCollision(GameTime gameTime, IEntity entity)
        {
            if (Rectangle.Intersects(entity.Rectangle))
            {
                var collisionDepth = Rectangle.GetIntersectionDirection(entity.Rectangle);
                if (collisionDepth.Y == 63)
                {
                    MoveRight(collisionDepth.X);
                }
                
            }
        }
    }
}
