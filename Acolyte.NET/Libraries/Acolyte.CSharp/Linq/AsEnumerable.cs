#if ASYNC_ENUMERABLE

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class AsyncEnumerableExtensions
    {
        /// <summary>
        /// Transforms a <see cref="IAsyncEnumerable{T}" /> to <see cref="IEnumerable{T}" />. This
        /// method used to work with API that cannot process <see cref="IAsyncEnumerable{T}" />
        /// sequences.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IAsyncEnumerable{T}" /> to convert to <see cref="IEnumerable{T}" />.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// </param>
        /// <returns>
        /// A <see cref="IEnumerable{T}" /> that contains elements from the input asynchronous
        /// sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static async Task<IEnumerable<T>> AsEnumerableAsync<T>(
            this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));

            var result = new List<T>();
            await foreach (T entity in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                result.Add(entity);
            }

            return result;
        }
    }
}

#endif
