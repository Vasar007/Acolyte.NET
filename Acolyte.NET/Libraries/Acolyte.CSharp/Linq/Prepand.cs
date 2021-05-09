using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Adds a value to the beginning of the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="element">The value to prepend to <paramref name="source"/>.</param>
        /// <returns>A new sequence that begins with element.</returns>
        ///<exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> PrependElement<T>(this IEnumerable<T> source, T element)
        {
            source.ThrowIfNull(nameof(source));

            yield return element;
            foreach (T item in source)
            {
                yield return item;
            }
        }
    }
}
