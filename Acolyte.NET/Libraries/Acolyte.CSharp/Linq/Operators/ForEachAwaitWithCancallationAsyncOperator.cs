using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Acolyte.Linq.Operators
{
    /// <summary>
    /// Helper method to perform function with cancellation token.
    /// </summary>
    internal static class ForEachAwaitWithCancallationAsyncOperator
    {
        /// <summary>
        /// Helper method to perform function with cancellation token.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <param name="item">An item to perform function on.</param>
        /// <param name="function">
        /// A function to apply on <paramref name="item" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
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
        internal static Task PerformFuncWithCancellation<TSource>(TSource item,
            Func<TSource, CancellationToken, Task> function, CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, cancellationToken), cancellationToken);
        }

        /// <summary>
        /// Helper method to perform function with cancellation token and item index.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <param name="item">An item to perform function on.</param>
        /// <param name="index">An index of item to perform function on.</param>
        /// <param name="function">
        /// A function to apply on <paramref name="item" /> element; the second
        /// parameter of the <paramref name="function" /> represents the index of the
        /// <paramref name="item" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
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
        internal static Task PerformFuncWithCancellation<TSource>(TSource item, int index,
            Func<TSource, int, CancellationToken, Task> function,
            CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, index, cancellationToken), cancellationToken);
        }

        /// <summary>
        /// Helper method to perform function with cancellation token.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="item">An item to perform function on.</param>
        /// <param name="function">
        /// A transform function to apply on <paramref name="item" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
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
        internal static Task<TResult> PerformFuncWithCancellation<TSource, TResult>(
            TSource item, Func<TSource, CancellationToken, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, cancellationToken), cancellationToken);
        }

        /// <summary>
        /// Helper method to perform function with cancellation token and item index.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="item">An item to perform function on.</param>
        /// <param name="index">An index of item to perform function on.</param>
        /// <param name="function">
        /// A transform function to apply on <paramref name="item" /> element; the second
        /// parameter of the <paramref name="function" /> represents the index of the
        /// <paramref name="item" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
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
        internal static Task<TResult> PerformFuncWithCancellation<TSource, TResult>(TSource item,
            int index, Func<TSource, int, CancellationToken, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, index, cancellationToken), cancellationToken);
        }
    }
}
