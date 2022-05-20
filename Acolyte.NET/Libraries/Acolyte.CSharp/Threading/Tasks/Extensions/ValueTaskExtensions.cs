#if VALUE_TASK

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Exceptions;

namespace Acolyte.Threading.Tasks.Extensions
{
    public static class ValueTaskExtensions
    {
#region Results Or Exceptions

        public static async ValueTask<Result<TResult, Exception>> WrapResultOrExceptionAsync<TResult>(
            this ValueTask<TResult> task)
        {
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

        public static async ValueTask<Result<NoneResult, Exception>> WrapResultOrExceptionAsync(
            this ValueTask task)
        {
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

#endregion

#region Cancellation

        public static async ValueTask<TResult> CancelIfFaulted<TResult>(
            this ValueTask<TResult> task, CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource.ThrowIfNull(nameof(cancellationTokenSource));

            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception ex) when (task.IsFaulted)
            {
                cancellationTokenSource.Cancel();

                throw new TaskFaultedException(
                    "Request cancellation because a task is in the faulted state.",
                    ex
                );
            }
        }

#endregion

#region Configure

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredValueTaskAwaitable WithoutCapturedContext(this ValueTask task)
        {
            return task.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredValueTaskAwaitable<TResult> WithoutCapturedContext<TResult>(
            this ValueTask<TResult> task)
        {
            return task.ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredValueTaskAwaitable WithCapturedContext(this ValueTask task)
        {
            return task.ConfigureAwait(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConfiguredValueTaskAwaitable<TResult> WithCapturedContext<TResult>(
            this ValueTask<TResult> task)
        {
            return task.ConfigureAwait(true);
        }

#endregion
    }
}

#endif
