using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine
{
    public class AptComponent : ComponentEventChain, IDisposable
    {
        /// <summary>
        /// Global Identification for <see cref="AptComponent"/>, not unique!
        /// </summary>
        public string Gid { get; }

        internal AptObject owner;
        public AptObject Owner => owner;
        public AptContext Context => owner?.Context;
        public Transform Transform => owner?.Transform;
        public AptInput Input => Context?.Input;
        public bool Disposed { get; private set; }

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

            if (disposing)
            {

            }

            owner = null;

            Disposed = true;
        }

        public AptComponent() =>
            // We're not ensuring Guid is totally unique like in AptObject
            // because its not going to be in a table.
            Gid = System.Guid.NewGuid().ToString("N");

        public override bool Equals(object obj)
            => obj is AptComponent comp && Gid.Equals(comp.Gid);

        public override int GetHashCode()
        {
            var hash = 269;
            hash = (hash * 47) + Gid.GetHashCode();
            return hash;
        }
    }
}