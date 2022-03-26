using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Common.Disposal;

namespace Acolyte.Collections
{
    public sealed class DisposableScope : Disposable
    {
        private readonly Stack<IDisposable?> _disposables;


        public DisposableScope()
            : this(capacity: 4)
        {
        }

        public DisposableScope(
            int capacity)
        {
            _disposables = new Stack<IDisposable?>(capacity);
        }

        [return: NotNullIfNotNull("disposable")]
        public T? Capture<T>(T? disposable)
            where T : IDisposable
        {
            _disposables.Push(disposable);
            return disposable;
        }

        public TCollection CaptureRange<TItem, TCollection>(TCollection disposableObjects)
            where TCollection : IEnumerable<TItem?>
            where TItem : IDisposable
        {
            disposableObjects.ThrowIfNullValue(nameof(disposableObjects));

            foreach (TItem? disposable in disposableObjects)
            {
                Capture(disposable);
            }

            return disposableObjects;
        }

        public void ReleaseAll()
        {
            _disposables.Clear();
        }

        [return: NotNullIfNotNull("valueToReturn")]
        public T? ReleaseAllAndReturn<T>(T? valueToReturn)
        {
            ReleaseAll();
            return valueToReturn;
        }

        #region IDisposable Implementation

        protected override void DisposeInternal()
        {

            foreach (IDisposable? disposable in _disposables)
            {
                disposable.DisposeSafe();
            }
        }

        #endregion
    }
}
