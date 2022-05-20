#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_DISPOSABLE

using System;
using System.Threading.Tasks;
using Acolyte.Assertions;

namespace Acolyte.Basic.Disposal
{
    public sealed class AsyncDisposableActionValue : AsyncDisposable
    {
        private readonly Func<ValueTask> _onDisposeActionAsync;


        public AsyncDisposableActionValue(
            Func<ValueTask> onDisposeActionAsync)
        {
            _onDisposeActionAsync = onDisposeActionAsync.ThrowIfNull(nameof(onDisposeActionAsync));
        }

        public bool Cancel()
        {
            bool wasDisposed = Disposed;
            Disposed = true;
            return wasDisposed;
        }

        #region IAsyncDisposable Members

        protected override async ValueTask DisposeInternalAsync()
        {
            await _onDisposeActionAsync();
        }

        #endregion
    }
}

#endif
