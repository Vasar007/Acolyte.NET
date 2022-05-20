#if ASYNC_ENUMERABLE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Linq.Operators;

namespace Acolyte.Linq
{
    public static partial class AsyncEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// An action to apply on each <paramref name="source" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method does not pass <paramref name="cancellationToken" /> to
        /// <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static async Task ParallelForEachAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Action<TSource> action,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            var results = new List<Task>();
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                var task = ForEachAsyncOperator.PerformActionWithCancellation(
                    item, action, cancellationToken
                );
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(false);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// A transform action to apply on each <paramref name="source" /> element; the second
        /// parameter of the <paramref name="action" /> represents the index of the
        /// <paramref name="source" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method does not pass <paramref name="cancellationToken" /> to
        /// <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static async Task ParallelForEachAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Action<TSource, int> action,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            var results = new List<Task>();
            int index = 0;
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                var task = ForEachAsyncOperator.PerformActionWithCancellation(
                    item, index, action, cancellationToken
                );
                ++index;
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(false);
        }
    }
}

#endif
