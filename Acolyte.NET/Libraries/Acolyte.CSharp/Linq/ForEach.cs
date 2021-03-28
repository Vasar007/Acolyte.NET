using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Threading;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region For Each

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Action{T}" /> delegate to perform on each element of the
        /// <paramref name="source" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            foreach (TSource item in source)
            {
                action(item);
            }
        }

        #endregion

        #region For Each Asynchronous

        #region Internals For Each Asynchronous

        /// <summary>
        /// Helper method to perform action with cancellation check.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of <paramref name="item" /> to perform function on.
        /// </typeparam>
        /// <param name="item">An item to perform action on.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the <paramref name="item" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="PerformActionWithCancellation{TSource}(TSource, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="action" />.
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
            Func<TSource, Task> action, CancellationToken cancellationToken)
        {
            Debug.Assert(action is not null, "Caller must check \"action\" parameter on null!");

            return Task.Run(() => action!(item), cancellationToken);
        }

        /// <summary>
        /// Helper method to perform function with cancellation check.
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
            Debug.Assert(function is not null, "Caller must check \"function\" parameter on null!");

            return Task.Run(() => function!(item), cancellationToken);
        }

        #endregion

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="action" />.
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
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> action, CancellationToken cancellationToken)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            action.ThrowIfNull(nameof(action));

            var results = source
                .Select(item => PerformActionWithCancellation(item, action, cancellationToken));

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            return source.ForEachAsync(action, cancellationToken: CancellationToken.None);
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
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
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source
                .Select(item => PerformFuncWithCancellation(item, function, cancellationToken));

            return Task.WhenAll(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
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
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return source.ForEachAsync(function, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// A cancellation token allows the work to be canceled if it has not yet started.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachSafeAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from action invocation on
        /// each item of <paramref name="source" />.
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
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, Task> action,
            CancellationToken cancellationToken)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            action.ThrowIfNull(nameof(action));

            var results = source
                .Select(item => PerformActionWithCancellation(item, action, cancellationToken));

            return TaskHelper.WhenAllResultsOrExceptions(results);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from action invocation on
        /// each item of <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return source.ForEachSafeAsync(action, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachSafeAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
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
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            // Null check for "source" parameter is provided by "Enumerable.Select" method.
            function.ThrowIfNull(nameof(function));

            var results = source
                .Select(item => PerformFuncWithCancellation(item, function, cancellationToken));

            return TaskHelper.WhenAllResultsOrExceptions(results);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">A sequence of values to perform function.</param>
        /// <param name="function">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
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
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return source.ForEachSafeAsync(function, cancellationToken: CancellationToken.None);
        }

#if NETSTANDARD2_1

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// A cancellation token allows the work to be canceled if it has not yet started.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="action" />.
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
        public static async Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task> action, CancellationToken cancellationToken)
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
        /// Performs the specified action on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            return source.ForEachAsync(action, cancellationToken: CancellationToken.None);
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
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
        public static async Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
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
        /// Performs the specified function on each element of the <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
        /// <param name="function">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
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
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return source.ForEachAsync(function, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// A cancellation token allows the work to be canceled if it has not yet started.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachSafeAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        /// does not pass <paramref name="cancellationToken" /> to <paramref name="action" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from action invocation on
        /// each item of <paramref name="source" />.
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
        public static async Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task> action,
            CancellationToken cancellationToken)
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

            return await TaskHelper.WhenAllResultsOrExceptions(results)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified action on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform action.</param>
        /// <param name="action">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <returns>
        /// A task that represents the completion of work on each item of <paramref name="source" />
        /// queued to execute in the thread pool. Task contains results from action invocation on
        /// each item of <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return source.ForEachSafeAsync(action, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
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
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the work if it has not yet started.
        /// <see cref="ForEachSafeAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
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
        public static async Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
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

            return await TaskHelper.WhenAllResultsOrExceptions(results)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Performs the specified function on each element of the <paramref name="source" /> and
        /// wraps execution in
        /// <see cref="TaskHelper.WhenAllResultsOrExceptions(IEnumerable{Task})" /> to make task for
        /// every item in <paramref name="source" /> safe. You can unwrap results and process it.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of element that <paramref name="function" /> returns.
        /// </typeparam>
        /// <param name="source">An asynchronous sequence of values to perform function.</param>
        /// <param name="function">
        /// The <see cref="Func{T, TResult}" /> delegate to perform on the
        /// <paramref name="source" />.
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
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return source.ForEachSafeAsync(function, cancellationToken: CancellationToken.None);
        }

#endif

        #endregion
    }
}
