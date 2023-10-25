using System;
using System.Collections;
using System.Collections.Generic;
namespace MyList
{
    public class CustomList<T> : IList<T>
    {
        private ListNode<T> _start;
        private ListNode<T> _end;
        private int _version;
        private int _count;
        public int Count => _count;
        public bool IsReadOnly => false;

        public event EventHandler<T> ItemAdded;
        public event EventHandler<T> ItemInserted;
        public event EventHandler<T> ItemRemoved;
        public event EventHandler<int> ItemSet;
        public event EventHandler Cleared;


        public CustomList()
        {
            _count = 0;
            _start = null;
            _end = null;
            _version = 0;
        }
        
        public void Clear()
        {
            _end = null;
            _start = null;
            _count = 0;
            _version++;
            Cleared?.Invoke(this, EventArgs.Empty);
        }
        
        public int IndexOf(T item)
        {
            int i = 0;
            foreach (var a in this)
            {
                if (a.Equals(item)) return i;
                i++;
            }
            return -1;
        }
        public bool Contains(T item)
        {
            foreach (var a in this)
            {
                if (a.Equals(item)) return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new Exception("Array is null");
            if (array.Length - arrayIndex < Count) throw new Exception("Array doesn't have enough space");

            int i = 0;
            foreach (var a in this)
            {
                array[arrayIndex + i] = a;
                i++;
            }
        }
        public void Add(T item)
        {
            AppendItem(item);

            ItemAdded?.Invoke(this, item);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count) throw new ArgumentOutOfRangeException("Index out of range");

            if (Count == 0 || index == _count)
            {
                AppendItem(item);

                ItemInserted?.Invoke(this, item);
                return;
            }

            _count++;
            _version++;

            if (index == 0)
            {
                var tmp = new ListNode<T>(item);
                tmp.Next = _start;
                _start = tmp;
                ItemInserted?.Invoke(this, item);

                return;
            }
            var prvs = _start;
            var curr = prvs.Next;
            for (int i = 1; i < _count - 1; i++)
            {
                if (i == index)
                {
                    var tmp = new ListNode<T>(item);
                    tmp.Next = curr;
                    prvs.Next = tmp;
                    ItemInserted?.Invoke(this, item);

                    return;
                }
                prvs = curr;
                curr = curr.Next;
            }

        }

        public bool Remove(T item)
        {
            if (Count == 0) return false;

            if (_start.Value.Equals(item))
            {
                _start = _start.Next;

                _count--;
                _version++;
                ItemRemoved?.Invoke(this, item);
                return true;
            }

            var prvs = _start;
            var curr = _start.Next;
            while (curr != _end)
            {
                if (curr.Value.Equals(item))
                {
                    prvs.Next = curr.Next;

                    _count--;
                    _version++;
                    ItemRemoved?.Invoke(this, item);
                    return true;
                }

                prvs = curr;
                curr = curr.Next;
            }

            if (_end.Value.Equals(item))
            {
                _end = prvs;
                _end.Next = null;

                _count--;
                _version++;
                ItemRemoved?.Invoke(this, item);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("Argument was out of range");

            _count--;
            _version++;

            if (index == 0)
            {
                var tmp = _start.Value;
                _start = _start.Next;
                ItemRemoved?.Invoke(this, tmp);
                return;
            }

            var prvs = _start;
            var curr = prvs.Next;
            for (int i = 1; i < _count; i++)
            {
                if (i == index)
                {
                    var tmp = curr.Value;
                    prvs.Next = curr.Next;
                    ItemRemoved?.Invoke(this, tmp);
                    return;
                }
                prvs = curr;
                curr = curr.Next;
            }
            if (index == _count)
            {
                var tmp = _end.Value;

                _end = prvs;
                _end.Next = null;
                ItemRemoved?.Invoke(this, tmp);

            }
        }

        public T this[int index]
        {
            get
            {
                if (Count > 0 && index >= 0 && Count > index)
                {
                    var tmp = _start;
                    for (int i = 0; i < index; i++)
                    {
                        tmp = tmp.Next;
                    }
                    return tmp.Value;
                }
                else { throw new IndexOutOfRangeException("Index was out of range"); }
            }
            set
            {
                if (Count > 0 && index >= 0 && Count > index)
                {
                    var tmp = _start;
                    for (int i = 0; i < index; i++)
                    {
                        tmp = tmp.Next;
                    }
                    tmp.Value = value;
                    _version++;
                    ItemSet?.Invoke(this, index);
                }
                else { throw new IndexOutOfRangeException("Index was out of range"); }
            }
        }

        private void AppendItem(T item)
        {
            if (_end is null)
            {
                _start = new ListNode<T>(item);
                _end = _start;
            }
            else
            {
                _end.Next = new ListNode<T>(item);
                _end = _end.Next;
            }
            _count++;
            _version++;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal class MyEnumerator : IEnumerator<T>
        {
            private readonly CustomList<T> _myCollection;
            private readonly int _versionSnapshot;

            private T _current { get; set; }
            public T Current => _current;
            object IEnumerator.Current => _current;

            private int _indexer;

            public MyEnumerator(CustomList<T> myCollection)
            {
                _myCollection = myCollection ?? throw new ArgumentNullException("The collection is null");

                _indexer = 0;
                _versionSnapshot = myCollection._version;

                _current = _myCollection.Count > 0 ? _myCollection[_indexer] : default(T);
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_versionSnapshot != _myCollection._version) throw new Exception("The collection has been modified");

                if (_indexer >= _myCollection.Count)
                {
                    Reset();
                    return false;
                }
                _current = _myCollection[_indexer];
                _indexer++;
                return true;
            }

            public void Reset()
            {
                _indexer = 0;
                _current = _myCollection?.Count > 0 ? _myCollection[_indexer] : default(T);
            }
        }

        private class ListNode<T>
        {
            public T Value { get; set; }
            public ListNode<T> Next { get; set; } = null;
            public ListNode(T value)
            {
                Value = value;
            }
        }
    }
}
