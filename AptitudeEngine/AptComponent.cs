using AptitudeEngine.CoordinateSystem;
using System;

namespace AptitudeEngine
{
    public class AptComponent : ComponentEventChain, IDisposable
    {
        /// <summary>
        /// Global Identification for <see cref="AptComponent"/>, not unique because the component is not stored in a table.
        /// </summary>
        public string Gid { get; }

        /// <summary>
        /// The private owner <see cref="AptObject"/> of the <see cref="AptComponent"/>
        /// </summary>
        internal AptObject owner;

        /// <summary>
        /// The owner <see cref="AptObject"/> of the <see cref="AptComponent"/>
        /// </summary>
        public AptObject Owner => owner;

        /// <summary>
        /// The owner <see cref="AptObject"/>'s context
        /// </summary>
        public AptContext Context => owner?.Context;

        /// <summary>
        /// The owner <see cref="AptObject"/>'s transform
        /// </summary>
        public Transform Transform => owner?.Transform;

        /// <summary>
        /// The <see cref="Context"/>'s input
        /// </summary>
        public AptInput Input => Context?.Input;

        /// <summary>
        /// Gets a value indicating whether this <see cref="AptComponent"/> is disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources related to the <see cref="AptComponent"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing) { }

            owner = null;
            Disposed = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AptComponent"/> class.
        /// </summary>
        public AptComponent() =>
            // We're not ensuring Guid is totally unique like in AptObject
            // because its not going to be in a table.
            Gid = System.Guid.NewGuid().ToString("N");

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this object by comparing Gid.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        public override bool Equals(object obj)
            => obj is AptComponent comp && Gid.Equals(comp.Gid);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            var hash = 269;
            hash = (hash * 47) + Gid.GetHashCode();
            return hash;
        }
    }
}