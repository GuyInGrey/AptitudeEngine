using System;

namespace AptitudeEngine.CoordinateSystem
{
    public class Transform
    {
        /// <summary>
        /// The Bounds of the <see cref="Transform"/>, as per <see cref="Transform.Position"/> and <see cref="Transform.Size"/>.
        /// </summary>
        public AptRectangle Bounds => new AptRectangle(Position, Size);

        /// <summary>
        /// World space position, relative to the world's origin (0, 0).
        /// </summary>
        public Vector2 Position { get; set; } = new Vector2(0f, 0f);

        /// <summary>
        /// World space rotation, in clockwise euler degrees.
        /// </summary>
        public float Rotation { get; set; } = 0f;

        /// <summary>
        /// World space scale of this transform.
        /// </summary>
        public Vector2 Size { get; set; } = new Vector2(1f, 1f);

        /// <summary>
        /// World space rotation, in counter-clockwise radians.
        /// </summary>
        public float RotationRadians => (float) Math.PI * Rotation / 180f;

        // Right is considered the "forward" vector, as it exists at 0 radians.
        // We negate RotationRadians because Cos and Sin are calculated using 
        // counter-clockwise rotation, which is counter intuitive to euler rotation (which we are using).
        public Vector2 Right
            => new Vector2((float) Math.Cos(-RotationRadians), (float) Math.Sin(-RotationRadians));

        // Up is 90 degrees, or PI / 2 radians, past the Right vector.
        public Vector2 Up
            => new Vector2(
                (float) Math.Cos(-RotationRadians + (Math.PI / 2)),
                (float) Math.Sin(-RotationRadians + (Math.PI / 2))
            );

        public Vector2 Left
            => -Right;

        public Vector2 Down
            => -Up;

        /// <summary>
        /// Moves this transform a specific amount specified by <paramref name="move"/>,
        /// applied relative to the rotation of this transform.
        /// </summary>
        /// <param name="move">The vector to rotate this object towards.</param>
        public void Translate(Vector2 move)
        {
            var sin = (float) Math.Sin(RotationRadians);
            var cos = (float) Math.Cos(RotationRadians);

            Position = new Vector2(
                move.X * cos - move.Y * sin,
                move.X * sin + move.Y * cos
            );
        }

        /// <summary>
        /// Rotates this transform by <paramref name="degrees"/>.
        /// </summary>
        /// <param name="degrees">The amount of degrees to rotate by.</param>
        public void Rotate(float degrees)
            // lmao p simple right
            => Rotation += degrees;
    }
}