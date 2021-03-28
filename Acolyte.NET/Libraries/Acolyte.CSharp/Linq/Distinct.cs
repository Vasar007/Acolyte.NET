using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns distinct by key elements from a sequence by using a specified
        /// <see cref="IEqualityComparer{TKey}" /> to compare keys.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="keyComparer">
        /// The <see cref="IEqualityComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="EqualityComparer{T}" /> implementation for the <typeparamref name="TKey" />
        /// type.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}" /> that contains distinct by key elements from
        /// the <paramref name="source" /> sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey>? keyComparer)
        {
            source.ThrowIfNull(nameof(source));
            keySelector.ThrowIfNull(nameof(keySelector));

            var seenKeys = new HashSet<TKey>(keyComparer);
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element))) yield return element;
            }
        }

        /// <summary>
        /// Returns distinct by key elements from a sequence by using the default equality comparer
        /// to compare values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}" /> that contains distinct by key elements from
        /// the <paramref name="source" /> sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, EqualityComparer<TKey>.Default);
        }
    }
}
