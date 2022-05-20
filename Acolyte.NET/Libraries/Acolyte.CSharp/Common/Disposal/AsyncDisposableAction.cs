#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_DISPOSABLE

using System;
using System.Threading.Tasks;
using Acolyte.Assertions;

namespace Acolyte.Common.Disposal
{
    public sealed class AsyncDisposableAction : AsyncDisposable
    {
        private readonly Func<Task> _onDisposeActionAsync;


        public AsyncDisposableAction(
            Func<Task> onDisposeActionAsync)
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
            await _onDisposeActionAsync().ConfigureAwait(false);
        }

        #endregion
    }
}

#endif
