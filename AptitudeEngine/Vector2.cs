using System;

namespace AptitudeEngine
{
    public struct Vector2
    {
		public static readonly Vector2 Zero = new Vector2(0f, 0f); 

        public float X { get; set; }
        public float Y { get; set; }

		public float SquareMagnitude => (X * X) + (Y * Y);
		
		public float Magnitude => (float)Math.Sqrt(SquareMagnitude);

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

		public void Normalize()
		{
			var inverse = 1f / Magnitude;
			X *= inverse;
			Y *= inverse;
		}

		public Vector2 Normalized()
			=> this * (1f / Magnitude);

		public static implicit operator OpenTK.Vector2(Vector2 vec)
			=> new OpenTK.Vector2(vec.X, vec.Y);
		public static implicit operator OpenTK.Vector3(Vector2 vec)
			=> new OpenTK.Vector3(vec.X, vec.Y, 0f);
		public static Vector2 operator *(Vector2 a, Vector2 b)
			=> new Vector2(a.X * b.X, a.Y * b.Y);
		public static Vector2 operator *(Vector2 a, float b)
			=> new Vector2(a.X * b, a.Y * b);
    }
}