using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Collapses a series of sequences down by using items from the first sequence until it
        /// finishes, then continuing from the same index through the second sequence, and so on
        /// until all sequences have been exhausted.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the input sequences.</typeparam>
        /// <param name="sequences">The input sequences.</param>
        /// <returns>
        /// Items from each sequence in turn, yielding those from the first sequence first.
        /// </returns>
        public static IEnumerable<T> Collapse<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            sequences.ThrowIfNull(nameof(sequences));

            int position = 0;
            foreach (IEnumerable<T> sequence in sequences)
            {
                foreach (T obj in sequence.Skip(position))
                {
                    ++position;
                    yield return obj;
                }
            }
        }
    }
}
