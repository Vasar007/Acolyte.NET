using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Collections.Concurrent
{
    /// <summary>
    /// Extends the standard <see cref="ConcurrentDictionary{TKey, TValue}" /> class with time-based
    /// logic. This collection cleanup expired objects when calling any method, i.g. expired objects
    /// will be removed on next collection access.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class TimeBasedConcurrentDictionary<TKey, TValue> :
        ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerable,
        IDictionary<TKey, TValue>,
        IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
        IReadOnlyDictionary<TKey, TValue>,
        IConcurrentDictionary<TKey, TValue>,
        ICollection,
        IDictionary
        where TKey : notnull
        where TValue : IHaveCreationTime
    {
        #region Private Fields

        private readonly TimeSpan _lifeTime;

        private readonly ConcurrentDictionary<TKey, TValue> _dictionary;

        #endregion

        #region Public Properties

        public TimeSpan LifeTime
        {
            get
            {
                CleanupExpiredObjects();
                return _lifeTime;
            }
        }

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

        #region Explicit Properties

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).IsReadOnly;

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

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary()" />
        /// <param name="lifeTime">
        /// The specified life time of the objects stored in dictionary.
        /// </param>
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime)
        {
            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEnumerable{KeyValuePair{TKey, TValue}})" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection);
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEqualityComparer{TKey})" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            IEqualityComparer<TKey> comparer)
        {
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(comparer);
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEnumerable{KeyValuePair{TKey, TValue}}, IEqualityComparer{TKey})" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            IEnumerable<KeyValuePair<TKey, TValue>> collection,
            IEqualityComparer<TKey> comparer)
        {
            collection.ThrowIfNull(nameof(collection));
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection, comparer);
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, int)" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            int concurrencyLevel,
            int capacity)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            capacity.ThrowIfValueIsOutOfRange(nameof(capacity), 0, int.MaxValue);

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity);
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, IEnumerable{KeyValuePair{TKey, TValue}}, IEqualityComparer{TKey})" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            int concurrencyLevel,
            IEnumerable<KeyValuePair<TKey, TValue>> collection,
            IEqualityComparer<TKey> comparer)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            collection.ThrowIfNull(nameof(collection));
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, collection, comparer);
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, int, IEqualityComparer{TKey})" />
        /// <inheritdoc cref="TimeBasedConcurrentDictionary{TKey, TValue}.TimeBasedConcurrentDictionary(TimeSpan)" path="//param" />
        public TimeBasedConcurrentDictionary(
            TimeSpan lifeTime,
            int concurrencyLevel,
            int capacity,
            IEqualityComparer<TKey> comparer)
        {
            concurrencyLevel.ThrowIfValueIsOutOfRange(nameof(concurrencyLevel), 1, int.MaxValue);
            capacity.ThrowIfValueIsOutOfRange(nameof(capacity), 0, int.MaxValue);
            comparer.ThrowIfNull(nameof(comparer));

            _lifeTime = lifeTime;
            _dictionary = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity, comparer);
        }

        #endregion

        #region IConcurrentDictionary<TKey, TValue> Implementation

        /// <inheritdoc />
        public TValue AddOrUpdate([DisallowNull] TKey key, TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            CleanupExpiredObjects();
            return _dictionary.AddOrUpdate(key, addValue, updateValueFactory);
        }

        /// <inheritdoc />
        public TValue AddOrUpdate([DisallowNull] TKey key, Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            CleanupExpiredObjects();
            return _dictionary.AddOrUpdate(key, addValueFactory, updateValueFactory);
        }

