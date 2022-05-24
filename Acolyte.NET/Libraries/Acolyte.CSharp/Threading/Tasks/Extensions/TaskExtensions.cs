using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Exceptions;

namespace Acolyte.Threading.Tasks
{
    /// <summary>
    /// Contains extension methods to work with tasks.
    /// </summary>
    public static class TaskExtensions
    {
        #region Results Or Exceptions

        public static async Task<Result<TResult, Exception>> WrapResultOrExceptionAsync<TResult>(
            this Task<TResult> task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                TResult taskResult = await task.ConfigureAwait(false);
                return Result.Ok(taskResult);
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public static async Task<Result<NoneResult, Exception>> WrapResultOrExceptionAsync(
            this Task task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                await task.ConfigureAwait(false);
                return Result.Ok(new NoneResult());
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public static IReadOnlyList<Exception> UnwrapResultsOrExceptions(
            this IEnumerable<Result<NoneResult, Exception>> source)
        {
            source.ThrowIfNull(nameof(source));

            var exceptions = new List<Exception>();
            foreach (Result<NoneResult, Exception> result in source)
            {
                // Filter null exceptions.
                if (!result.IsSuccess && result.Error is not null)
                {
                    exceptions.Add(result.Error);
                }
            }

            return exceptions.AsReadOnly();
        }

        public static (IReadOnlyList<TResult> taskResults, IReadOnlyList<Exception> taskExceptions)
            UnwrapResultsOrExceptions<TResult>(this IEnumerable<Result<TResult, Exception>> source)
        {
            source.ThrowIfNull(nameof(source));

            var taskResults = new List<TResult>();
            var taskExceptions = new List<Exception>();
            foreach (Result<TResult, Exception> item in source)
            {
                if (item.IsSuccess)
                {
                    // List allows to pass null values.
                    taskResults.Add(item.Ok!);
                }
                else if (item.Error is not null) // Filter null exceptions.
                {
                    taskExceptions.Add(item.Error);
                }
            }

            return (taskResults.AsReadOnly(), taskExceptions.AsReadOnly());
        }

        #endregion

        #region Cancellation

        public static Task<TResult> CancelIfFaulted<TResult>(
            this Task<TResult> task, CancellationTokenSource cancellationTokenSource)
        {
            task.ThrowIfNullDiscard(nameof(task));
            cancellationTokenSource.ThrowIfNull(nameof(cancellationTokenSource));

            return task.ContinueWith(
                p =>
                {
                    switch (p.Status)
                    {
                        case TaskStatus.RanToCompletion:
                        {
                            return p.Result;
                        }

                        case TaskStatus.Faulted:
                        {
                            cancellationTokenSource.Cancel();

                            Exception? exception = null;
                            if (p.Exception is not null)
                                exception = ExceptionsHelper.UnwrapAggregateExceptionIfSingle(p.Exception);

                            throw new TaskFaultedException(
                                "Request cancellation because a task is in the faulted state.",
                                exception
                            );
                        }

                        default:
                        {
                            return default!;
                        }
                    }
                }
            );
        }

        #endregion

        #region Checked Continue

        public static Task CheckedContinueWith(this Task task, Action<Task> continuationAction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationAction.ThrowIfNull(nameof(continuationAction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                continuationAction(p);
            });
        }

        public static Task CheckedContinueWith<TResult>(this Task<TResult> task,
            Action<Task<TResult>> continuationAction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationAction.ThrowIfNull(nameof(continuationAction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                continuationAction(p);
            });
        }

        public static Task<TNewResult> CheckedContinueWith<TResult, TNewResult>(
            this Task<TResult> task, Func<Task<TResult>, TNewResult> continuationFunction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationFunction.ThrowIfNull(nameof(continuationFunction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                return continuationFunction(p);
            });
        }

        public static Task<TResult> CheckedContinueWith<TResult>(this Task task,
            Func<Task, TResult> continuationFunction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationFunction.ThrowIfNull(nameof(continuationFunction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                return continuationFunction(p);
            });
        }

        public static Task<TNewResult> CheckedContinueWith<TNewResult>(this Task task,
            Func<Task, Task<TNewResult>> continuationFunction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationFunction.ThrowIfNull(nameof(continuationFunction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                return continuationFunction(p);
            }).Unwrap();
        }

        public static Task<TNewResult> CheckedContinueWith<TFirst, TNewResult>(
            this Task<TFirst> task, Func<Task<TFirst>, Task<TNewResult>> continuationFunction)
        {
            task.ThrowIfNullDiscard(nameof(task));
            continuationFunction.ThrowIfNull(nameof(continuationFunction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                return continuationFunction(p);
            }).Unwrap();
        }

        #endregion

        #region Wait And Unwrap

        /// <summary>
        /// Synchronously waits for the given task to complete, and returns the result.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <typeparam name="T">The result type of the task</typeparam>
        /// <param name="task">The task to wait for.</param>
        /// <returns>The result of the completed task.</returns>
        public static T ResultWithUnwrappedExceptions<T>(this Task<T> task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            task.WaitWithUnwrappedExceptions();
            return task.Result;
        }

        /// <summary>
        /// Synchronously waits for the given task to complete.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        public static void WaitWithUnwrappedExceptions(this Task task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                throw ExceptionsHelper.UnwrapAndThrow(ex);
            }
        }

