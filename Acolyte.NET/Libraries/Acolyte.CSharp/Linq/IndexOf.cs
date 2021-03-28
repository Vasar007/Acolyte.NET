using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Searches for an element that satisfies the condition and returns the zero-based index of
        /// the first occurrence within the entire sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that satisfies 
        /// <paramref name="predicate" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            predicate.ThrowIfNull(nameof(predicate));

            int foundIndex = source
                .Select((value, index) => (value, index))
                .Where(x => predicate(x.value))
                .Select(x => x.index)
                .FirstOrDefault(Constants.NotFoundIndex);

            return foundIndex;
        }

        /// <summary>
        /// Searches for an element that equals to the specified element and returns the zero-based
        /// index of the first occurrence within the entire sequence. The values are compared by
        /// using a specified <paramref name="comparer" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <param name="value">Value to find index of.</param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{TSource}" /> implementation to use when
        /// comparing values, or <see langword="null" /> to use the default
        /// <see cref="EqualityComparer{TSource}" /> implementation for the
        /// <typeparamref name="TSource" /> type.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that equals to the 
        /// <paramref name="value" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value,
            IEqualityComparer<TSource>? comparer)
        {
            comparer ??= EqualityComparer<TSource>.Default;

            return source.IndexOf(item => comparer.Equals(item, value));
        }

        /// <summary>
        /// Searches for an element that equals to the specified element and returns the zero-based
        /// index of the first occurrence within the entire sequence. The values are compared by
        /// using a default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <param name="value">Value to find index of.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that equals to the 
        /// <paramref name="value" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return source.IndexOf(value, EqualityComparer<TSource>.Default);
        }
    }
}
