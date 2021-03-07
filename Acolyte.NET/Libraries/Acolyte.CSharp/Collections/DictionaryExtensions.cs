using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;

namespace Acolyte.Collections
{
    /// <summary>
    /// Contains extension methods to simplify work with associative collections.
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Try Get Key

        public static bool TryGetKey<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, bool> predicate,
            [NotNullWhen(true)] out TKey? key)
            where TKey : notnull
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            predicate.ThrowIfNull(nameof(predicate));

            // Use plain old foreach loop because LINQ does not help us to detect if dictionary
            // contains required key or not (e.g. the key is suitable and equal to default).
            foreach (KeyValuePair<TKey, TValue> kvPair in dictionary)
            {
                if (predicate(kvPair))
                {
                    key = kvPair.Key;
                    return true;
                }
            }

            key = default;
            return false;
        }

        public static bool TryGetKey<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TKey, bool> predicate,
            [NotNullWhen(true)] out TKey? key)
            where TKey : notnull
        {
            return dictionary.TryGetKey(kvPair => predicate(kvPair.Key), out key);
        }

        public static bool TryGetKey<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TValue, bool> predicate,
            [NotNullWhen(true)] out TKey? key)
            where TKey : notnull
        {
            return dictionary.TryGetKey(kvPair => predicate(kvPair.Value), out key);
        }

        #endregion

        #region Try Get Value

        public static bool TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, bool> predicate,
            [MaybeNullWhen(false)] out TValue value)
            where TKey : notnull
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            predicate.ThrowIfNull(nameof(predicate));

            // Use plain old foreach loop because LINQ does not help us to detect if dictionary
            // contains required value or not (e.g. the value is suitable and equal to default).
            foreach (KeyValuePair<TKey, TValue> kvPair in dictionary)
            {
                if (predicate(kvPair))
                {
                    value = kvPair.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public static bool TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TKey, bool> predicate,
            [MaybeNullWhen(false)] out TValue value)
            where TKey : notnull
        {
            return dictionary.TryGetValue(kvPair => predicate(kvPair.Key), out value);
        }

        public static bool TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TValue, bool> predicate,
            [MaybeNullWhen(false)] out TValue value)
            where TKey : notnull
        {
            return dictionary.TryGetValue(kvPair => predicate(kvPair.Value), out value);
        }

        #endregion

        #region Get Or Create
        
        public static TValue GetOrCreate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            [DisallowNull] TKey key,
            Func<TValue> newValueGetter)
            where TKey : notnull
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            key.ThrowIfNullValue(nameof(key), assertOnPureValueTypes: false);
            newValueGetter.ThrowIfNull(nameof(newValueGetter));

            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = newValueGetter();
                dictionary[key] = value;
            }

            return value;
        }

        #endregion

        #region Add Or Update

        public static TValue AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            [DisallowNull] TKey key,
            Func<TValue> newValue,
            Func<TValue, TValue> updateValue)
            where TKey : notnull
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            key.ThrowIfNullValue(nameof(key), assertOnPureValueTypes: false);
            newValue.ThrowIfNull(nameof(newValue));
            updateValue.ThrowIfNull(nameof(updateValue));

            if (dictionary.TryGetValue(key, out TValue value))
            {
                value = updateValue(value);
                dictionary[key] = value;
            }
            else
            {
                value = newValue();
                dictionary.Add(key, value);
            }

            return value;
        }

        #endregion
    }
}
