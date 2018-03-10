using Microsoft.Xna.Framework;
using System;

namespace HogiaSpel.Extensions
{
    static class RectangleExtensions
    {
        public static Vector2 GetIntersectionDirection(this Rectangle rectA, Rectangle rectB)
        {
            float halfWidthA = rectA.Width / 2.0f;
            float halfHeightA = rectA.Height / 2.0f;
            float halfWidthB = rectB.Width / 2.0f;
            float halfHeightB = rectB.Height / 2.0f;
            
            Vector2 centerA = new Vector2(rectA.Left + halfWidthA, rectA.Top + halfHeightA);
            Vector2 centerB = new Vector2(rectB.Left + halfWidthB, rectB.Top + halfHeightB);
            
            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
            {
                return Vector2.Zero;
            }

            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);
        }

        public static bool CollisionDown(this Rectangle rectA, Rectangle rectB)
        {
            Rectangle temp = rectA;
            temp.Location = new Point(rectA.Location.X, (rectA.Location.Y + 1));
            if (temp.Intersects(rectB))
            {
                return true;
            }
            return false;
        }

        public static bool CollisionUp(this Rectangle rectA, Rectangle rectB)
        {
            Rectangle temp = rectA;
            temp.Location = new Point(rectA.Location.X, (rectA.Location.Y - 1));
            if (rectA.Intersects(rectB))
            {
                return true;
            }
            return false;
        }

        public static bool CollisionRight(this Rectangle rectA, Rectangle rectB)
        {
            Rectangle temp = rectA;
            temp.Location = new Point((rectA.Location.X + 1), rectA.Location.Y);
            if (rectA.Intersects(rectB))
            {
                return true;
            }
            return false;
        }

        public static bool CollisionLeft(this Rectangle rectA, Rectangle rectB)
        {
            Rectangle temp = rectA;
            temp.Location = new Point((rectA.Location.X - 1), rectA.Location.Y);
            if (rectA.Intersects(rectB))
            {
                return true;
            }
            return false;
        }
    }
}
