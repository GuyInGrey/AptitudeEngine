using System;
using System.Runtime.CompilerServices;

namespace AptitudeEngine
{
    public struct Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0f, 0f);

        public float X { get; set; }
        public float Y { get; set; }

        public float SquareMagnitude => (X * X) + (Y * Y);

        public float Magnitude => (float)Math.Sqrt(SquareMagnitude);

        public Vector2 Normalized => this * (1f / Magnitude);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        {
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
            => new Vector2(left.X - right.X, left.Y - right.Y);

        public static Vector2 operator +(Vector2 left, Vector2 right)
            => new Vector2(left.X + right.X, left.Y + right.Y);

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
    }
}