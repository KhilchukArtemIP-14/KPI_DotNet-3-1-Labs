using System;
using System.Collections;
using System.Collections.Generic;
namespace MyList
{
    public class CustomList<T> : ICollection<T>
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

        public int Count =>_count;

        public bool IsReadOnly => false;
        
        public void Add(T item)
        {
            if(_end is null)
            {
                _start = new ListNode<T>(item);
                _end = _start;
            }
            else
            {
                _end.Next= new ListNode<T>(item);
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
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
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
                }
                else { throw new IndexOutOfRangeException("Index was out of range"); }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public class MyEnumerator : IEnumerator<T>
        {
            private T _current { get; set; }
            public T Current => _current;
            private readonly CustomList<T> _myCollection;
            private int _indexer;
            object IEnumerator.Current => _current;
            public MyEnumerator(CustomList<T> myCollection)
            {
                _myCollection = myCollection;
                _indexer = 0;
                if(_myCollection?.Count>0)
                {
                    _current = _myCollection[_indexer];
                }
                else
                {
                    _current= _myCollection == null ? throw new Exception("The collection is null"):default(T);
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_indexer >= _myCollection?.Count)
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
                if (_myCollection?.Count > 0)
                {
                    _current = _myCollection[_indexer];
                }
                else
                {
                    _current = _myCollection == null ? throw new Exception("The collection is null") : default(T);
                }
            }
        }

        private class ListNode<T>
        {
            public T Value { get; set; }
            public ListNode<T> Next { get; set; } = null;
            //public ListNode<T> Previous { get; set; } = null;
            internal ListNode(T value) {
                Value = value;
            }
        }
    }
}
