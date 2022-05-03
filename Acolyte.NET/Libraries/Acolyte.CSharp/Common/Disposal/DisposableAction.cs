using System;
using Acolyte.Assertions;

namespace Acolyte.Common.Disposal
{
    public sealed class DisposableAction : Disposable
    {
        private readonly Action _onDisposeAction;


        public DisposableAction(
            Action onDisposeAction)
        {
            _onDisposeAction = onDisposeAction.ThrowIfNull(nameof(onDisposeAction));
        }

        public bool Cancel()
        {
            bool wasDisposed = Disposed;
            Disposed = true;
            return wasDisposed;
        }

        #region IAsyncDisposable Members

        protected override void DisposeInternal()
        {
            _onDisposeAction();
        }

        #endregion
    }
}
