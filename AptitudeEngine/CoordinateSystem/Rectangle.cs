using System;

namespace AptitudeEngine
{
    public class Rectangle
    {
        /// <summary>
        /// Represents a rectangle with position (0,0) and size (0,0)
        /// </summary>
        public static readonly Rectangle Zero = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// Represents a rectangle with position (0,0) and size (1,1)
        /// </summary>
        public static readonly Rectangle One = new Rectangle(0, 0, 1, 1);

        /// <summary>
        /// The X axis location.
        /// </summary>
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }

        /// <summary>
        /// The Y axis location.
        /// </summary>
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }

        /// <summary>
        /// The size on the X axis.
        /// </summary>
        public float Width
        {
            get => Size.X;
            set => Size = new Vector2(value, Size.Y);
        }

        /// <summary>
        /// The size on the Y axis.
        /// </summary>
        public float Height
        {
            get => Size.Y;
            set => Size = new Vector2(Size.X, value);
        }

        /// <summary>
        /// The position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Check whether vector v is within the bounds of the rectangle.
        /// </summary>
        /// <param name="v">The Vector to check.</param>
        /// <returns></returns>
        public bool ContainsVector(Vector2 v)
            => v.X > X && v.Y > Y && v.X < X + Width && v.Y < Y + Height;

        /// <summary>
        /// Create a new <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="width">The width size.</param>
        /// <param name="height">The height size.</param>
        public Rectangle(float x, float y, float width, float height) : this(new Vector2(x, y), new Vector2(width, height)) { }
        

        /// <summary>
        /// Create a new <see cref="Rectangle"/>. If possible use <see cref="Rectangle(float, float, float, float)"/> constructor instead.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// See if this rectangle's bounds intersect at all with the given rectangle.
        /// </summary>
        /// <param name="r">The rectangle to check with.</param>
        /// <returns></returns>
        public bool IntersectsWith(Rectangle r)
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
        public Rectangle Intersect(Rectangle r)
        {
            var x1 = Math.Max(X, r.X);
            var x2 = Math.Min(X + Width, r.X + r.Width);
            var y1 = Math.Max(Y, r.Y);
            var y2 = Math.Min(Y + Height, r.Y + r.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }

            return Zero;
        }

        public Vector2 Center => new Vector2(X + (Width / 2), Y + (Height / 2));
    }
}