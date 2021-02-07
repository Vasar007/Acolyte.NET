using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Acolyte.Collections.Concurrent
{
    public class ConcurrentTwoWayDictionary<TKey, TValue> : TwoWayDictionary<TKey, TValue>
    {
        private readonly object _lock;

        protected readonly IEqualityComparer<TKey> KeyComparer;
        protected readonly IEqualityComparer<TValue> ValueComparer;
        protected readonly ConcurrentDictionary<TKey, TValue> FwdConcurrentDictionary;
        protected readonly ConcurrentDictionary<TValue, TKey> RevConcurrentDictionary;

        public ConcurrentTwoWayDictionary()
            : this(null, null)
        {
        }

        public ConcurrentTwoWayDictionary(
            IEqualityComparer<TKey>? keyComparer)
            : this(keyComparer, null)
        {
        }

        public ConcurrentTwoWayDictionary(
            IEqualityComparer<TValue>? valueComparer)
            : this(null, valueComparer)
        {
        }

        public ConcurrentTwoWayDictionary(
            IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
            : this(
                keyComparer ?? EqualityComparer<TKey>.Default,
                valueComparer ?? EqualityComparer<TValue>.Default,
                true)
        {
        }

        private ConcurrentTwoWayDictionary(
            IEqualityComparer<TKey> keyComparer,
            IEqualityComparer<TValue> valueComparer,
            bool _)
            : base(new ConcurrentDictionary<TKey, TValue>(keyComparer),
                   new ConcurrentDictionary<TValue, TKey>(valueComparer))
        {
            KeyComparer = keyComparer;
            ValueComparer = valueComparer;
            RevConcurrentDictionary = (ConcurrentDictionary<TValue, TKey>) RevDictionary;
            FwdConcurrentDictionary = (ConcurrentDictionary<TKey, TValue>) FwdDictionary;

            _lock = new object();
        }

        public TValue GetOrAddValue(TKey key, Func<TKey, TValue> valueFactory,
            TwoWayDictionaryAddFlags flags)
        {
            return GetOrAdd(
                key,
                valueFactory,
                flags,
                ValueComparer,
                FwdConcurrentDictionary,
                RevConcurrentDictionary,
                HandleKeyWithSameValue
            );
        }

        public TKey GetOrAddKey(TValue value, Func<TValue, TKey> keyFactory,
            TwoWayDictionaryAddFlags flags)
        {
            return GetOrAdd(
                value,
                keyFactory,
                flags,
                KeyComparer,
                RevConcurrentDictionary,
                FwdConcurrentDictionary,
                HandleValueWithSameKey
            );
        }

        private T2 GetOrAdd<T1, T2>(
            T1 source,
            Func<T1, T2> targetFactory,
            TwoWayDictionaryAddFlags flags,
            IEqualityComparer<T2> targetComparer,
            ConcurrentDictionary<T1, T2> fwdConcurrentDictionary,
            ConcurrentDictionary<T2, T1> revConcurrentDictionary,
            HandleKeyWithSameItemDelegate<T1, T2> errorHandler)
        {
            if (fwdConcurrentDictionary.TryGetValue(source, out T2 newTarget))
            {
                return newTarget;
            }

            bool isBlocking = flags.HasFlag(TwoWayDictionaryAddFlags.Blocking);
            if (isBlocking)
            {
                Monitor.Enter(_lock);
            }

            try
            {
                newTarget = targetFactory(source);

                if (newTarget is IDisposable)
                {
                    if (!isBlocking)
                    {
                        throw new NotSupportedException(
                            "Use blocking ConcurrentTwoWayDictionary to store disposable items."
                        );
                    }
                }

                lock (_lock)
                {
                    T2 addededValue = fwdConcurrentDictionary.GetOrAdd(source, newTarget);
                    if (!targetComparer.Equals(addededValue, newTarget))
                    {
                        return addededValue;
                    }

                    if (!revConcurrentDictionary.TryAdd(newTarget, source))
                    {
                        errorHandler(source, flags, newTarget);
                    }

                    return newTarget;
                }
            }
            finally
            {
                if (isBlocking)
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        private delegate void HandleKeyWithSameItemDelegate<T1, T2>(T1 newSource,
            TwoWayDictionaryAddFlags flags, T2 value);

        private void HandleKeyWithSameValue(TKey newKey, TwoWayDictionaryAddFlags flags,
            TValue value)
        {
            TKey existingKey = RevConcurrentDictionary[value];

            string message =
                TwoWayDictionaryExceptions.KeyWithSameValue(existingKey, newKey, value);
            if (flags.HasFlag(TwoWayDictionaryAddFlags.Safe))
            {
                Trace.TraceWarning(message);
            }
            else
            {
                throw new ArgumentException(message, nameof(value));
            }
        }

        private void HandleValueWithSameKey(TValue newValue, TwoWayDictionaryAddFlags flags,
            TKey key)
        {
            TValue existingValue = FwdConcurrentDictionary[key];

            string message =
                TwoWayDictionaryExceptions.ItemWithSameKey(existingValue, key, newValue);
            if (flags.HasFlag(TwoWayDictionaryAddFlags.Safe))
            {
                Trace.TraceWarning(message);
            }
            else
            {
                throw new ArgumentException(message, nameof(key));
            }
        }

        public override void Add(TKey key, TValue value)
        {
            GetOrAddValue(key, k => value, TwoWayDictionaryAddFlags.None);
        }

        public override void RemoveByKey(TKey key)
        {
            lock (_lock)
            {
                if (FwdConcurrentDictionary.TryRemove(key, out TValue value))
                {
                    RevConcurrentDictionary.TryRemove(value, out _);
                }
            }
        }

        public override void RemoveByValue(TValue value)
        {
            lock (_lock)
            {
                if (RevConcurrentDictionary.TryRemove(value, out TKey key))
                {
                    FwdConcurrentDictionary.TryRemove(key, out _);
                }
            }
        }
    }
}
