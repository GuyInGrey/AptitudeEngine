using System.Drawing;

namespace AptitudeEngine.CoordinateSystem
{
    public class PolyVector
    {
        public Vector2 Position;
        public Color Color;

        /// <summary>
        /// Creates a new <see cref="PolyVector"/>.
        /// </summary>
        /// <param name="pos">The position of the vector.</param>
        /// <param name="c">The color of the vector.</param>
        public PolyVector(Vector2 pos, Color c)
        {
            Position = pos;
            Color = c;
        }
    }
}