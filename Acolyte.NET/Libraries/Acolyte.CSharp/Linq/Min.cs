using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region Min For Generic Types With Comparer

        /// <summary>
        /// Returns the minimum value in a generic sequence with provided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum value of.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IComparer{TSource}" /> implementation to use when
        /// comparing values, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TSource}" /> implementation for the
        /// <typeparamref name="TSource" /> type.
        /// </param>
        /// <returns>
        /// The minimum value in the sequence.
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
        public static TSource? Min<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            source.ThrowIfNull(nameof(source));
            comparer ??= Comparer<TSource>.Default;

            TSource? minValue = default;
            if (minValue is null)
            {
                foreach (TSource x in source)
                {
                    if (x is not null && (minValue is null || comparer.Compare(x, minValue) < 0)) minValue = x;
                }

                return minValue;
            }
            else
            {
                bool hasValue = false;
                foreach (TSource x in source)
                {
                    if (hasValue)
                    {
                        if (comparer.Compare(x, minValue) < 0) minValue = x;
                    }
                    else
                    {
                        minValue = x;
                        hasValue = true;
                    }
                }

                if (hasValue) return minValue;

                throw Error.NoElements();
            }
        }

        #endregion

        #region Min By Key

        /// <summary>
        /// Retrieves minimum element by key in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <param name="comparer">
        /// The <see cref="IComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TKey}" /> implementation for the
        /// <typeparamref name="TKey" /> type.
        /// </param>
        /// <returns>
        /// The minimum by key element in the sequence.
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
        public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        {
            source.ThrowIfNull(nameof(source));
            keySelector.ThrowIfNull(nameof(keySelector));
            comparer ??= Comparer<TKey>.Default;

            TSource? minValue = default;

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (minValue is null) return minValue;

                    throw Error.NoElements();
                }

                minValue = enumerator.Current;
                TKey minElementKey = keySelector(minValue);

                while (enumerator.MoveNext())
                {
                    TSource element = enumerator.Current;
                    TKey elementKey = keySelector(element);

                    if (comparer.Compare(elementKey, minElementKey) < 0)
                    {
                        minValue = element;
                        minElementKey = elementKey;
                    }
                }
            }

            return minValue;
        }

        /// <summary>
        /// Retrieves minimum element by key in a generic sequence with default key comparer of type
        /// <typeparamref name="TKey" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <returns>
        /// The minimum by key element in the sequence.
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
        public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.MinBy(keySelector, Comparer<TKey>.Default);
        }

        #endregion
    }
}
