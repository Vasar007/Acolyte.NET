#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_DISPOSABLE

using System;
using System.Threading.Tasks;

namespace Acolyte.Basic.Disposal
{
    /// <summary>
    /// A general implementation of the asynchronous disposable pattern for managed resources
    /// only. This class extends <see cref="Disposable" /> class for asynchronous cases.<br/>
    /// </summary>
    /// <remarks>
    /// A base class that implements <see cref="IAsyncDisposable" />.
    /// By implementing <see cref="AsyncDisposable" />, you are announcing that
    /// instances of this type allocate scarce resources.<br/>
    /// See <a href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable" />.
    /// </remarks>
    public abstract class AsyncDisposable : Disposable, IAsyncDisposable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected AsyncDisposable()
        {
        }

        #region IDisposable Members

        /// <inheritdoc />
        /// <remarks>
        /// This method is not virtual.
        /// A derived class should not be able to override this method.
        /// </remarks>
        public async ValueTask DisposeAsync()
        {
            // Check to see if Dispose has already been called.
            if (Disposed) return;

            await DisposeInternalAsync().ConfigureAwait(false);

            // Note disposing has been done.
            Disposed = true;
        }

        /// <summary>
        /// Dispose worker method.
        /// </summary>
        /// <remarks>
        /// Release all managed resources here.
        /// </remarks>
        protected abstract ValueTask DisposeInternalAsync();

        #endregion
    }
}

#endif
