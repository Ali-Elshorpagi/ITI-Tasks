using System.Collections;

namespace Task02
{
    public delegate bool Predicate<in T>(T obj);
    internal class MyList<T> : IEnumerable<T>
    {
        private T[] _items;
        private int Capacity = 5;
        public int Count { get; private set; }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                _items[index] = value;
            }
        }
        public MyList()
        {
            _items = new T[5];
            Count = 0;
        }
        public MyList(int size)
        {
            Capacity = size;
            _items = new T[size];
            Count = 0;
        }
        private void ExpandCapacity()
        {
            Capacity <<= 1;
            T[] tmpArr = new T[Capacity];
            Array.Copy(_items, tmpArr, _items.Length);
            _items = tmpArr;
        }
        public void Add(T item)
        {
            if( Count == Capacity )
                ExpandCapacity();
            _items[Count++] = item;
        }
        public bool Remove(T item)
        {
            int idx = Array.IndexOf(_items, item, 0, Count);
            if (idx == -1)
                return false;

            for (int i = idx + 1; i < Count; ++i)
                _items[i - 1] = _items[i];

            --Count;
            _items[Count] = default;

            return true;
        }
        public IEnumerator<T> GetEnumerator()
        {
           for(int i = 0; i < Count; ++i)
                yield return _items[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public T Find(Predicate<T> match)
        {
            if(match is null)
                return default;

            for(int i = 0; i < Count; ++i)
            {
                if(match(_items[i]))
                    return _items[i];
            }

            return default;
        }
        public MyList<T> FindAll(Predicate<T> match)
        {
            if (match is null)
                return new MyList<T>();

            MyList<T> result = new MyList<T>();

            for (int i = 0; i < Count; ++i)
            {
                if (match(_items[i]))
                    result.Add(_items[i]);
            }

            return result;
        }

    }
}
