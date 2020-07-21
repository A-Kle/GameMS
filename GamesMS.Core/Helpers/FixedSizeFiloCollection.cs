using System.Collections;
using System.Collections.Generic;

namespace GamesMS.Core.Helpers
{
    public class FixedSizeFiloCollection<T> : IEnumerable<T>
    {
        private readonly int size;
        private IList<T> Items { get; }
        public FixedSizeFiloCollection(int size)
        {
            Items = new List<T>(size);
            this.size = size;
        }

        public void Push(T obj)
        {
            if (Items.Count < size)
            {
                Items.Insert(0, obj);
            }
            else
            {
                if(size != 0)
                    Items.RemoveAt(size - 1);

                Items.Insert(0, obj);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
