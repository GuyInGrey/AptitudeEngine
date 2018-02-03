using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AptitudeEngine.CoordinateSystem
{
    public struct Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0f, 0f);

        public float X { get; set; }
        public float Y { get; set; }

        public float SquareMagnitude => (X * X) + (Y * Y);

        public float Magnitude => (float) Math.Sqrt(SquareMagnitude);

        public Vector2 Normalized => this * (1f / Magnitude);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Vector2(OpenTK.Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }

        public static Vector2 Add(params Vector2[] vecs)
        {
            var toReturn = Zero;
            for (var i = 0; i < vecs.Length; i++)
            {
                toReturn += vecs[i];
            }

            return toReturn;
        }

        /// <summary>
        /// Add the specified instances
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of addition</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(Vector2 a, Vector2 b)
            => a + b;

        /// <summary>
        /// Subtract one Vector from another
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Result of subtraction</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 a, Vector2 b)
            => a - b;

        /// <summary>
        /// Multiply a vector and a scalar
        /// </summary>
        /// <param name="a">Vector operand</param>
        /// <param name="f">Scalar operand</param>
        /// <returns>Result of the multiplication</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 a, float f)
            => a * f;

        /// <summary>
        /// Divide a vector by a scalar
        /// </summary>
        /// <param name="a">Vector operand</param>
        /// <param name="f">Scalar operand</param>
        /// <returns>Result of the division</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 a, float f)
            => a / f;

        /// <summary>
        /// Calculate the dot (scalar) product of two vectors
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>The dot product of the two inputs</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 a, Vector2 b)
            => (a.X * b.X) + (a.Y * b.Y);

        /// <summary>
        /// Returns a new Vector that is the linear blend of the 2 given Vectors
        /// </summary>
        /// <param name="a">First input vector</param>
        /// <param name="b">Second input vector</param>
        /// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
        /// <returns>a when blend=0, b when blend=1, and a linear combination otherwise</returns>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float blend)
            => new Vector2(blend * (b.X - a.X) + a.X, blend * (b.Y - a.Y) + a.Y);

        /// <summary>
        /// Interpolate 3 Vectors using Barycentric coordinates
        /// </summary>
        /// <param name="a">First input Vector</param>
        /// <param name="b">Second input Vector</param>
        /// <param name="c">Third input Vector</param>
        /// <param name="u">First Barycentric Coordinate</param>
        /// <param name="v">Second Barycentric Coordinate</param>
        /// <returns>a when u=v=0, b when u=1,v=0, c when u=0,v=1, and a linear combination of a,b,c otherwise</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 BaryCentric(Vector2 a, Vector2 b, Vector2 c, float u, float v)
            => a + u * (b - a) + v * (c - a);

        /// <summary>
        /// Scales the Vector2 to unit length.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 a)
            => a.Normalized;

        /// <summary>
        /// Clamp a vector to the given minimum and maximum vectors
        /// </summary>
        /// <param name="vec">Input vector</param>
        /// <param name="min">Minimum vector</param>
        /// <param name="max">Maximum vector</param>
        /// <returns>The clamped vector</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(Vector2 vec, Vector2 min, Vector2 max)
        {
            vec.X = vec.X < min.X ? min.X :
                vec.X > max.X ? max.X : vec.X;
            vec.Y = vec.Y < min.Y ? min.Y :
                vec.Y > max.Y ? max.Y : vec.Y;
            return vec;
        }

        public Vector2 Rotate(Vector2 centerPoint, float angleInRadians)
        {
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);

            return new Vector2()
            {
                X =
                    (float)
                    (cosTheta * (X - centerPoint.X) -
                    sinTheta * (Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (float)
                    (sinTheta * (X - centerPoint.X) +
                    cosTheta * (Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public Vector2 Move(double angle, double distance)
        {
            angle = angle + 270;
            var toReturn = Zero;
            toReturn.X = X + (float)(Math.Cos(angle * Math.PI / 180.0) * distance);
            toReturn.Y = Y + (float)(Math.Sin(angle * Math.PI / 180.0) * distance);

            return toReturn;
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
            => new Vector2(left.X - right.X, left.Y - right.Y);

        public static Vector2 operator -(Vector2 vec)
            => new Vector2(-vec.X, -vec.Y);

        public static Vector2 operator +(Vector2 left, Vector2 right)
            => new Vector2(left.X + right.X, left.Y + right.Y);

        public static Vector2 operator +(Vector2 left, Vector2? right) 
            => (right == null) ? left : left + right.Value;

        public static Vector2 operator *(Vector2 left, float right)
            => new Vector2(left.X * right, left.Y * right);

        public static Vector2 operator *(float left, Vector2 right)
            => new Vector2(left * right.X, left * right.Y);

        public static Vector2 operator /(Vector2 left, float right)
            => new Vector2(left.X / right, left.Y / right);

        public static implicit operator OpenTK.Vector2(Vector2 vec)
            => new OpenTK.Vector2(vec.X, vec.Y);

        public static implicit operator OpenTK.Vector3(Vector2 vec)
            => new OpenTK.Vector3(vec.X, vec.Y, 0f);

        public override string ToString() => "(" + X + "," + Y + ")";
    }
}