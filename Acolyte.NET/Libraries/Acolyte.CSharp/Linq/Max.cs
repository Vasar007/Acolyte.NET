using System;
using System.Collections.Generic;
using Acolyte.Collections;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region Max For Generic Types With Comparer

        /// <summary>
        /// Returns the maximum value in a generic sequence with provided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum value of.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IComparer{TSource}" /> implementation to use when
        /// comparing values, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TSource}" /> implementation for the
        /// <typeparamref name="TSource" /> type.
        /// </param>
        /// <returns>
        /// The maximum value in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        public static TSource? Max<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            return source.Min(new InverseComparer<TSource>(comparer));
        }

        #endregion

        #region Max By Key

        /// <summary>
        /// Retrieves maximum element by key in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <param name="comparer">
        /// The <see cref="IComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TKey}" /> implementation for the
        /// <typeparamref name="TKey" /> type.
        /// </param>
        /// <returns>
        /// The maximum by key element value in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        {
            return source.MinBy(keySelector, new InverseComparer<TKey>(comparer));
        }

        /// <summary>
        /// Retrieves maximum element by key in a generic sequence with default key comparer of type
        /// <typeparamref name="TKey" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <returns>
        /// The maximum by key value in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TKey}" /> interface.
        /// </exception>
        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.MinBy(keySelector, new InverseComparer<TKey>(Comparer<TKey>.Default));
        }

        #endregion
    }
}
