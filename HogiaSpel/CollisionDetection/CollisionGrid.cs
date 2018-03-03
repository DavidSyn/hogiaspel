using HogiaSpel.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace HogiaSpel.CollisionDetection
{
    public sealed class CollisionGrid
    {
        public static CollisionGrid Instance { get { return _lazy.Value; } }
        private static readonly Lazy<CollisionGrid> _lazy = new Lazy<CollisionGrid>(() => new CollisionGrid());

        public List<List<CollisionCell>> Grid { get; private set; }
        public int MaxRows { get; private set; }
        public int MaxColumns { get; private set; }

        private int _cellHeight;
        private int _cellWidth;

        public CollisionGrid()
        {
            Grid = new List<List<CollisionCell>>();
        }

        public void Initialize(int windowHeight, int windowWidth, int cellHeight, int cellWidth)
        {
            MaxRows = windowHeight / cellHeight;
            MaxColumns = windowWidth / cellWidth;
            _cellHeight = cellHeight;
            _cellWidth = cellWidth;

            for (int i = 0; i < MaxColumns; i++)
            {
                Grid.Add(new List<CollisionCell>());
                for (int j = 0; j < MaxRows; j++)
                {
                    float x = i * _cellWidth;
                    float y = j * _cellHeight;
                    var position = new Vector2(x, y);
                    var cell = new CollisionCell(position, _cellHeight, _cellWidth);
                    Grid[i].Add(cell);
                }
            }
        }

        public List<Tuple<int, int>> UpdateCellPosition(IEntity entity)
        {
            var result = new List<Tuple<int, int>>();
            for (int i = 0; i < MaxColumns; i++)
            {
                float cellWidthBegin = i * _cellWidth;
                float cellWidthEnd = (i * _cellWidth) + _cellWidth;
                for (int j = 0; j < MaxRows; j++)
                {
                    float cellHeightTop = j * _cellHeight;
                    float cellHeightBottom = (j * _cellHeight) + _cellHeight;
                    if (Grid[i][j].Rectangle.Intersects(entity.Rectangle))
                    {
                        Grid[i][j].Entities.Add(entity);
                        result.Add(Tuple.Create(i, j));
                    }
                }
            }
            return result;
        }

        public void Clear()
        {
            for (int i = 0; i < MaxColumns; i++)
            {
                for (int j = 0; j < MaxRows; j++)
                {
                    Grid[i][j].Entities.Clear();
                }
            }
        }

        public List<IEntity> GetEntitiesWithinCell(Tuple<int, int> cellPosition)
        {
            return Grid[cellPosition.Item1][cellPosition.Item2].Entities;
        }
    }
}
