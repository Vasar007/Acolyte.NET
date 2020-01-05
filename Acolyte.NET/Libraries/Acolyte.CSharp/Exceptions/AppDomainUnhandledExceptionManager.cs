using System;
using System.Collections.Concurrent;
using Acolyte.Assertions;

namespace Acolyte.Exceptions
{
    /// <summary>
    /// Allows to set up exception handlers when an exception is not caught.
    /// </summary>
    public static class AppDomainUnhandledExceptionManager
    {
        private static readonly ConcurrentBag<IUnhandledExceptionHandler> Handlers =
            new ConcurrentBag<IUnhandledExceptionHandler>();

        public static void SetHandler(IUnhandledExceptionHandler handler)
        {
            handler.ThrowIfNull(nameof(handler));

            Handlers.Add(handler);

            AppDomain.CurrentDomain.UnhandledException += handler.UnhandledExceptionEventHandler;
        }

        public static void RemoveHandlers()
        {
            foreach (IUnhandledExceptionHandler handler in Handlers)
            {
                AppDomain.CurrentDomain.UnhandledException -= handler.UnhandledExceptionEventHandler;
            }

            Handlers.Clear();
        }
    }
}
