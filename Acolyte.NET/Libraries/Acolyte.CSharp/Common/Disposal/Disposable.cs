using System;

namespace Acolyte.Common.Disposal
{
    /// <summary>
    /// A general implementation of the disposable pattern for managed resources only.<br/>
    /// </summary>
    /// <remarks>
    /// A base class that implements <see cref="IDisposable" />.
    /// By implementing <see cref="Disposable" />, you are announcing that
    /// instances of this type allocate scarce resources.<br/>
    /// See <a href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable" />.
    /// </remarks>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Disposable()
        {
        }

        #region IDisposable Members

        /// <summary>
        /// Boolean flag used to show that object has already been disposed.
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <inheritdoc />
        /// <remarks>
        /// This method is not virtual.
        /// A derived class should not be able to override this method.
        /// </remarks>
        public void Dispose()
        {
            // Check to see if Dispose has already been called.
            if (Disposed) return;

            DisposeInternal();

            // Note disposing has been done.
            Disposed = true;
        }

        /// <summary>
        /// Dispose worker method.
        /// </summary>
        /// <remarks>
        /// Release all managed resources here.
        /// </remarks>
        protected virtual void DisposeInternal()
        {
            // Nothing to do.
        }

        #endregion
    }
}
