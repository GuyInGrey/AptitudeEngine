using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine.Events
{
    public class KeyPressEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="keyChar">The ASCII character that was typed.</param>
        public KeyPressEventArgs(char keyChar)
            => KeyChar = keyChar;

        /// <summary>
        /// Gets a <see cref="System.Char"/> that defines the ASCII character that was typed.
        /// </summary>
        public char KeyChar { get; internal set; }

        public static implicit operator KeyPressEventArgs(OpenTK.KeyPressEventArgs args)
            => new KeyPressEventArgs(args.KeyChar);
    }
}
