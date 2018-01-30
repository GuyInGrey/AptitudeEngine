using System.Drawing;

namespace AptitudeEngine.CoordinateSystem
{
    public class PolyPoint
    {
        public Vector2 Position;
        public Color Color;

        public PolyPoint(Vector2 pos, Color c)
        {
            Position = pos;
            Color = c;
        }
    }
}