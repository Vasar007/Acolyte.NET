using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Common
{
    public static class DisposableExtensions
    {
        private const string ErrorFormat = "Failed to dispose \"{0}\"{1}Full callstack:{1}{2}";

        public static void DisposeSafe([AllowNull] this IDisposable self)
        {
            try
            {
                if (!(self is null))
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

        public static void DisposeSafe<T>([AllowNull] this Lazy<T> self)
            where T : IDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            self.Value.DisposeSafe();
        }

        public static void DisposeSafe<T>([AllowNull] this ResetableLazy<T> self)
            where T : IDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            self.Value.DisposeSafe();
        }

        public static void DisposeSafe([AllowNull] this IEnumerable<IDisposable> self)
        {
            if (self is null) return;

            foreach (IDisposable obj in self)
            {
                obj.DisposeSafe();
            }
        }
    }
}
