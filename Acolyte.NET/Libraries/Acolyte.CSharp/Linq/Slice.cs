using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Extracts specified range from a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">The sequence to extract elements from.</param>
        /// <param name="startIndex">A start index to extract element.</param>
        /// <param name="count">Number of elements to extract.</param>
        /// <returns>
        /// Extracted range from <paramref name="source" />. If <paramref name="startIndex" /> or
        /// <paramref name="count"/> parameters for range is unspecified, range will be extracted
        /// based on specified parameter. If both parameters are unspecified, the original
        /// <paramref name="source" /> sequence will be return.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> SliceIfRequired<TSource>(
            this IEnumerable<TSource> source, int? startIndex, int? count)
        {
            source.ThrowIfNull(nameof(source));

            if (!startIndex.HasValue && !count.HasValue) return source;

            // TODO: write tests and ensure that this part of code is redundant.
            if (startIndex.HasValue && count.HasValue)
            {
                return source
                    .Skip(startIndex.Value)
                    .Take(count.Value);
            }

            int startIndexValue = startIndex.GetValueOrDefault();

            IEnumerable<TSource> result = source.Skip(startIndexValue);
            if (count.HasValue)
            {
                result = result.Take(count.Value);
            }

            return result;
        }
    }
}