#if NETSTANDARD2_1

        /// <inheritdoc />
        public TValue AddOrUpdate<TArg>([DisallowNull] TKey key,
            Func<TKey, TArg, TValue> addValueFactory,
            Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
        {
            CleanupExpiredObjects();
            return _dictionary.AddOrUpdate(
                key, addValueFactory, updateValueFactory, factoryArgument
            );
        }

#endif

        /// <inheritdoc />
        public TValue GetOrAdd([DisallowNull] TKey key, TValue value)
        {
            CleanupExpiredObjects();
            return _dictionary.GetOrAdd(key, value);
        }

        /// <inheritdoc />
        public TValue GetOrAdd([DisallowNull] TKey key, Func<TKey, TValue> valueFactory)
        {
            CleanupExpiredObjects();
            return _dictionary.GetOrAdd(key, valueFactory);
        }

#if NETSTANDARD2_1

        /// <inheritdoc />
        public TValue GetOrAdd<TArg>([DisallowNull] TKey key, Func<TKey, TArg, TValue> valueFactory,
            TArg factoryArgument)
        {
            CleanupExpiredObjects();
            return _dictionary.GetOrAdd(key, valueFactory, factoryArgument);
        }

#endif

        /// <inheritdoc />
        public bool TryAdd([DisallowNull] TKey key, TValue value)
        {
            CleanupExpiredObjects();
            return _dictionary.TryAdd(key, value);
        }

        /// <inheritdoc />
        public bool TryRemove([DisallowNull] TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            CleanupExpiredObjects();
            return _dictionary.TryRemove(key, out value);
        }

        /// <inheritdoc />
        public bool TryUpdate([DisallowNull] TKey key, TValue newValue, TValue comparisonValue)
        {
            CleanupExpiredObjects();
            return _dictionary.TryUpdate(key, newValue, comparisonValue);
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

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array,
            int arrayIndex)
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
            CleanupExpiredObjects();
            ((ICollection) _dictionary).CopyTo(array, index);
        }

        #endregion

        #region IDictionary<TKey, TValue> Implementation

        /// <inheritdoc />
        public bool ContainsKey([DisallowNull] TKey key)
        {
            CleanupExpiredObjects();
            return _dictionary.ContainsKey(key);
        }

        // Suppress warning because IDictionary from .NET does not have
        // nullable attributes.
        /// <inheritdoc />
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool TryGetValue([DisallowNull] TKey key, [MaybeNullWhen(false)] out TValue value)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            CleanupExpiredObjects();
            return _dictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        void IDictionary<TKey, TValue>.Add([DisallowNull] TKey key, TValue value)
        {
            CleanupExpiredObjects();
            ((IDictionary<TKey, TValue>) _dictionary).Add(key, value);
        }

        /// <inheritdoc />
        bool IDictionary<TKey, TValue>.Remove([DisallowNull] TKey key)
        {
            CleanupExpiredObjects();
            return ((IDictionary<TKey, TValue>) _dictionary).Remove(key);
        }

        #endregion

        #region IDictionary Implementation

        /// <inheritdoc />
        void IDictionary.Add([DisallowNull] object key, object value)
        {
            CleanupExpiredObjects();
            ((IDictionary) _dictionary).Add(key, value);
        }

        /// <inheritdoc />
        bool IDictionary.Contains([DisallowNull] object key)
        {
            CleanupExpiredObjects();
            return ((IDictionary) _dictionary).Contains(key);
        }

        /// <inheritdoc />
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            CleanupExpiredObjects();
            return ((IDictionary) _dictionary).GetEnumerator();
        }

        /// <inheritdoc />
        void IDictionary.Remove([DisallowNull] object key)
        {
            CleanupExpiredObjects();
            ((IDictionary) _dictionary).Remove(key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Implementation

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            CleanupExpiredObjects();
            return _dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Implementation

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Protected Implementation Details

        protected void CleanupExpiredObjects()
        {
            var keysToRemove = new List<TKey>();
            foreach (var kvp in _dictionary)
            {
                TimeSpan timeDifference = DateTime.UtcNow - kvp.Value.CreationTimeUtc;
                if (timeDifference > _lifeTime)
                {
                    keysToRemove.Add(kvp.Key);
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
