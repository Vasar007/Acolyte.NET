using System;
using System.Collections;
using System.Collections.Generic;

namespace Acolyte.Collections
{
    public class TwoWayDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerable
    {
        protected readonly IDictionary<TKey, TValue> FwdDictionary;
        protected readonly IDictionary<TValue, TKey> RevDictionary;

        public TwoWayDictionary()
            : this((IEqualityComparer<TKey>?) null, (IEqualityComparer<TValue>?) null)
        {
        }

        public TwoWayDictionary(
            IEqualityComparer<TKey> keyComparer)
            : this(keyComparer, null)
        {
        }

        public TwoWayDictionary(
            IEqualityComparer<TValue> valueComparer)
            : this(null, valueComparer)
        {
        }

        public TwoWayDictionary(
            IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
            : this(new Dictionary<TKey, TValue>(keyComparer),
                   new Dictionary<TValue, TKey>(valueComparer))
        {
        }

        protected TwoWayDictionary(
            IDictionary<TKey, TValue> fwdDictionary,
            IDictionary<TValue, TKey> revDictionary)
        {
            FwdDictionary = fwdDictionary;
            RevDictionary = revDictionary;
        }

        public TValue this[TKey key] => FwdDictionary[key];
        public TKey this[TValue val] => RevDictionary[val];

        public IReadOnlyList<TKey> GetKeys()
        {
            return new List<TKey>(FwdDictionary.Keys);
        }

        public IReadOnlyList<TValue> GetValues()
        {
            return new List<TValue>(FwdDictionary.Values);
        }

        public virtual void Add(TKey key, TValue value)
        {
            try
            {
                FwdDictionary.Add(key, value);
            }
            catch (ArgumentException ex)
            {
                if (RevDictionary.ContainsKey(value))
                {
                    throw new ArgumentException(
                        TwoWayDictionaryExceptions.PairAlreadyAdded(key, value), ex
                    );
                }

                if (!FwdDictionary.TryGetValue(key, out TValue existing))
                {
                    throw;
                }

                throw new InvalidOperationException(
                    TwoWayDictionaryExceptions.ItemWithSameKey(existing, key, value), ex
                );
            }

            try
            {
                RevDictionary.Add(value, key);
            }
            catch (ArgumentException ex)
            {
                FwdDictionary.Remove(key);

                if (!RevDictionary.TryGetValue(value, out TKey existing))
                {
                    throw;
                }

                throw new InvalidOperationException(
                    TwoWayDictionaryExceptions.KeyWithSameValue(existing, key, value), ex
                );
            }
        }

        public virtual void RemoveByKey(TKey key)
        {
            if (TryGetValue(key, out TValue value))
            {
                FwdDictionary.Remove(key);
                RevDictionary.Remove(value);
            }
        }

        public virtual void RemoveByValue(TValue value)
        {
            if (TryGetKey(value, out TKey key))
            {
                FwdDictionary.Remove(key);
                RevDictionary.Remove(value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return FwdDictionary.TryGetValue(key, out value);
        }

        public bool TryGetKey(TValue value, out TKey key)
        {
            return RevDictionary.TryGetValue(value, out key);
        }

        public bool ContainsValue(TValue value)
        {
            return RevDictionary.ContainsKey(value);
        }

        public bool ContainsKey(TKey key)
        {
            return FwdDictionary.ContainsKey(key);
        }

        public virtual void Clear()
        {
            FwdDictionary.Clear();
            RevDictionary.Clear();
        }

        public IEnumerable<TValue> Values => FwdDictionary.Values;

        #region IEnumerator<KeyValuePair<TKey, TValue>> Implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return FwdDictionary.GetEnumerator();
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
