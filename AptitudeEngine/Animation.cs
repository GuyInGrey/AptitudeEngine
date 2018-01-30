using System.Collections.Generic;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine
{
    public class Animation
    {
        public List<AptRectangle> Frames { get; set; }
        public int FrameRate { get; set; }
    }
}