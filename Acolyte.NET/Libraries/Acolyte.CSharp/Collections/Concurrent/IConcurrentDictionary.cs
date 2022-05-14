using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Collections.Concurrent
{
    public interface IConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.AddOrUpdate(TKey, TValue, Func{TKey, TValue, TValue})" />
        TValue AddOrUpdate([DisallowNull] TKey key, TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory);

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.AddOrUpdate(TKey, Func{TKey, TValue}, Func{TKey, TValue, TValue})" />
        TValue AddOrUpdate([DisallowNull] TKey key, Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory);

#if NETSTANDARD2_1

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.AddOrUpdate{TArg}(TKey, Func{TKey, TArg, TValue}, Func{TKey, TValue, TArg, TValue}, TArg)" />
        TValue AddOrUpdate<TArg>([DisallowNull] TKey key, Func<TKey, TArg, TValue> addValueFactory,
            Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument);

#endif

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.GetOrAdd(TKey, TValue)" />
        TValue GetOrAdd([DisallowNull] TKey key, Func<TKey, TValue> valueFactory);

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.GetOrAdd(TKey, Func{TKey, TValue})" />
        TValue GetOrAdd([DisallowNull] TKey key, TValue value);

#if NETSTANDARD2_1

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.GetOrAdd{TArg}(TKey, Func{TKey, TArg, TValue}, TArg)" />
        TValue GetOrAdd<TArg>([DisallowNull] TKey key, Func<TKey, TArg, TValue> valueFactory,
            TArg factoryArgument);

#endif

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.TryAdd(TKey, TValue)" />
        bool TryAdd([DisallowNull] TKey key, TValue value);

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)" />
        new bool TryGetValue([DisallowNull] TKey key, [MaybeNullWhen(false)] out TValue value);

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.TryRemove(TKey, out TValue)" />
        bool TryRemove([DisallowNull] TKey key, [MaybeNullWhen(false)] out TValue value);

        /// <inheritdoc cref="ConcurrentDictionary{TKey, TValue}.TryUpdate(TKey, TValue, TValue)" />
        bool TryUpdate([DisallowNull] TKey key, TValue newValue, TValue comparisonValue);
    }
}
