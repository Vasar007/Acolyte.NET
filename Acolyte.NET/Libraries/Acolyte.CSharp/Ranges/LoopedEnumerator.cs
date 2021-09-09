using System.Collections;
using System.Collections.Generic;

namespace Acolyte.Ranges
{
    public sealed class LoopedEnumerator<T> : IEnumerator<T?>, IEnumerator
    {
        private readonly RangeEnumerator<T> _rangeEnumerator;

        public T? Current { get; private set; }

        object? IEnumerator.Current => Current;


        public LoopedEnumerator(
            IEnumerable<T> originalRange)
        {
            _rangeEnumerator = RangeEnumerator.Create(originalRange, isInfinite: true);
            Current = default;
        }

        public bool MoveNext()
        {
            // "MoveNext" method will always be true.
            if (!_rangeEnumerator.MoveNext()) return false;

            Current = _rangeEnumerator.Current;

            return true;
        }

        public void Reset()
        {
            _rangeEnumerator.Reset();
        }

        #region IDisposable Implementation

        /// <summary>
        /// Boolean flag used to show that object has already been disposed.
        /// </summary>
        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            _rangeEnumerator.Dispose();

            _disposed = true;
        }

        #endregion
    }

    public static class LoopedEnumerator
    {
        public static LoopedEnumerator<T> Create<T>(
            IEnumerable<T> originalRange)
        {
            return new LoopedEnumerator<T>(
                originalRange: originalRange
            );
        }
    }
}
