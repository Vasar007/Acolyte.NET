using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public sealed class LoopedEnumerator<T> : IEnumerator<T?>, IEnumerator
    {
        private readonly IEnumerable<T> _originalRange;

        private readonly bool _isInfinite;

        private IEnumerator<T> _originalEnumerator;

        public T? Current { get; private set; }

        object? IEnumerator.Current => Current;


        public LoopedEnumerator(
            IEnumerable<T> originalRange,
            bool isInfinite)
        {
            _originalRange = originalRange.ThrowIfNull(nameof(originalRange));
            _isInfinite = isInfinite;

            _originalEnumerator = _originalRange.GetEnumerator();
            Current = default;
        }

        public bool MoveNext()
        {
            if (!_originalEnumerator.MoveNext())
            {
                // If loop is infinite, recreate enumerator and start enumeration again.
                if (ShouldContinue())
                {
                    _originalEnumerator.Dispose();
                    _originalEnumerator = _originalRange.GetEnumerator();
                    return MoveNext();
                }

                return false;
            }

            Current = _originalEnumerator.Current;

            return true;
        }

        public void Reset()
        {
            _originalEnumerator.Reset();
        }

        #region IDisposable Implementation

        /// <summary>
        /// Boolean flag used to show that object has already been disposed.
        /// </summary>
        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            _originalEnumerator.Dispose();

            _disposed = true;
        }

        #endregion

        private bool ShouldContinue()
        {
            return _isInfinite;
        }
    }


    public static class LoopedEnumerator
    {
        public static LoopedEnumerator<T> Create<T>(
            IEnumerable<T> originalRange,
            bool isInfinite)
        {
            return new LoopedEnumerator<T>(
                originalRange: originalRange,
                isInfinite: isInfinite
            );
        }
    }
}
