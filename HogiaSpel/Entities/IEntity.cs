using HogiaSpel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HogiaSpel.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        float Speed { get; }
        float BaseSpeed { get; }
        float TopSpeed { get; }
        float Acceleration { get; }
        DirectionEnum CurrentAccelerationDirection { get; }
        bool Active { get; }
        int Width { get; }
        int Height { get; }
        Rectangle Rectangle { get; }

        SpriteHandler SpriteHandler { get; }
        List<Tuple<int, int>> CollisionCellPositions { get; }

        void Initialize(Vector2 position);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
