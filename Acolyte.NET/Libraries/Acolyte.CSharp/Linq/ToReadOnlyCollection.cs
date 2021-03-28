using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region To Read-Only Dictionary

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> produces a key that is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector)
            where TKey : notnull
        {
            // Null checks for parameters are provided by "Enumerable.ToDictionary" method.
            return new ReadOnlyDictionary<TKey, TSource>(source.ToDictionary(keySelector));
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to a specified key selector function and key
        /// comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="EqualityComparer{TKey}" /> implementation for the
        /// <typeparamref name="TKey" /> type.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> produces a key that is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            // Null checks for parameters are provided by "Enumerable.ToDictionary" method.
            return new ReadOnlyDictionary<TKey, TSource>(
                source.ToDictionary(keySelector, comparer)
            );
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to specified key selector and element selector
        /// functions.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">
        /// A transform function to produce a result element value from each element.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains values of type
        /// <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// <paramref name="elementSelector" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> produces a key that is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector)
            where TKey : notnull
        {
            // Null checks for parameters are provided by "Enumerable.ToDictionary" method.
            return new ReadOnlyDictionary<TKey, TElement>(
                source.ToDictionary(keySelector, elementSelector)
            );
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> to a specified key selector function, a comparer, and an
        /// element selector function.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">
        /// A transform function to produce a result element value from each element.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="EqualityComparer{TKey}" /> implementation for the
        /// <typeparamref name="TKey" /> type.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains values of type
        /// <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// <paramref name="elementSelector" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> produces a key that is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            // Null checks for parameters are provided by "Enumerable.ToDictionary" method.
            return new ReadOnlyDictionary<TKey, TElement>(
                source.ToDictionary(keySelector, elementSelector, comparer)
            );
        }

        #endregion

        #region To Read-Only List

        /// <summary>
        /// Creates a <see cref="IReadOnlyList{T}" /> from an <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to create a <see cref="IReadOnlyList{T}" /> from.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static IReadOnlyList<TSource> ToReadOnlyList<TSource>(
            this IEnumerable<TSource> source)
        {
            // Null check for "source" parameter is provided by "Enumerable.ToList" method.
            return source
                .ToList()
                .AsReadOnly();
        }

        #endregion

        #region To Read-Only Collection

        /// <summary>
        /// Creates a <see cref="IReadOnlyCollection{T}" /> from an <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to create a <see cref="IReadOnlyCollection{T}" />
        /// from.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyCollection{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static IReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(
            this IEnumerable<TSource> source)
        {
            // Null check for "source" parameter is provided by "Enumerable.ToList" method.
            return source
                .ToList()
                .AsReadOnly();
        }

        #endregion

    }
}
