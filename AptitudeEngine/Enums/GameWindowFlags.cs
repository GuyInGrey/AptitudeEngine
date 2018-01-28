using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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