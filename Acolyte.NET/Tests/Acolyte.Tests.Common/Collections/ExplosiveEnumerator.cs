using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Exceptions;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private readonly IEnumerator<T> _originalEnumerator;

        private readonly int _explosiveIndex;

        private bool _disposed;

        private int _currentIndex;

        [AllowNull, MaybeNull]
        public T Current { get; private set; }

        object? IEnumerator.Current => Current;


        public ExplosiveEnumerator(
            IEnumerator<T> originalEnumerator,
            int explosiveIndex)
        {
            _originalEnumerator = originalEnumerator.ThrowIfNull(nameof(originalEnumerator));
            _explosiveIndex =
                explosiveIndex.ThrowIfValueIsOutOfRange(nameof(explosiveIndex), 0, int.MaxValue);

            Current = default;
            _currentIndex = Constants.NotFoundIndex;
        }

        public bool MoveNext()
        {
            if (_currentIndex == _explosiveIndex) throw new ExplosiveException(_explosiveIndex);

            if (!_originalEnumerator.MoveNext()) return false;

            ++_currentIndex;
            Current = _originalEnumerator.Current;
            return true;
        }

        public void Reset()
        {
            _originalEnumerator.Reset();
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            if (_disposed) return;

            _originalEnumerator.Dispose();

            _disposed = true;
        }

        #endregion
    }


    public static class ExplosiveEnumerator
    {
        public static ExplosiveEnumerator<T> Create<T>(
            IEnumerator<T> originalEnumerator,
            int explosiveIndex)
        {
            return new ExplosiveEnumerator<T>(
                originalEnumerator: originalEnumerator,
                explosiveIndex: explosiveIndex
            );
        }
    }
}
