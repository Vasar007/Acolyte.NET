﻿using System;
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
        /// Helper method to perform function with cancellation token.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <param name="item">An item to perform function on.</param>
        /// <param name="function">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the <paramref name="item" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="PerformFuncWithCancellation{TSource}(TSource, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        private static Task PerformFuncWithCancellation<TSource>(TSource item,
            Func<TSource, Task> function, CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item), cancellationToken);
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the <paramref name="item" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="PerformFuncWithCancellation{TSource}(TSource, int, Func{TSource, int, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        private static Task PerformFuncWithCancellation<TSource>(TSource item, int index,
            Func<TSource, int, Task> function, CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, index), cancellationToken);
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the <paramref name="item" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="PerformFuncWithCancellation{TSource, TResult}(TSource, Func{TSource, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        private static Task<TResult> PerformFuncWithCancellation<TSource, TResult>(
            TSource item, Func<TSource, Task<TResult>> function, CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item), cancellationToken);
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the <paramref name="item" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="PerformFuncWithCancellation{TSource, TResult}(TSource, int, Func{TSource, int, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        private static Task<TResult> PerformFuncWithCancellation<TSource, TResult>(
            TSource item, int index, Func<TSource, int, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            Debug.Assert(
                function is not null,
                $"Caller must check \"{nameof(function)}\" parameter on null!"
            );

            return Task.Run(() => function!(item, index), cancellationToken);
        }

        #endregion

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static Task ParallelForEachAwaitAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> function, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                item => PerformFuncWithCancellation(item, function, cancellationToken)
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource}(IEnumerable{TSource}, Func{TSource, int, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static Task ParallelForEachAwaitAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, Task> function, CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                (item, index) => PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static Task<TResult[]> ParallelForEachAwaitAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                item => PerformFuncWithCancellation(item, function, cancellationToken)
            );

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, int, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static Task<TResult[]> ParallelForEachAwaitAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, int, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source.Select(
                (item, index) => PerformFuncWithCancellation(
                    item, index, function, cancellationToken
                )
            );

            return Task.WhenAll(results);
        }

#if NETSTANDARD2_1

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static async Task ParallelForEachAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task> function, CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task>();
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformFuncWithCancellation(item, function, cancellationToken);
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, int, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static async Task ParallelForEachAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, int, Task> function, CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task>();
            int index = 0;
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformFuncWithCancellation(item, index, function, cancellationToken);
                ++index;
                results.Add(task);
            }

            await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static async Task<TResult[]> ParallelForEachAwaitAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task<TResult>>();
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformFuncWithCancellation(item, function, cancellationToken);
                results.Add(task);
            }

            return await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> by
        /// incorporating element's index.
        /// A cancellation token allows the work to be canceled if it has not yet started.
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
        /// <see cref="ParallelForEachAwaitAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, int, Task{TResult}}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="function" />.
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
        public static async Task<TResult[]> ParallelForEachAwaitAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<TResult>> function,
            CancellationToken cancellationToken = default)
        {
            source.ThrowIfNull(nameof(source));
            function.ThrowIfNull(nameof(function));

            var results = new List<Task<TResult>>();
            int index = 0;
            await foreach (TSource item in source.WithCancellation(cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var task = PerformFuncWithCancellation(item, index, function, cancellationToken);
                ++index;
                results.Add(task);
            }

            return await Task.WhenAll(results).ConfigureAwait(continueOnCapturedContext: false);
        }

#endif
    }
}
