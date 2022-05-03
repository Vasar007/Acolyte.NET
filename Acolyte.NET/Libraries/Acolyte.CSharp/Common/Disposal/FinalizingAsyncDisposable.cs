#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_DISPOSABLE

using System;
using System.Threading.Tasks;

namespace Acolyte.Common.Disposal
{
    /// <summary>
    /// A general implementation of the disposable pattern for both managed and unmanaged
    /// resources. This class extends <see cref="FinalizingDisposable" /> class for asynchronous
    /// cases.<br/>
    /// </summary>
    /// <remarks>
    /// A base class that implements <see cref="IDisposable" />.
    /// By implementing <see cref="FinalizingAsyncDisposable" />, you are announcing that
    /// instances of this type allocate scarce resources.<br/>
    /// See <a href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable" />.
    /// </remarks>
    public abstract class FinalizingAsyncDisposable : FinalizingDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected FinalizingAsyncDisposable()
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

            await DisposeAsync(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);

            // Note disposing has been done.
            Disposed = true;
        }

        /// <summary>
        /// Dispose worker method.
        /// </summary>
        /// <remarks>
        /// Release all managed and/or unmanaged resources here.
        /// <see cref="DisposeAsync(bool)" /> executes in two distinct scenarios.
        /// If <paramref name="disposing" /> equals <see langword="true" />, the method has been
        /// called directly or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If <paramref name="disposing" /> equals <see langword="false" />, the method has been
        /// called by the runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </remarks>
        /// <param name="disposing">Are we disposing? Otherwise we're finalizing.</param>
        protected abstract ValueTask DisposeAsync(bool disposing);

        /// <summary>
        /// Finalizer.
        /// </summary>
        /// <remarks>
        /// Use C# finalizer syntax for finalization code.
        /// This finalizer will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide finalizer in types derived from this class.
        /// </remarks>
        ~FinalizingAsyncDisposable()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}

#endif
