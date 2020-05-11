using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests
{
    public static class HelperExtensions
    {
        public static IReadOnlyList<int?> ToNullable(this IEnumerable<int> source)
        {
            // Null check for "source" parameter is provided by Enumerable.Select method.
            return source
               .Select(i => new int?(i))
               .ToList();
        }
    }
}
