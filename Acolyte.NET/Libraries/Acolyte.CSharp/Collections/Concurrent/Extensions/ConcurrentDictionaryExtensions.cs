using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Basic.Disposal;

namespace Acolyte.Collections.Concurrent
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue? TryGet<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : class?
        {
            dictionary.ThrowIfNull(nameof(dictionary));

            return dictionary.TryGetValue(key, out TValue value)
                ? value
                : null;
        }

        public static bool TryAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary,
            KeyValuePair<TKey, TValue> pair)
        {
            dictionary.ThrowIfNull(nameof(dictionary));

            return dictionary.TryAdd(pair.Key, pair.Value);
        }

        public static bool TryRemove<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> pair,
            [MaybeNullWhen(false)] out TValue value)
        {
            dictionary.ThrowIfNull(nameof(dictionary));

            return dictionary.TryRemove(pair.Key, out value);
        }

        public static void DisposeAndClearSafe<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue>? dictionary)
            where TValue : IDisposable
        {
            if (dictionary is null) return;

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                dictionary.TryRemove(pair, out _);
                pair.Value.DisposeSafe();
            }
        }

        public static bool IsAddedNotGotten<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue?> addValueFactory,
            [MaybeNullWhen(false)] out TValue value)
            where TValue : class?
        {
            return dictionary.IsAddedNotGotten(
                key: key,
                addValueFactory: addValueFactory,
                allowNullValues: true,
                value: out value
            );
        }

        public static bool IsAddedNotGotten<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue?> addValueFactory,
            bool allowNullValues,
            [MaybeNullWhen(false)] out TValue value)
            where TValue : class?
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            addValueFactory.ThrowIfNull(nameof(addValueFactory));

            TValue? createdValue = default;
            Func<TKey, TValue?> valueFactory = k =>
            {
                createdValue = addValueFactory(key);
                if (createdValue is null && !allowNullValues)
                {
                    throw new NotSupportedException($"A new value cannot be null. Key: {key}");
                }

                return createdValue;
            };

            // ConcurrentDictionary allows to add null values.
            value = dictionary.GetOrAdd(key, valueFactory!);

            return ReferenceEquals(value, createdValue);
        }

        public static IEnumerable<TKey> EnumerateKeys<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(kvp => kvp.Key);
        }

        public static IEnumerable<TValue> EnumerateValues<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(kvp => kvp.Value);
        }

        public static TValue GetOrAdd<TKey, TArg, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TArg arg,
            Func<TKey, TArg, TValue> valueFactory)
        {
            dictionary.ThrowIfNull(nameof(dictionary));

            var factory = new AddFactory<TKey, TArg, TValue>(valueFactory, arg);
            return dictionary.GetOrAdd(key, factory.Produce);
        }

        public static TValue GetOrAdd<TKey, TArg1, TArg2, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TArg1 arg1, TArg2 arg2,
            Func<TKey, TArg1, TArg2, TValue> valueFactory)
        {
            dictionary.ThrowIfNull(nameof(dictionary));

            var factory = new AddFactory<TKey, TArg1, TArg2, TValue>(valueFactory, arg1, arg2);
            return dictionary.GetOrAdd(key, factory.Produce);
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
