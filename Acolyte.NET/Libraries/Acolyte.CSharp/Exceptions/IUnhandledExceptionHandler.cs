using System;

namespace Acolyte.Exceptions
{
    /// <summary>
    /// Provides interface for exception handler used by
    /// <see cref="AppDomainUnhandledExceptionManager" />.
    /// </summary>
    public interface IUnhandledExceptionHandler
    {
        /// <summary>
        /// Method to process unhandled exception.
        /// </summary>
        /// <param name="sender">The source of the unhandled exception event.</param>
        /// <param name="e">
        /// An <see cref="UnhandledExceptionEventArgs" /> that contains the event data.
        /// </param>
        void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
    }
}
