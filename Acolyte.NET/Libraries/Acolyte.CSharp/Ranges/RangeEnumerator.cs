using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public sealed class RangeEnumerator<T> : IEnumerator<T?>, IEnumerator
    {
        // TODO: optimize this enumerator for small ranges (store all values in cache).
        // TODO: accept configuration instead of raw parameters.

        private readonly IEnumerable<T> _originalRange;

        private IEnumerator<T> _originalEnumerator;

        public bool IsInfinite { get; }

        public T? Current { get; private set; }

        object? IEnumerator.Current => Current;


        public RangeEnumerator(
            IEnumerable<T> originalRange,
            bool isInfinite)
        {
            _originalRange = originalRange.ThrowIfNull(nameof(originalRange));
            IsInfinite = isInfinite;

            _originalEnumerator = _originalRange.GetEnumerator();
            Current = default;
        }

        public bool MoveNext()
        {
            if (!_originalEnumerator.MoveNext())
            {
                // If range is infinite, recreate enumerator and start enumeration again.
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
            return IsInfinite;
        }
    }


    public static class RangeEnumerator
    {
        public static RangeEnumerator<T> Create<T>(
            IEnumerable<T> originalRange,
            bool isInfinite)
        {
            return new RangeEnumerator<T>(
                originalRange: originalRange,
                isInfinite: isInfinite
            );
        }
    }
}
