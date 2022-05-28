using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acolyte.Common;

namespace Acolyte.Threading.Tasks
{
    /// <summary>
    /// Contains common logic to work with tasks.
    /// </summary>
    public static class TaskHelper
    {
        #region When All Results Or Exceptions

        public static Task<Result<TResult, Exception>[]> WhenAllResultsOrExceptions<TResult>(
            params Task<TResult>[] tasks)
        {
            return tasks.WhenAllResultsOrExceptions();
        }

        public static Task<Result<TResult, Exception>[]> WhenAllResultsOrExceptions<TResult>(
            IEnumerable<Task<TResult>> tasks)
        {
            return tasks.WhenAllResultsOrExceptions();
        }

        public static Task<Result<NoneResult, Exception>[]> WhenAllResultsOrExceptions(
            params Task[] tasks)
        {
            return tasks.WhenAllResultsOrExceptions();
        }

        public static Task<Result<NoneResult, Exception>[]> WhenAllResultsOrExceptions(
            IEnumerable<Task> tasks)
        {
            return tasks.WhenAllResultsOrExceptions();
        }

        #endregion
    }
}
