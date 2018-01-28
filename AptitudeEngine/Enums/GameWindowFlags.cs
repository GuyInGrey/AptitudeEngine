using System;

namespace AptitudeEngine.Enums
{
    [Flags]
    public enum GameWindowFlags
    {
        Default = 0,
        Fullscreen = 1,
        FixedWindow = 2
    }
}