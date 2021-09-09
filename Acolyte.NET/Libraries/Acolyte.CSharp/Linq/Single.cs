using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of a sequence, or a specified default value if the sequence
        /// contains no elements; this method throws an exception if sequence contains more than
        /// one element.
        /// </summary>
        ///<typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to return the single element from or default.
        /// </param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements.
        /// </param>
        /// <returns>
        /// The single element of the input sequence, or <paramref name="defaultValue" /> if the
        /// sequence contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The input sequence contains more than one element.
        /// </exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<TSource> list)
            {
                switch (list.Count)
                {
                    case 0: return defaultValue;
                    case 1: return list[0];
                }
            }
            else
            {
                using IEnumerator<TSource> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext()) return defaultValue;
                TSource result = enumerator.Current;
                if (!enumerator.MoveNext()) return result;
            }

            throw Error.MoreThanOneElement();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or
        /// a default value if no such element exists; this method throws an exception if
        /// more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to return the single element from or default.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements that satisfies the 
        /// condition.
        /// </param>
        /// <returns>
        /// The single element of the input sequence that satisfies the condition, or
        /// <paramref name="defaultValue" /> if no such element is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// More than one element satisfies the condition in predicate.
        /// </exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));
            predicate.ThrowIfNull(nameof(predicate));

            TSource result = defaultValue;
            int count = 0;
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                    ++count;

                    // If there are more than one element satisfies the condition in predicate,
                    // skip the last part of sequence.
                    if (count > 1) break;
                }
            }

            return count switch
            {
                0 or 1 => result,
                _ => throw Error.MoreThanOneMatch(),
            };
        }
    }
}
