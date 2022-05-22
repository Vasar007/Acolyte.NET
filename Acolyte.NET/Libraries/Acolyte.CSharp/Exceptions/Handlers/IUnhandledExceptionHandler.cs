using System;

namespace Acolyte.Exceptions.Handlers
{
    /// <summary>
    /// Provides interface for exception handler used by
    /// <see cref="AppDomainUnhandledExceptionManager" />.
    /// </summary>
    public interface IUnhandledExceptionHandler
    {
        /// <inheritdoc cref="UnhandledExceptionEventHandler" />
        void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e);
    }
}
