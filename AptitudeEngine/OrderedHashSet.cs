using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine
{
    public class OrderedHashSet<T> : KeyedCollection<T, T>
    {
        public bool TryAdd(T item)
        {
            try
            {
                Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryRemove(T item)
        {
            try
            {
                Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override T GetKeyForItem(T item)
            => item;
    }
}