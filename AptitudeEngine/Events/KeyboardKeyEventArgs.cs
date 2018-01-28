using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptitudeEngine;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using AptitudeEngine.Logging;

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