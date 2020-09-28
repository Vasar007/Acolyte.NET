using System;

namespace Acolyte.Collections
{
    internal static class TwoWayDictionaryExceptions
    {
        public static string ItemWithSameKey<TKey, TValue>(TValue existing, TKey key, TValue value)
        {
            return $"An item [{existing}] with the same key [{key}] has already been added. New item: [{value}]";
        }

        public static string KeyWithSameValue<TKey, TValue>(TKey existing, TKey key, TValue value)
        {
            return $"A key [{existing}] with the same value [{value}] has already been added. New key: [{key}]";
        }

        public static string PairAlreadyAdded<TKey, TValue>(TKey key, TValue value)
        {
            return $"A pair ([{key}], [{value}]) has already been added.";
        }
    }
}
