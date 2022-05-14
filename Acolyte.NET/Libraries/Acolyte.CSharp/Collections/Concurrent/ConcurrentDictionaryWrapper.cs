using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Acolyte.Collections.Concurrent
{
    public class ConcurrentDictionaryWrapper<TKey, TValue> :
        ConcurrentDictionary<TKey, TValue>,
        IConcurrentDictionary<TKey, TValue>
        where TKey : notnull
    {
        #region Constructors

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary()" />
        public ConcurrentDictionaryWrapper()
            : base()
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEnumerable{KeyValuePair{TKey, TValue}})" />
        public ConcurrentDictionaryWrapper(
            IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection)
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEqualityComparer{TKey})" />
        public ConcurrentDictionaryWrapper(
            IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(IEnumerable{KeyValuePair{TKey, TValue}}, IEqualityComparer{TKey})" />
        public ConcurrentDictionaryWrapper(
            IEnumerable<KeyValuePair<TKey, TValue>> collection,
            IEqualityComparer<TKey> comparer)
            : base(collection, comparer)
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, int)" />
        public ConcurrentDictionaryWrapper(
            int concurrencyLevel,
            int capacity)
            : base(concurrencyLevel, capacity)
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, IEnumerable{KeyValuePair{TKey, TValue}}, IEqualityComparer{TKey})" />
        public ConcurrentDictionaryWrapper(
            int concurrencyLevel,
            IEnumerable<KeyValuePair<TKey, TValue>> collection,
            IEqualityComparer<TKey> comparer)
            : base(concurrencyLevel, collection, comparer)
        {
        }

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.ConcurrentDictionary(int, int, IEqualityComparer{TKey})" />
        public ConcurrentDictionaryWrapper(
            int concurrencyLevel,
            int capacity,
            IEqualityComparer<TKey> comparer)
            : base(concurrencyLevel, capacity, comparer)
        {
        }

        #endregion
    }
}
