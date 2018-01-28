using System;

namespace AptitudeEngine
{
    public struct Rectangle
    {
        public static readonly Rectangle Empty = new Rectangle();

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 Position => new Vector2(X, Y);
        public Vector2 Size => new Vector2(Width, Height);

        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(Vector2 position, Vector2 size)
            : this(position.X, position.Y, size.X, size.Y) { }

        public bool IntersectsWith(Rectangle rect)
            => rect.X < X + Width &&
               X < rect.X + rect.Width &&
               rect.Y < Y + Height &&
               Y < rect.Y + rect.Height;

        public Rectangle Intersect(Rectangle rect)
        {
            var x1 = Math.Max(X, rect.X);
            var x2 = Math.Min(X + Width, rect.X + rect.Width);
            var y1 = Math.Max(Y, rect.Y);
            var y2 = Math.Min(Y + Height, rect.Y + rect.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }
    }
}