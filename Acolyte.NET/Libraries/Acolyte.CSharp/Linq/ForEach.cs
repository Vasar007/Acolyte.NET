using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Action{T}" /> delegate to perform on each element of the
        /// <paramref name="source" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            ForEachEnumerator(source, (item, index) => action(item));
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> and
        /// item index.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Action{T}" /> delegate to perform on each element of the
        /// <paramref name="source" /> and item index.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource, int> action)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            ForEachEnumerator(source, action);
        }

        private static void ForEachEnumerator<TSource>(this IEnumerable<TSource> source,
           Action<TSource, int> action)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                action(item, index++);
            }
        }
    }
}
