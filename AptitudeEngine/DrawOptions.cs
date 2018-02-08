using System;

namespace AptitudeEngine
{
    [Flags()]
    public enum DrawFlags : int
    {
        None = 0,
        ParentCoordinateRelative = 1,
        CustomBounds = 2,
    }
}