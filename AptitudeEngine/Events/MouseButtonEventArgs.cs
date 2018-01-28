using AptitudeEngine.Enums;

namespace AptitudeEngine.Events
{
    public class MouseButtonEventArgs : OpenTK.Input.MouseEventArgs
    {
        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        public MouseButtonEventArgs() { }

        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="button">The mouse button for the event.</param>
        /// <param name="pressed">The current state of the button.</param>
        public MouseButtonEventArgs(int x, int y, KeyCode key, bool pressed)
            : base(x, y)
        {
            Key = key;
            IsPressed = pressed;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseButtonEventArgs"/> instance to clone.</param>
        public MouseButtonEventArgs(MouseButtonEventArgs args)
            : this(args.X, args.Y, args.Key, args.IsPressed) { }

        /// <summary>
        /// The mouse button for the event.
        /// </summary>
        public KeyCode Key { get; internal set; }

        /// <summary>
        /// Gets a System.Boolean representing the state of the mouse button for the event.
        /// </summary>
        public bool IsPressed { get; internal set; }

        public static implicit operator MouseButtonEventArgs(OpenTK.Input.MouseButtonEventArgs args)
            => new MouseButtonEventArgs(args.X, args.Y, args.Button.ToApt(), args.IsPressed);
    }
}