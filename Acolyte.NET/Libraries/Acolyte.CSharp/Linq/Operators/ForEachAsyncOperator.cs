using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Acolyte.Linq.Operators
{
    /// <summary>
    /// Helper method to perform action with cancellation token.
    /// </summary>
    internal static class ForEachAsyncOperator
    {
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
        public static Task PerformActionWithCancellation<TSource>(TSource item,
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
        public static Task PerformActionWithCancellation<TSource>(TSource item, int index,
            Action<TSource, int> action, CancellationToken cancellationToken)
        {
            Debug.Assert(
                action is not null,
                $"Caller must check \"{nameof(action)}\" parameter on null!"
            );

            return Task.Run(() => action!(item, index), cancellationToken);
        }
    }
}
