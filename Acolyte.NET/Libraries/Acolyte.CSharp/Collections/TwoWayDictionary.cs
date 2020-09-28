using System;
using System.Collections;
using System.Collections.Generic;

namespace Acolyte.Collections
{
    public sealed class TwoWayDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly IDictionary<TKey, TValue> _fwdDictionary;
        private readonly IDictionary<TValue, TKey> _revDictionary;

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
            : this(new Dictionary<TKey, TValue>(keyComparer), new Dictionary<TValue, TKey>(valueComparer))
        {
        }

        private TwoWayDictionary(
            IDictionary<TKey, TValue> fwdDictionary,
            IDictionary<TValue, TKey> revDictionary)
        {
            _fwdDictionary = fwdDictionary;
            _revDictionary = revDictionary;
        }

        public TValue this[TKey key] { get => _fwdDictionary[key]; }
        public TKey this[TValue val] { get => _revDictionary[val]; }

        public IReadOnlyList<TKey> GetKeys() => new List<TKey>(_fwdDictionary.Keys);
        public IReadOnlyList<TValue> GetValues() => new List<TValue>(_fwdDictionary.Values);

        public void Add(TKey key, TValue value)
        {
            try
            {
                _fwdDictionary.Add(key, value);
            }
            catch (ArgumentException ex)
            {
                if (_revDictionary.ContainsKey(value))
                    throw new ArgumentException(TwoWayDictionaryExceptions.PairAlreadyAdded(key, value), ex);

                if (!_fwdDictionary.TryGetValue(key, out TValue existing))
                    throw;

                throw new InvalidOperationException(TwoWayDictionaryExceptions.ItemWithSameKey(existing, key, value), ex);
            }

            try
            {
                _revDictionary.Add(value, key);
            }
            catch (ArgumentException ex)
            {
                _fwdDictionary.Remove(key);

                if (!_revDictionary.TryGetValue(value, out TKey existing))
                    throw;

                throw new InvalidOperationException(TwoWayDictionaryExceptions.KeyWithSameValue(existing, key, value), ex);
            }
        }

        public void RemoveByKey(TKey key)
        {
            if (TryGetValue(key, out TValue value))
            {
                _fwdDictionary.Remove(key);
                _revDictionary.Remove(value);
            }
        }

        public void RemoveByValue(TValue value)
        {
            if (TryGetKey(value, out TKey key))
            {
                _fwdDictionary.Remove(key);
                _revDictionary.Remove(value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _fwdDictionary.TryGetValue(key, out value);
        }

        public bool TryGetKey(TValue value, out TKey key)
        {
            return _revDictionary.TryGetValue(value, out key);
        }

        public bool ContainsValue(TValue value)
        {
            return _revDictionary.ContainsKey(value);
        }

        public bool ContainsKey(TKey key)
        {
            return _fwdDictionary.ContainsKey(key);
        }

        public void Clear()
        {
            _fwdDictionary.Clear();
            _revDictionary.Clear();
        }

        public IEnumerable<TValue> Values => _fwdDictionary.Values;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _fwdDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
