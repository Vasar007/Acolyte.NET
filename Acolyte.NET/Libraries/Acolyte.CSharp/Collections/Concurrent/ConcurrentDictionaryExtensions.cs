using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Acolyte.Common;

namespace Acolyte.Collections.Concurrent
{
    public static class ConcurrentDictionaryExtensions
    {
        [return: MaybeNull]
        public static TValue TryGet<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dic,
            TKey key)
            where TValue : class
        {
            return dic.TryGetValue(key, out TValue value) ? value : null;
        }

        public static void Add<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key,
            TValue value)
        {
            ((IDictionary<TKey, TValue>) self).Add(key, value);
        }

        public static void Add<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self,
            KeyValuePair<TKey, TValue> pair)
        {
            ((IDictionary<TKey, TValue>) self).Add(pair);
        }

        public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self,
            TKey key)
        {
            return ((IDictionary<TKey, TValue>) self).Remove(key);
        }

        public static void Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self,
            KeyValuePair<TKey, TValue> pair)
        {
            ((IDictionary<TKey, TValue>) self).Remove(pair);
        }

        public static void DisposeAndClearSafe<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> self)
            where TValue : IDisposable
        {
            if (self is null)
            {
                return;
            }

            foreach (KeyValuePair<TKey, TValue> pair in self)
            {
                self.Remove(pair);
                pair.Value.DisposeSafe();
            }
        }

        public static bool IsAddedNotGotten<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> self,
            TKey key,
            Func<TKey, TValue> addValueFactory,
            [MaybeNullWhen(false)] out TValue value)
            where TValue : class
        {
            TValue? createdValue = default;
            value = self.GetOrAdd(key, (k) =>
            {
                createdValue = addValueFactory(key);
                if (createdValue is null)
                {
                    throw new NotSupportedException($"A new value cannot be null. Key: {key}");
                }

                return createdValue;
            });

            return ReferenceEquals(value, createdValue);
        }

        public static IEnumerable<TKey> EnumerateKeys<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> self)
        {
            return self.Select(kvp => kvp.Key);
        }

        public static IEnumerable<TValue> EnumerateValues<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> self)
        {
            return self.Select(kvp => kvp.Value);
        }

        public static TValue GetOrAdd<TKey, TArg, TValue>(
            this ConcurrentDictionary<TKey, TValue> self, TKey key, TArg arg,
            Func<TKey, TArg, TValue> valueFactory)
        {
            var factory = new AddFactory<TKey, TArg, TValue>(valueFactory, arg);
            return self.GetOrAdd(key, factory.Produce);
        }

        public static TValue GetOrAdd<TKey, TArg1, TArg2, TValue>(
            this ConcurrentDictionary<TKey, TValue> self, TKey key, TArg1 arg1, TArg2 arg2,
            Func<TKey, TArg1, TArg2, TValue> valueFactory)
        {
            var factory = new AddFactory<TKey, TArg1, TArg2, TValue>(valueFactory, arg1, arg2);
            return self.GetOrAdd(key, factory.Produce);
        }

        private sealed class AddFactory<TKey, TArg, TValue>
        {
            private readonly Func<TKey, TArg, TValue> _valueFactory;
            private readonly TArg _arg;

            public AddFactory(Func<TKey, TArg, TValue> valueFactory, TArg arg)
            {
                _valueFactory = valueFactory;
                _arg = arg;
            }

            public TValue Produce(TKey key)
            {
                return _valueFactory(key, _arg);
            }
        }

        private sealed class AddFactory<TKey, TArg1, TArg2, TValue>
        {
            private readonly Func<TKey, TArg1, TArg2, TValue> _valueFactory;
            private readonly TArg1 _arg1;
            private readonly TArg2 _arg2;

            public AddFactory(Func<TKey, TArg1, TArg2, TValue> valueFactory, TArg1 arg1, TArg2 arg2)
            {
                _valueFactory = valueFactory;
                _arg1 = arg1;
                _arg2 = arg2;
            }

            public TValue Produce(TKey key)
            {
                return _valueFactory(key, _arg1, _arg2);
            }
        }
    }
}
