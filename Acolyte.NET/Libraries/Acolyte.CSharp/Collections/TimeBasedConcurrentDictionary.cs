using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;

namespace Acolyte.Collections
{
    /// <summary>
    /// Extends the standard <see cref="ConcurrentDictionary{TKey, TValue}" /> class with time-based
    /// logic (this collection cleanup expired objects when calling any method).
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public sealed class TimeBasedConcurrentDictionary<TKey, TValue> :
        ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerable,
        IDictionary<TKey, TValue>,
        IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
        IReadOnlyDictionary<TKey, TValue>,
        ICollection, IDictionary
        where TKey : notnull
        where TValue : IHaveCreationTime
    {
        #region Private Fields

        private readonly TimeSpan _lifeTime;
        
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary;

        #endregion

        #region Public Properties

        public TValue this[[DisallowNull] TKey key]
        {
            get
            {
                CleanupExpiredObjects();
                return _dictionary[key];
            }

            set
            {
                CleanupExpiredObjects();
                _dictionary[key] = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                CleanupExpiredObjects();
                return _dictionary.IsEmpty;
            }
        }

        public int Count
        {
            get
            {
                CleanupExpiredObjects();
                return _dictionary.Count;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                CleanupExpiredObjects();
                return _dictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                CleanupExpiredObjects();
                return _dictionary.Values;
            }
        }

        #endregion

        #region Excplicit Properties

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).IsReadOnly;

        bool IDictionary.IsReadOnly => ((IDictionary) _dictionary).IsReadOnly;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

        bool ICollection.IsSynchronized => ((ICollection) _dictionary).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection) _dictionary).SyncRoot;

        bool IDictionary.IsFixedSize => ((IDictionary) _dictionary).IsFixedSize;

        ICollection IDictionary.Keys
        {
            get
            {
                CleanupExpiredObjects();
                return ((IDictionary) _dictionary).Keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                CleanupExpiredObjects();
                return ((IDictionary) _dictionary).Values;
            }
        }

        object IDictionary.this[[DisallowNull] object key]
        {
            get
            {
                CleanupExpiredObjects();
                return ((IDictionary) _dictionary)[key];
            }

            set
            {
                CleanupExpiredObjects();
                ((IDictionary) _dictionary)[key] = value;
            }
        }
        #endregion


        #region Constructors

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime)
        {
            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime,
            IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection);
        }

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime, IEqualityComparer<TKey> comparer)
        {
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(comparer);
        }

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime, int concurrencyLevel, int capacity)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            capacity.ThrowIfValueIsOutOfRange(nameof(capacity), 0, int.MaxValue);

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity);
        }

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime, int concurrencyLevel,
            IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            collection.ThrowIfNull(nameof(collection));
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, collection, comparer);
        }

        public TimeBasedConcurrentDictionary(TimeSpan lifeTime, int concurrencyLevel, int capacity,
            IEqualityComparer<TKey> comparer)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            capacity.ThrowIfValueIsOutOfRange(nameof(capacity), 0, int.MaxValue);
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity, comparer);
        }

        #endregion

        #region Additional Methods

        public void AddOrUpdate([DisallowNull] TKey key, TValue value,
            Func<TKey, TValue, TValue> updateAction)
        {
            CleanupExpiredObjects();
            _dictionary.AddOrUpdate(key, value, updateAction);
        }

        public bool TryRemove([DisallowNull] TKey key, out TValue value)
        {
            CleanupExpiredObjects();
            return _dictionary.TryRemove(key, out value);
        }

        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Implementation

        public void Clear()
        {
            _dictionary.Clear();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            CleanupExpiredObjects();
            ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            CleanupExpiredObjects();
            return ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            CleanupExpiredObjects();
            ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            CleanupExpiredObjects();
            return ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).Remove(item);
        }

        #endregion

        #region ICollection Implementation

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) _dictionary).CopyTo(array, index);
        }

        #endregion

        #region IDictionary<TKey, TValue> Implementation

        public bool ContainsKey([DisallowNull] TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        // Suppress warning because ConcurrentDictionary from .NET does not have
        // nullable attributes.
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool TryGetValue([DisallowNull] TKey key, [MaybeNullWhen(false)] out TValue value)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            return _dictionary.TryGetValue(key, out value);
        }

        void IDictionary<TKey, TValue>.Add([DisallowNull] TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>) _dictionary).Add(key, value);
        }

        bool IDictionary<TKey, TValue>.Remove([DisallowNull] TKey key)
        {
            return ((IDictionary<TKey, TValue>) _dictionary).Remove(key);
        }

        #endregion

        #region IDictionary Impelementation

        void IDictionary.Add([DisallowNull] object key, object value)
        {
            CleanupExpiredObjects();
            ((IDictionary) _dictionary).Add(key, value);
        }

        bool IDictionary.Contains([DisallowNull] object key)
        {
            CleanupExpiredObjects();
            return ((IDictionary) _dictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            CleanupExpiredObjects();
            return ((IDictionary) _dictionary).GetEnumerator();
        }

        void IDictionary.Remove([DisallowNull] object key)
        {
            CleanupExpiredObjects();
            ((IDictionary) _dictionary).Remove(key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            CleanupExpiredObjects();
            return _dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Impelementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private Implementation Details

        private void CleanupExpiredObjects()
        {
            List<TKey> keysToRemove = new List<TKey>();
            foreach (var kvpCode in _dictionary)
            {
                TimeSpan timeDifference = DateTime.UtcNow - kvpCode.Value.CreationTime;
                if (timeDifference > _lifeTime)
                {
                    keysToRemove.Add(kvpCode.Key);
                }
            }

            keysToRemove.ForEach(key =>
            {
                _dictionary.TryRemove(key, out TValue forgetAboutMe);
            });
        }
        
        #endregion
    }
}
