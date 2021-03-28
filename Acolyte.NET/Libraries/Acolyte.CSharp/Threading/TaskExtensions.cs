using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Exceptions;

namespace Acolyte.Threading
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
            _ = task.ThrowIfNull(nameof(task));

            try
            {
                TResult taskResult = await task.ConfigureAwait(continueOnCapturedContext: false);
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
            _ = task.ThrowIfNull(nameof(task));

            try
            {
                await task.ConfigureAwait(continueOnCapturedContext: false);
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
                if (result.IsSuccess && result.Error is not null)
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
                if (!item.IsSuccess)
                {
                    // List allows to pass null values.
#pragma warning disable CS8604 // Possible null reference argument.
                    taskResults.Add(item.Ok);
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else if (!(item.Error is null)) // Filter null exceptions.
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
            _ = task.ThrowIfNull(nameof(task));
            cancellationTokenSource.ThrowIfNull(nameof(cancellationTokenSource));

            return task.ContinueWith(
                task =>
                {
                    switch (task.Status)
                    {
                        case TaskStatus.RanToCompletion:
                        {
                            return task.Result;
                        }

                        case TaskStatus.Faulted:
                        {
                            cancellationTokenSource.Cancel();

                            Exception exception =
                                ExceptionsHelper.UnwrapAggregateExceptionIfSingle(task.Exception);

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
            _ = task.ThrowIfNull(nameof(task));
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
            _ = task.ThrowIfNull(nameof(task));
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
            _ = task.ThrowIfNull(nameof(task));
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
            _ = task.ThrowIfNull(nameof(task));
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
            _ = task.ThrowIfNull(nameof(task));
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
            _ = task.ThrowIfNull(nameof(task));
            continuationFunction.ThrowIfNull(nameof(continuationFunction));

            return task.ContinueWith(p =>
            {
                p.ThrowIfFaulted();
                return continuationFunction(p);
            }).Unwrap();
        }

        #endregion
    }
}
