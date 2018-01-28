using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine.Events
{
    public class MouseMoveEventArgs : OpenTK.Input.MouseEventArgs
    {
        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        public MouseMoveEventArgs() { }

        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="xDelta">The change in X position produced by this event.</param>
        /// <param name="yDelta">The change in Y position produced by this event.</param>
        public MouseMoveEventArgs(int x, int y, int xDelta, int yDelta)
            : base(x, y)
        {
            XDelta = xDelta;
            YDelta = yDelta;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseMoveEventArgs"/> instance to clone.</param>
        public MouseMoveEventArgs(MouseMoveEventArgs args)
            : this(args.X, args.Y, args.XDelta, args.YDelta) { }

        /// <summary>
        /// Gets the change in X position produced by this event.
        /// </summary>
        public int XDelta { get; internal set; }

        /// <summary>
        /// Gets the change in Y position produced by this event.
        /// </summary>
        public int YDelta { get; internal set; }

        public static implicit operator MouseMoveEventArgs(OpenTK.Input.MouseMoveEventArgs args)
            => new MouseMoveEventArgs(args.X, args.Y, args.XDelta, args.YDelta);
    }
}