using System;
using AptitudeEngine.Enums;

namespace AptitudeEngine.Events
{
    public class KeyboardKeyEventArgs : EventArgs
    {
        KeyCode key;

        /// <summary>
        /// Constructs a new KeyboardEventArgs instance.
        /// </summary>
        public KeyboardKeyEventArgs() { }

        /// <summary>
        /// Constructs a new KeyboardEventArgs instance.
        /// </summary>
        /// <param name="args">An existing KeyboardEventArgs instance to clone.</param>
        public KeyboardKeyEventArgs(KeyboardKeyEventArgs args)
            => Key = args.Key;

        /// <summary>
        /// Gets the <see cref="Key"/> that generated this event.
        /// </summary>
        public KeyCode Key
        {
            get => key;
            internal set => key = value;
        }

        public static implicit operator KeyboardKeyEventArgs(OpenTK.Input.KeyboardKeyEventArgs args)
            => new KeyboardKeyEventArgs {Key = (KeyCode) args.Key};
    }
}