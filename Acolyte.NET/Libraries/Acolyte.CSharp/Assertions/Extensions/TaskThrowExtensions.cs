using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Acolyte.Exceptions;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Provides <see langword="null" /> check for task reference type value.
        /// This method does not return original object to avoid compiler warnings.
        /// </summary>
        /// <param name="obj">Instance to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        public static void ThrowIfNullDiscard(this Task? obj, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Checks task state and throws exception if task is in the
        /// <see cref="TaskStatus.Faulted" /> state.
        /// </summary>
        /// <param name="task">Task to check.</param>
        /// <returns>The original task.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="task" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="task" /> is not completed yet (that is, the task is in one of the three
        /// final states: <see cref="TaskStatus.RanToCompletion" />,
        /// <see cref="TaskStatus.Faulted" />, or <see cref="TaskStatus.Canceled" />).
        /// </exception>
        /// <exception cref="TaskFaultedException">
        /// <paramref name="task" /> is faulted (that is, the task is in the 
        /// <see cref="TaskStatus.Faulted" /> state).
        /// </exception>
        [return: NotNull]
        public static Task ThrowIfFaulted(this Task task)
        {
            task.ThrowIfNullDiscard(nameof(task));

            if (!task.IsCompleted)
            {
                throw new ArgumentException("Task is not completed yet.", nameof(task));
            }

            if (task.IsFaulted)
            {
                throw new TaskFaultedException("Task is faulted.", task.Exception);
            }

            return task;
        }
    }
}
