using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Checks if enumerable is <see langword="null" /> or empty without throwing exception.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}" /> to check.</param>
        /// <returns>
        /// Returns <see langword="true" /> in case the enumerable is <see langword="null" /> or empty, <see langword="false" /> 
        /// otherwise.
        /// </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource>? source)
        {
            return source is null || !source.Any();
        }
    }
}
