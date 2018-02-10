using System;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components
{
    public class Timer : AptComponent
    {
        public event EventHandler<TimerEventArgs> Tick;

        public bool Enabled { get; set; } = true;

        public float IntervalInSeconds { get; set; } = 1f;

        public float DeltaSinceLastTick { get; private set; }

        public override void Render(FrameEventArgs a)
        {
            if (Enabled)
            {
                DeltaSinceLastTick += (float)a.Delta;

                if (DeltaSinceLastTick > IntervalInSeconds)
                {
                    Tick(this, new TimerEventArgs(this));
                    DeltaSinceLastTick = 0f;
                }
            }
        }
    }
}