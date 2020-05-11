using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests
{
    internal static class HelperExtensions
    {
        internal static IReadOnlyList<int?> ToNullable(this IEnumerable<int> source)
        {
            // Null check for "source" parameter is provided by Enumerable.Select method.
            return source
               .Select(i => new int?(i))
               .ToList();
        }
    }
}
