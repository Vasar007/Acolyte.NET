#if NETSTANDARD2_1

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acolyte.Collections
{
    /// <summary>
    /// Contains useful methods to work with asynchronous enumerable items.
    /// </summary>
    [Obsolete("Use \"Acolyte.Linq.AsyncEnumerableExtensions\" instead. This class will be remove in next major version.", error: false)]
    public static class AsyncEnumerableExtensions
    {
        /// <inheritdoc cref="Linq.AsyncEnumerableExtensions.AsEnumerableAsync{T}(IAsyncEnumerable{T}, System.Threading.CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.AsyncEnumerableExtensions.AsEnumerableAsync\" instead. This method will be removed in next major version.", error: false)]
        public static Task<IEnumerable<T>> AsEnumerable<T>(this IAsyncEnumerable<T> source)
        {
            return Linq.AsyncEnumerableExtensions.AsEnumerableAsync(source);
        }
    }
}

#endif
