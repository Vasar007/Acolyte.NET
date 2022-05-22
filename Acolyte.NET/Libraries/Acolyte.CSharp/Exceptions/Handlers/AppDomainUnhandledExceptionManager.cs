using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Exceptions.Handlers
{
    /// <summary>
    /// Allows to set up exception handlers when an exception is not caught.
    /// </summary>
    public static class AppDomainUnhandledExceptionManager
    {
        private static readonly List<IUnhandledExceptionHandler> Handlers = new();

        private static readonly object Lock = new();


        public static void RegisterHandler(IUnhandledExceptionHandler handler)
        {
            handler.ThrowIfNull(nameof(handler));

            lock (Lock)
            {
                Handlers.Add(handler);
                AppDomain.CurrentDomain.UnhandledException += handler.HandleUnhandledException;
            }
        }

        public static void UnregisterHandler(IUnhandledExceptionHandler handler)
        {
            handler.ThrowIfNull(nameof(handler));

            lock (Lock)
            {
                if (Handlers.Remove(handler))
                {
                    AppDomain.CurrentDomain.UnhandledException -= handler.HandleUnhandledException;
                }
            }
        }

        public static void UnregisterAllHandlers()
        {
            lock (Lock)
            {
                foreach (IUnhandledExceptionHandler handler in Handlers)
                {
                    AppDomain.CurrentDomain.UnhandledException -= handler.HandleUnhandledException;
                }

                Handlers.Clear();
            }
        }
    }
}
