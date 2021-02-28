using System;
using System.Threading;
using Acolyte.Collections;

namespace Acolyte.Threading
{
    /// <summary>
    /// Contains additional logic to work with <see cref="Thread" /> class.
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// Creates message with information about current thread with specified data.
        /// </summary>
        /// <param name="outputObjects">Additional objects to append to message.</param>
        /// <returns>Message about current thread with specified data in message.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="outputObjects"/> is <see langword="null" />.
        /// </exception>
        public static string GetThreadInfoWithParams(params object[] outputObjects)
        {
            string message = outputObjects.IsNullOrEmpty()
                             ? string.Empty
                             : ", params: " + string.Join(", ", outputObjects);

            return $"On thread {Thread.CurrentThread.ManagedThreadId.ToString()}{message}";
        }
    }
}