        /// <summary>
        /// Synchronously waits for the given task to complete.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="timeout">
        /// A TimeSpan that represents the number of milliseconds to wait, or
        /// -1 milliseconds to wait indefinitely.
        /// </param>
        public static bool WaitWithUnwrappedExceptions(this Task task, TimeSpan timeout)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                return task.Wait(timeout);
            }
            catch (AggregateException ex)
            {
                throw ExceptionsHelper.UnwrapAndThrow(ex);
            }
        }

        /// <summary>
        /// Synchronously waits for the given task to complete.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or -1 to wait indefinitely.
        /// </param>
        public static bool WaitWithUnwrappedExceptions(this Task task, int millisecondsTimeout)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                return task.Wait(millisecondsTimeout);
            }
            catch (AggregateException ex)
            {
                throw ExceptionsHelper.UnwrapAndThrow(ex);
            }
        }

        /// <summary>
        /// Synchronously waits for the given task to complete.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or -1 to wait indefinitely.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the task to complete.
        /// </param>
        public static bool WaitWithUnwrappedExceptions(this Task task, int millisecondsTimeout,
            CancellationToken cancellationToken)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                return task.Wait(millisecondsTimeout, cancellationToken);
            }
            catch (AggregateException ex)
            {
                throw ExceptionsHelper.UnwrapAndThrow(ex);
            }
        }

        /// <summary>
        /// Synchronously waits for the given task to complete.
        /// Any <see cref="AggregateException" /> thrown is unwrapped to the first inner exception.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the task to complete.
        /// </param>
        public static void WaitWithUnwrappedExceptions(this Task task,
            CancellationToken cancellationToken)
        {
            task.ThrowIfNullDiscard(nameof(task));

            try
            {
                task.Wait(cancellationToken);
            }
            catch (AggregateException ex)
            {
                throw ExceptionsHelper.UnwrapAndThrow(ex);
            }
        }

        #endregion

        #region Timeout

        public static async Task AwaitWithTimeoutAsync(this Task task, TimeSpan timeout)
        {
            task.ThrowIfNullDiscard(nameof(task));

            var completion = await Task.WhenAny(task, DelayedResultTask(timeout))
                .ConfigureAwait(false);

            await completion.ConfigureAwait(false);
        }

        public static async Task AwaitWithTimeoutAndExceptionAsync(this Task task, TimeSpan timeout)
        {
            task.ThrowIfNullDiscard(nameof(task));

            var completion = await Task.WhenAny(task, DelayedTimeoutExceptionTask(timeout))
                .ConfigureAwait(false);

            await completion.ConfigureAwait(false);
        }

        private static Task DelayedResultTask(TimeSpan delay)
        {
            return Task.Delay(delay);
        }

        private static async Task DelayedTimeoutExceptionTask(TimeSpan delay)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            throw new TimeoutException();
        }

        public static Task<T?> AwaitWithTimeoutAsync<T>(this Task<T> task, TimeSpan timeout)
        {
            return task.AwaitWithTimeoutAndFallbackAsync(timeout, () => default!)!;
        }

        public static async Task<T> AwaitWithTimeoutAndFallbackAsync<T>(this Task<T> task,
            TimeSpan timeout, Func<T> fallbackFactory)
        {
            task.ThrowIfNullDiscard(nameof(task));
            fallbackFactory.ThrowIfNull(nameof(fallbackFactory));

            var completion = await Task.WhenAny(task, DelayedResultTask(timeout, fallbackFactory))
                .ConfigureAwait(false);
            return await completion.ConfigureAwait(false);
        }

        public static async Task<T> AwaitWithTimeoutAndExceptionAsync<T>(this Task<T> task,
            TimeSpan timeout)
        {
            task.ThrowIfNullDiscard(nameof(task));

            var completion = await Task.WhenAny(task, DelayedTimeoutExceptionTask<T>(timeout))
                .ConfigureAwait(false);

            return await completion.ConfigureAwait(false);
        }

        private static async Task<T> DelayedResultTask<T>(TimeSpan delay, Func<T> fallbackFactory)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            return fallbackFactory();
        }

        private static async Task<T> DelayedTimeoutExceptionTask<T>(TimeSpan delay)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            throw new TimeoutException();
        }

        #endregion

        #region Configure

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredTaskAwaitable WithoutCapturedContext(this Task task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            return task.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredTaskAwaitable<TResult> WithoutCapturedContext<TResult>(
            this Task<TResult> task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            return task.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredTaskAwaitable WithCapturedContext(this Task task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            return task.ConfigureAwait(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredTaskAwaitable<TResult> WithCapturedContext<TResult>(
            this Task<TResult> task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            return task.ConfigureAwait(true);
        }

        #endregion
    }
}
