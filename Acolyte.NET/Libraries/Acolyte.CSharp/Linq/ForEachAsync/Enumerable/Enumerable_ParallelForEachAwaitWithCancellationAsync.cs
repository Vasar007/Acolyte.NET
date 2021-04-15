using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Linq.Operators;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// A function to apply on each <paramref name="source" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="function" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static Task ParallelForEachAwaitWithCancellationAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, CancellationToken, Task> function,
            CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                item => ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// A function to apply on each <paramref name="source" /> element; the second parameter of
        /// the <paramref name="function" /> represents the index of the <paramref name="source" />
        /// element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="function" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static Task ParallelForEachAwaitWithCancellationAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, int, CancellationToken, Task> function,
            CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                (item, index) => ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// A transform function to apply on each <paramref name="source" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from function invocation on
        /// each item of <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="function" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static Task<TResult[]> ParallelForEachAwaitWithCancellationAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, CancellationToken,
            Task<TResult>> function, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                item => ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// A transform function to apply on each <paramref name="source" /> element; the second
        /// parameter of the <paramref name="function" /> represents the index of the
        /// <paramref name="source" /> element.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional cancellation token for canceling the sequence at any time.
        /// This method passes <paramref name="cancellationToken" /> to
        /// <paramref name="function" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from function invocation on
        /// each item of <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="function" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">
        /// The <see cref="CancellationTokenSource" /> associated with
        /// <paramref name="cancellationToken" /> was disposed.
        /// </exception>
        public static Task<TResult[]> ParallelForEachAwaitWithCancellationAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, int, CancellationToken,
            Task<TResult>> function, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                (item, index) => ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }
    }
}
