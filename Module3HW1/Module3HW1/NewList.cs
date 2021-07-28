using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Module3HW1
{
    public class NewList<T>
    {
        private const int DefaultCapacity = 4;
        private T[] _items;
        private int _indexItem;
        private static readonly T[] _emptyArray = new T[0];

        public NewList()
        {
            _items = _emptyArray;
        }

        public int Count => _indexItem;

        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        var newItems = new T[value];
                        if (_indexItem > 0)
                        {
                            Array.Copy(_items, newItems, _indexItem);
                        }

                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }

        public void AddRange(IEnumerable<T> collection)
            => InsertRange(_indexItem, collection);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        public void Add(T item)
        {
            var mass = _items;
            var indexItem = _indexItem;
            if (indexItem < mass.Length)
            {
                _indexItem = indexItem + 1;
                mass[indexItem] = item;
            }
            else
            {
                AddWithResize(item);
            }
        }

        public void AddWithResize(T item)
        {
            var indexItem = _indexItem;
            EnsureCapacity(indexItem + 1);
            _indexItem = indexItem + 1;
            _items[indexItem] = item;
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection is ICollection<T> c)
            {
                var count = c.Count;
                if (count > 0)
                {
                    EnsureCapacity(_indexItem + count);
                    if (index < _indexItem)
                    {
                        Array.Copy(_items, index, _items, index + count, _indexItem - index);
                    }

                    if (this == c)
                    {
                        Array.Copy(_items, 0, _items, index, index);
                        Array.Copy(_items, index + count, _items, index * 2, _indexItem - index);
                    }
                    else
                    {
                        c.CopyTo(_items, index);
                    }

                    _indexItem += count;
                }
            }
            else
            {
                using (var en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Insert(index++, en.Current);
                    }
                }
            }
        }

        public void Insert(int index, T item)
        {
            if (_indexItem == _items.Length)
            {
                EnsureCapacity(_indexItem + 1);
            }

            if (index < _indexItem)
            {
                Array.Copy(_items, index, _items, index + 1, _indexItem - index);
            }

            _items[index] = item;
            _indexItem++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < _indexItem)
            {
                Array.Copy(_items, index + 1, _items, index, _indexItem - index);
            }

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                _items[_indexItem] = default;
            }

            _indexItem--;
        }

        public void Sort()
        {
            Array.Sort(_items, new ItemComparer<T>());
        }

        public int IndexOf(T item)
            => Array.IndexOf(_items, item, 0, _indexItem);

        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                var newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                if (newCapacity < min)
                {
                    newCapacity = min;
                }

                Capacity = newCapacity;
            }
        }
    }
}
