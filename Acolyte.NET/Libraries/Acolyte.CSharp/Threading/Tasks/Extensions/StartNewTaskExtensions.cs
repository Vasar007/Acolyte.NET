using System;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;

namespace Acolyte.Threading.Tasks.Extensions
{
    public static class StartNewTaskExtensions
    {
        #region Start New For Action

        public static Task StartNew(
            this Action action,
            TaskScheduler taskScheduler)
        {
            return StartNew(
                action, taskScheduler, TaskCreationOptions.HideScheduler, CancellationToken.None
            );
        }

        public static Task StartNew(
            this Action action,
            TaskScheduler taskScheduler,
            CancellationToken token)
        {
            return StartNew(action, taskScheduler, TaskCreationOptions.HideScheduler, token);
        }

        public static Task StartNew(
            this Action action,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions)
        {
            return StartNew(action, taskScheduler, creationOptions, CancellationToken.None);
        }

        public static Task StartNew(
            this Action action,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions,
            CancellationToken token)
        {
            action.ThrowIfNull(nameof(action));
            taskScheduler.ThrowIfNull(nameof(taskScheduler));

            // Check that TaskCreationOptions value is not out of range
            // is provided by Task.Factory.StartNew method.
            return Task.Factory.StartNew(action, token, creationOptions, taskScheduler);
        }

        #endregion

        #region Start New For Func<TResult>

        public static Task<TResult> StartNew<TResult>(
            this Func<TResult> function,
            TaskScheduler taskScheduler)
        {
            return StartNew(
                function, taskScheduler, TaskCreationOptions.HideScheduler, CancellationToken.None
            );
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<TResult> function,
            TaskScheduler taskScheduler,
            CancellationToken token)
        {
            return StartNew(function, taskScheduler, TaskCreationOptions.HideScheduler, token);
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<TResult> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions)
        {
            return StartNew(function, taskScheduler, creationOptions, CancellationToken.None);
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<TResult> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions,
            CancellationToken token)
        {
            function.ThrowIfNull(nameof(function));
            taskScheduler.ThrowIfNull(nameof(taskScheduler));

            // Check that TaskCreationOptions value is not out of range
            // is provided by Task.Factory.StartNew method.
            return Task.Factory.StartNew(function, token, creationOptions, taskScheduler);
        }

        #endregion

        #region Start New For Func<Task>

        public static Task StartNew(
            this Func<Task> function,
            TaskScheduler taskScheduler)
        {
            return StartNew(
                function, taskScheduler, TaskCreationOptions.HideScheduler, CancellationToken.None
            );
        }

        public static Task StartNew(
            this Func<Task> function,
            TaskScheduler taskScheduler,
            CancellationToken token)
        {
            return StartNew(function, taskScheduler, TaskCreationOptions.HideScheduler, token);
        }

        public static Task StartNew(
            this Func<Task> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions)
        {
            return StartNew(function, taskScheduler, creationOptions, CancellationToken.None);
        }

        public static Task StartNew(
            this Func<Task> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions,
            CancellationToken token)
        {
            function.ThrowIfNull(nameof(function));
            taskScheduler.ThrowIfNull(nameof(taskScheduler));

            // Check that TaskCreationOptions value is not out of range
            // is provided by Task.Factory.StartNew method.
            return Task.Factory.StartNew(function, token, creationOptions, taskScheduler).Unwrap();
        }

        #endregion

        #region Start New For Func<Task<TResult>>

        public static Task<TResult> StartNew<TResult>(
            this Func<Task<TResult>> function,
            TaskScheduler taskScheduler)
        {
            return StartNew(
                function, taskScheduler, TaskCreationOptions.HideScheduler, CancellationToken.None
            );
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<Task<TResult>> function,
            TaskScheduler taskScheduler,
            CancellationToken token)
        {
            return StartNew(function, taskScheduler, TaskCreationOptions.HideScheduler, token);
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<Task<TResult>> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions)
        {
            return StartNew(function, taskScheduler, creationOptions, CancellationToken.None);
        }

        public static Task<TResult> StartNew<TResult>(
            this Func<Task<TResult>> function,
            TaskScheduler taskScheduler,
            TaskCreationOptions creationOptions,
            CancellationToken token)
        {
            function.ThrowIfNull(nameof(function));
            taskScheduler.ThrowIfNull(nameof(taskScheduler));

            // Check that TaskCreationOptions value is not out of range
            // is provided by Task.Factory.StartNew method.
            return Task.Factory.StartNew(function, token, creationOptions, taskScheduler).Unwrap();
        }

        #endregion
    }
}
