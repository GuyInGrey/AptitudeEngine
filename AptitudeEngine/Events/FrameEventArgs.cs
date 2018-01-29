using System;

namespace AptitudeEngine.Events
{
    public class FrameEventArgs : EventArgs
    {
        double elapsed;

        /// <summary>
        /// Constructs a new FrameEventArgs instance. 
        /// </summary>
        public FrameEventArgs() { }

        /// <summary>
        /// Constructs a new FrameEventArgs instance. 
        /// </summary>
        /// <param name="elapsed">The amount of time that has elapsed since the previous event, in seconds.</param>
        public FrameEventArgs(double elapsed) => Time = elapsed;

        /// <summary>
        /// Gets a <see cref="System.Double"/> that indicates how many seconds of time elapsed since the previous frame.
        /// </summary>
        public double Time
        {
            get => elapsed;
            internal set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                elapsed = value;
            }
        }

        public static implicit operator FrameEventArgs(OpenTK.FrameEventArgs args)
            => new FrameEventArgs(args.Time);
    }
}