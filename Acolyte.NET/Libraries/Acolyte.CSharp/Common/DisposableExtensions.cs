﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Acolyte.Common
{
    public static class DisposableExtensions
    {
        private const string ErrorFormat = "Failed to dispose \"{0}\"{1}Full call stack:{1}{2}";

        public static void DisposeSafe(this IDisposable? self)
        {
            try
            {
                if (self is not null)
                {
                    self.Dispose();
                }
            }
            catch (ObjectDisposedException ex)
            {
                var currentStack = new StackTrace(true);
                Trace.TraceWarning(
                    ErrorFormat, ex, ErrorFormat, self, Environment.NewLine, currentStack
                );
            }
            catch (Exception ex)
            {
                var currentStack = new StackTrace(true);
                Trace.TraceError(
                    ErrorFormat, ex, ErrorFormat, self, Environment.NewLine, currentStack
                );
            }
        }

        public static void DisposeSafe<T>(this Lazy<T>? self)
            where T : IDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            self.Value.DisposeSafe();
        }

        public static void DisposeSafe<T>(this ResetableLazy<T>? self)
            where T : IDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            self.Value.DisposeSafe();
        }

        public static void DisposeSafe(this IEnumerable<IDisposable>? self)
        {
            if (self is null) return;

            foreach (IDisposable obj in self)
            {
                obj.DisposeSafe();
            }
        }
    }
}
