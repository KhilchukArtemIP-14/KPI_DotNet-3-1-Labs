using System;
using System.Collections;
using System.Collections.Generic;
namespace MyList
{
    public class CustomList<T> : IList<T>
    {
        private ListNode<T> _start;
        private ListNode<T> _end;
        private int _count;
        private int _version;
        public CustomList()
        {
            _count = 0;
            _start = null;
            _end = null;
            _version = 0;
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
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

        public void Clear()
        {
            _end = null;
            _start = null;
            _count = 0;
            _version++;
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

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        public bool Remove(T item)
        {
            var tmp = _start;
            if (tmp is null) return false;

            if (tmp.Value.Equals(item))
            {
                _start = tmp.Next;

                _count--;
                _version++;

                return true;
            }
            var prvs = tmp;

            tmp = tmp.Next;

            while (tmp != _end)
            {
                if (tmp.Value.Equals(item))
                {
                    prvs.Next = tmp.Next;

                    _count--;
                    _version++;

                    return true;
                }

                prvs = tmp;
                tmp = tmp.Next;
            }

            if (_end.Value.Equals(item))
            {
                _end = prvs;
                _end.Next = null;
                _count--;
                _version++;

                return true;
            }

            return false;
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
                }
                else { throw new IndexOutOfRangeException("Index was out of range"); }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public void Insert(int index, T item)
        {
            if (_start is null || (index == _count)) Add(item);
            if (index == 0)
            {
                var tmp = new ListNode<T>(item);
                tmp.Next = _start;
                _start = tmp;
                _count++;
                _version++;
                return;
            }
            var prvs = _start;
            var curr = prvs.Next;
            for (int i = 1; i < _count; i++)
            {
                if (i == index)
                {
                    var tmp = new ListNode<T>(item);
                    tmp.Next = curr;
                    prvs.Next = tmp;
                    _count++;
                    _version++;
                    return;
                }
                prvs = curr;
                curr = curr.Next;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("Argument was out of range");
            if (_start is null) return;

            _count--;
            _version++;

            if (index == 0)
            {
                _start = _start.Next;
                return;
            }

            var prvs = _start;
            var curr = prvs.Next;
            for (int i = 1; i < _count - 1; i++)
            {
                if (i == index)
                {
                    prvs.Next = curr.Next;
                    return;
                }
                prvs = curr;
                curr = curr.Next;
            }
            if (index == _count - 1)
            {
                _end = prvs;
                _end.Next = null;
            }
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
            internal ListNode(T value)
            {
                Value = value;
            }
        }
    }
}
