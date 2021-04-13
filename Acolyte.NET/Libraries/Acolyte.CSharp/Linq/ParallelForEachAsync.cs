using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region Internals For Each Asynchronous

        /// <summary>
        /// Helper method to perform action with cancellation token.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform action on.
        /// </typeparam>
        /// <param name="item">An item to perform action on.</param>
        /// <param name="action">
        /// An action to apply on <paramref name="item" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method does not pass <paramref name="cancellationToken" /> to
        /// <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the work on an <paramref name="item" /> queued to execute in the
        /// thread pool.
        /// </returns>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        private static Task PerformActionWithCancellation<TSource>(TSource item,
            Action<TSource> action, CancellationToken cancellationToken)
        {
            Debug.Assert(
                action is not null,
                $"Caller must check \"{nameof(action)}\" parameter on null!"
            );

            return Task.Run(() => action!(item), cancellationToken);
        }

        /// <summary>
        /// Helper method to perform action with cancellation token and item index.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform action on.
        /// </typeparam>
        /// <param name="item">An item to perform action on.</param>
        /// <param name="index">An index of item to perform action on.</param>
        /// <param name="action">
        /// An action to apply on <paramref name="item" /> element; the second parameter of
        /// the <paramref name="action" /> represents the index of the <paramref name="item" />
        /// element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method does not pass <paramref name="cancellationToken" /> to
        /// <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the work on an <paramref name="item" /> queued to execute in the
        /// thread pool.
        /// </returns>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        private static Task PerformActionWithCancellation<TSource>(TSource item, int index,
            Action<TSource, int> action, CancellationToken cancellationToken)
        {
            Debug.Assert(
                action is not null,
                $"Caller must check \"{nameof(action)}\" parameter on null!"
            );

            return Task.Run(() => action!(item, index), cancellationToken);
        }

        #endregion

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
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
        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source,
            Action<TSource> action, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            action.ThrowIfNull(nameof(action));

            var results = source.Select(
                item => PerformActionWithCancellation(item, action, cancellationToken)
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// An action to apply on each <paramref name="source" /> element; the second parameter of
        /// the <paramref name="action" /> represents the index of the <paramref name="source" />
        /// element.
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
        public static Task ParallelForEachAsync<TSource>(this IEnumerable<TSource> source,
            Action<TSource, int> action, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            action.ThrowIfNull(nameof(action));

            var results = source.Select(
                (item, index) => PerformActionWithCancellation(
                    item, index, action, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

#if NETSTANDARD2_1

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
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformActionWithCancellation(item, action, cancellationToken);
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
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
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformActionWithCancellation(item, index, action, cancellationToken);
                ++index;
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

#endif
    }
}
