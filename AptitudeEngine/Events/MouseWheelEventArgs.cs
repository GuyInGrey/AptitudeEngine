using System;

namespace AptitudeEngine.Events
{
    public class MouseWheelEventArgs : OpenTK.Input.MouseEventArgs
    {
        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        public MouseWheelEventArgs()
        {
        }

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="value">The value of the wheel.</param>
        /// <param name="delta">The change in value of the wheel for this event.</param>
        public MouseWheelEventArgs(int x, int y, int value, int delta)
            : base(x, y)
        {
            ValuePrecise = value;
            DeltaPrecise = delta;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="value">The value of the wheel.</param>
        /// <param name="delta">The change in value of the wheel for this event.</param>
        public MouseWheelEventArgs(int x, int y, float value, float delta)
            : base(x, y)
        {
            ValuePrecise = value;
            DeltaPrecise = delta;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseWheelEventArgs"/> instance to clone.</param>
        public MouseWheelEventArgs(MouseWheelEventArgs args)
            : this(args.X, args.Y, args.Value, args.Delta)
        {
        }

        /// <summary>
        /// Gets the value of the wheel in integer units.
        /// To support high-precision mice, it is recommended to use <see cref="ValuePrecise"/> instead.
        /// </summary>
        public int Value => (int) Math.Round(ValuePrecise, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Gets the change in value of the wheel for this event in integer units.
        /// To support high-precision mice, it is recommended to use <see cref="DeltaPrecise"/> instead.
        /// </summary>
        public int Delta => (int) Math.Round(DeltaPrecise, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Gets the precise value of the wheel in floating-point units.
        /// </summary>
        public float ValuePrecise { get; internal set; }

        /// <summary>
        /// Gets the precise change in value of the wheel for this event in floating-point units.
        /// </summary>
        public float DeltaPrecise { get; internal set; }

        public static implicit operator MouseWheelEventArgs(OpenTK.Input.MouseWheelEventArgs args)
            => new MouseWheelEventArgs(args.X, args.Y, args.ValuePrecise, args.DeltaPrecise);
    }
}