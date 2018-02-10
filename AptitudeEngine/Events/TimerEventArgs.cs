using System;
using AptitudeEngine.Components;

namespace AptitudeEngine.Events
{
    public class TimerEventArgs : EventArgs
    {
        public Timer Timer { get; set; }

        public TimerEventArgs(Timer o) => Timer = o;
    }
}
