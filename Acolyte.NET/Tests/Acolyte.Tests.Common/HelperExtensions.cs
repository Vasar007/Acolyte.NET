using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests
{
    public static class HelperExtensions
    {
        public static IReadOnlyList<TSource?> ToNullable<TSource>(this IEnumerable<TSource> source)
            where TSource : struct
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            return source
               .Select(i => new TSource?(i))
               .ToList();
        }
    }
}
