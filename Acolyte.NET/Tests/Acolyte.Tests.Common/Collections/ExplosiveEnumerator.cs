﻿using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Tests.Exceptions;
using Acolyte.Threading;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveEnumerator<T> : IEnumerator<T?>, IEnumerator
    {
        private readonly IEnumerator<T> _originalEnumerator;

        private readonly CounterInt32 _visitedItemsNumber;

        public int ExplosiveIndex { get; }

        public T? Current { get; private set; }

        object? IEnumerator.Current => Current;


        public ExplosiveEnumerator(
            IEnumerator<T> originalEnumerator,
            int explosiveIndex,
            CounterInt32 visitedItemsNumber)
        {
            _originalEnumerator = originalEnumerator.ThrowIfNull(nameof(originalEnumerator));
            ExplosiveIndex = explosiveIndex.ThrowIfValueIsOutOfRange(
                nameof(explosiveIndex), Constants.NotFoundIndex, int.MaxValue
            );
            _visitedItemsNumber = visitedItemsNumber.ThrowIfNull(nameof(visitedItemsNumber));

            Current = default;
        }

        public bool MoveNext()
        {
            if (!_originalEnumerator.MoveNext()) return false;

            _visitedItemsNumber.Increment();
            Current = _originalEnumerator.Current;

            if (ShouldExplode()) throw new ExplosiveException(ExplosiveIndex);

            return true;
        }

        public void Reset()
        {
            _visitedItemsNumber.Reset();
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

        private bool ShouldExplode()
        {
            return _visitedItemsNumber.Value == ExplosiveIndex + 1;
        }
    }


    public static class ExplosiveEnumerator
    {
        public static ExplosiveEnumerator<T> Create<T>(
            IEnumerator<T> originalEnumerator,
            int explosiveIndex,
            CounterInt32 visitedItemsNumber)
        {
            return new ExplosiveEnumerator<T>(
                originalEnumerator: originalEnumerator,
                explosiveIndex: explosiveIndex,
                visitedItemsNumber: visitedItemsNumber
            );
        }
    }
}
