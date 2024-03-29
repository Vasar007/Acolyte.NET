﻿#if NETSTANDARD2_1

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
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
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
        public static async Task ParallelForEachAwaitWithCancellationAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, Task> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task>();
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, function, cancellationToken
                );
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
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
        public static async Task ParallelForEachAwaitWithCancellationAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, Task> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task>();
            int index = 0;
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                );
                ++index;
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
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
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
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
        public static async Task<TResult[]> ParallelForEachAwaitWithCancellationAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, CancellationToken, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task<TResult>>();
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, function, cancellationToken
                );
                results.Add(task);
            }

            return await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
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
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
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
        public static async Task<TResult[]> ParallelForEachAwaitWithCancellationAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, int, CancellationToken, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task<TResult>>();
            int index = 0;
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = ForEachAwaitWithCancallationAsyncOperator.PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                );
                ++index;
                results.Add(task);
            }

            return await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}

#endif
