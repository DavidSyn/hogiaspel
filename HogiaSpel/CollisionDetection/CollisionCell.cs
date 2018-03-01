using HogiaSpel.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogiaSpel.CollisionDetection
{
    public class CollisionCell
    {
        public Guid Id { get; protected set; }
        public Vector2 Position { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public List<IEntity> Entities { get; set; }

        public CollisionCell(Vector2 position, int height, int width)
        {
            Id = Guid.NewGuid();
            Entities = new List<IEntity>();
            Position = position;
            Height = height;
            Width = width;
        }
    }
}
