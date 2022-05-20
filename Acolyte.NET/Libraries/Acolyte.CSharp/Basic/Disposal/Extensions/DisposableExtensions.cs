using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acolyte.Common;

namespace Acolyte.Basic.Disposal
{
    public static class DisposableExtensions
    {
        private const string ErrorFormat = "Failed to dispose \"{0}\"{1}Full call stack:{1}{2}";

        public static void DisposeSafe(this IDisposable? self)
        {
            try
            {
                self?.Dispose();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceWarning(ErrorFormat, self, System.Environment.NewLine, ex);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ErrorFormat, self, System.Environment.NewLine, ex);
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
