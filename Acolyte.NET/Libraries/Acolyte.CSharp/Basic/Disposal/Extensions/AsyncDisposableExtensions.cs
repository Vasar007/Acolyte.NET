#if ASYNC_DISPOSABLE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acolyte.Common;

namespace Acolyte.Basic.Disposal
{
    public static class AsyncDisposableExtensions
    {
        private const string ErrorFormat = "Failed to dispose: {0} \"{1}\"{2}Full call stack:{2}{3}";

        public static async ValueTask DisposeSafe(this IAsyncDisposable? self)
        {
            try
            {
                if (self is null) return;

                await self.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                var stackTrace = new StackTrace(true);
                Trace.TraceWarning(ErrorFormat, ex.Message, self, Environment.NewLine, stackTrace);
            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(true);
                Trace.TraceWarning(ErrorFormat, ex.Message, self, Environment.NewLine, stackTrace);
            }
        }

        public static async ValueTask DisposeSafe<T>(this Lazy<T?>? self)
            where T : IAsyncDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            await self.Value.DisposeSafe();
        }

        public static async ValueTask DisposeSafe<T>(this ResetableLazy<T?>? self)
            where T : IAsyncDisposable
        {
            if (self is null) return;
            if (!self.IsValueCreated) return;

            await self.Value.DisposeSafe();
        }

        public static async ValueTask DisposeSafe(this IEnumerable<IAsyncDisposable?>? self)
        {
            if (self is null) return;

            foreach (var obj in self)
            {
                await obj.DisposeSafe();
            }
        }
    }
}

#endif
