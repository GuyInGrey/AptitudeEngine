using System.Collections.ObjectModel;

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