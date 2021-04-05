using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns the last element of a sequence, or a specified default value if the sequence
        /// contains no elements.
        /// </summary>
        ///<typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to return the last element of or default.
        /// </param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if source is empty; otherwise, the last element in
        /// source.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<TSource> list)
            {
                int count = list.Count;
                if (count > 0) return list[count - 1];
            }
            else
            {
                using IEnumerator<TSource> enumerator = source.GetEnumerator();

                if (enumerator.MoveNext())
                {
                    TSource result;
                    do
                    {
                        result = enumerator.Current;
                    }
                    while (enumerator.MoveNext());

                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies the condition, or a specified 
        /// default value if the sequence contains no elements or no elements satisfy the condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to return the last element of or default.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements that satisfies the 
        /// condition.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if <paramref name="source" /> is empty or if no element
        /// passes the test specified by predicate; otherwise, the last element in
        /// <paramref name="source" /> that passes the test specified by
        /// <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));
            predicate.ThrowIfNull(nameof(predicate));

            TSource result = defaultValue;
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                }
            }

            return result;
        }
    }
}
