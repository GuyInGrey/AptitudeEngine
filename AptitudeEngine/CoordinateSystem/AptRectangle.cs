using System;

namespace AptitudeEngine.CoordinateSystem
{
    public struct AptRectangle
    {
        /// <summary>
        /// An empty rectangle.
        /// </summary>
        public static readonly AptRectangle Empty = new AptRectangle();

        /// <summary>
        /// The X axis location.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y axis location.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The size on the X axis (width).
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The size on the Y axis (height).
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The position. If possible, use <see cref="X"/> and <see cref="Y"/>.
        /// </summary>
        public Vector2 Position => new Vector2(X, Y);

        /// <summary>
        /// The size. If possible, use <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        public Vector2 Size => new Vector2(Width, Height);

        /// <summary>
        /// Check whether vector v is within the bounds of the rectangle.
        /// </summary>
        /// <param name="v">The Vector to check.</param>
        /// <returns></returns>
        public bool ContainsVector(Vector2 v)
            => v.X > X && v.Y > Y && v.X < X + Width && v.Y < Y + Height;

        /// <summary>
        /// Create a new <see cref="AptRectangle"/>.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="width">The width size.</param>
        /// <param name="height">The height size.</param>
        public AptRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Create a new <see cref="AptRectangle"/>. If possible use <see cref="AptRectangle(float, float, float, float)"/> constructor instead.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public AptRectangle(Vector2 position, Vector2 size)
            : this(position.X, position.Y, size.X, size.Y) { }

        /// <summary>
        /// See if this rectangle's bounds intersect at all with <see cref="r"/>.
        /// </summary>
        /// <param name="r">The rectangle to check with.</param>
        /// <returns></returns>
        public bool IntersectsWith(AptRectangle r)
        {
            var l1 = r.Position;
            var l2 = Position;
            var r1 = new Vector2(r.X + r.Width, r.Y + r.Height);
            var r2 = new Vector2(X + Width, Y + Height);

            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            if (l1.Y < r2.Y || l2.Y < r1.Y)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the intersecting area of this rectangle and r.
        /// </summary>
        /// <param name="r">The rectangle to check intersection with.</param>
        /// <returns></returns>
        public AptRectangle Intersect(AptRectangle r)
        {
            var x1 = Math.Max(X, r.X);
            var x2 = Math.Min(X + Width, r.X + r.Width);
            var y1 = Math.Max(Y, r.Y);
            var y2 = Math.Min(Y + Height, r.Y + r.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new AptRectangle(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }
    }
}