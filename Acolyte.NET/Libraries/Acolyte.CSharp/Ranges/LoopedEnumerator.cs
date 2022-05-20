using System.Collections;
using System.Collections.Generic;
using Acolyte.Basic.Disposal;

namespace Acolyte.Ranges
{
    public sealed class LoopedEnumerator<T> : Disposable, IEnumerator<T?>, IEnumerator
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

        protected override void DisposeInternal()
        {
            _rangeEnumerator.Dispose();
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
