using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Collections
{
    public sealed class ConcurrentHashSet<T> :
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable,
        IReadOnlyCollection<T>,
        ISet<T>
    {
        #region Private Fields

        private readonly object _lock = new object();

        private readonly HashSet<T> _set;

        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _set.Count;
                }
            }
        }

        #endregion

        #region Excplicit Properties

        bool ICollection<T>.IsReadOnly => ((ICollection<T>) _set).IsReadOnly;

        #endregion


        #region Constructors

        public ConcurrentHashSet()
        {
            _set = new HashSet<T>();
        }

        public ConcurrentHashSet(IEnumerable<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            _set = new HashSet<T>(collection);
        }

        public ConcurrentHashSet(IEqualityComparer<T> comparer)
        {
            comparer.ThrowIfNull(nameof(comparer));

            _set = new HashSet<T>(comparer);
        }

        public ConcurrentHashSet(int capacity)
        {
            _set = new HashSet<T>(capacity);
        }

        public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            collection.ThrowIfNull(nameof(collection));
            comparer.ThrowIfNull(nameof(comparer));

            _set = new HashSet<T>(collection, comparer);
        }

        public ConcurrentHashSet(int capacity, IEqualityComparer<T> comparer)
        {
            capacity.ThrowIfValueIsOutOfRange(nameof(capacity), 0, int.MaxValue);
            comparer.ThrowIfNull(nameof(comparer));

            _set = new HashSet<T>(capacity, comparer);
        }

        #endregion

        #region Additional Methods

        public T[] ToArray()
        {
            lock (_lock)
            {
                return _set.ToArray();
            }
        }

        public List<T> ToList()
        {
            lock (_lock)
            {
                return _set.ToList();
            }
        }

        public IReadOnlyList<T> ToReadOnlyList()
        {
            lock (_lock)
            {
                return _set.ToReadOnlyList();
            }
        }

        public IReadOnlyCollection<T> ToReadOnlyCollection()
        {
            lock (_lock)
            {
                return _set.ToReadOnlyCollection();
            }
        }

        #endregion

        #region ISet<T> Implementation

        public bool Add([AllowNull] T item)
        {
            lock (_lock)
            {
                return _set.Add(item);
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            lock (_lock)
            {
                _set.ExceptWith(other);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            lock (_lock)
            {
                _set.IntersectWith(other);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            lock (_lock)
            {
                return _set.IsProperSubsetOf(other);
            }
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            lock (_lock)
            {
                return _set.IsProperSupersetOf(other);
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            lock (_lock)
            {
                return _set.IsSubsetOf(other);
            }
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            lock (_lock)
            {
                return _set.IsSupersetOf(other);
            }
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            lock (_lock)
            {
            }   return _set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            lock (_lock)
            {
                return _set.SetEquals(other);
            }
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            lock (_lock)
            {
                _set.SymmetricExceptWith(other);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            lock (_lock)
            {
                _set.UnionWith(other);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            lock (_lock)
            {
                foreach (T value in values)
                {
                    _set.Add(value);
                }
            }
        }

        public bool Remove([AllowNull] T item)
        {
            lock (_lock)
            {
                return _set.Remove(item);
            }
        }

        public int RemoveWhere(Predicate<T> match)
        {
            lock (_lock)
            {
                return _set.RemoveWhere(match);
            }
        }

        public bool Contains([AllowNull] T item)
        {
            lock (_lock)
            {
                return _set.Contains(item);
            }
        }

        #endregion

        #region ICollection<T> Implementation

        public void Clear()
        {
            _set.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _set.CopyTo(array, arrayIndex);
            }
        }

        void ICollection<T>.Add([AllowNull] T item)
        {
            lock (_lock)
            {
                _set.Add(item);
            }
        }

        #endregion

        #region IEnumerable<T> Implementation

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _set.GetEnumerator();
            }
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
