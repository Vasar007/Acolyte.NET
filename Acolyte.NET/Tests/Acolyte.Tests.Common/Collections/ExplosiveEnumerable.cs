using System;
using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Threading;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveEnumerable<T> : IEnumerable<T?>, IEnumerable
    {
        private readonly IEnumerable<T> _originalEnumerable;

        public int ExplosiveIndex { get; }

        private readonly CounterInt32 _visitedItemsNumber;
        public int VisitedItemsNumber => _visitedItemsNumber.Value;

        private readonly CounterInt32 _getEnumeratorCounter;
        public int GetEnumeratorCounter => _getEnumeratorCounter.Value;


        public ExplosiveEnumerable(
            IEnumerable<T> originalEnumerable,
            int explosiveIndex)
        {
            _originalEnumerable = originalEnumerable.ThrowIfNull(nameof(originalEnumerable));
            ExplosiveIndex = explosiveIndex.ThrowIfValueIsOutOfRange(
                nameof(explosiveIndex), Constants.NotFoundIndex, int.MaxValue
            );

            _visitedItemsNumber = new CounterInt32();
            _getEnumeratorCounter = new CounterInt32();
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            _getEnumeratorCounter.Increment();
            return ExplosiveEnumerator.Create(
                originalEnumerator: _originalEnumerable.GetEnumerator(),
                explosiveIndex: ExplosiveIndex,
                visitedItemsNumber: _visitedItemsNumber.Reset()
            );
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Verify Methods

        public Reasonable<bool> Verify(int expectedVisitedItemsNumber,
            int expectedGetEnumeratorCalls)
        {
            var expected = (expectedVisitedItemsNumber, expectedGetEnumeratorCalls);
            var actual = (VisitedItemsNumber, GetEnumeratorCounter);

            if (expected == actual) return Reasonable.Ok(true);

            string message =
                $"Verify    (VisitedItemsNumber, GetEnumeratorCounter){Environment.NewLine}" +
                $"Expected: {expected.ToString()}{Environment.NewLine}" +
                $"Actual:   {actual.ToString()}";
            return Reasonable.Wrap(false, message);
        }

        public Reasonable<bool> VerifyOnce(int expectedVisitedItemsNumber)
        {
            return Verify(expectedVisitedItemsNumber, expectedGetEnumeratorCalls: 1);
        }

        public Reasonable<bool> VerifyTwice(int expectedVisitedItemsNumber)
        {
            return Verify(expectedVisitedItemsNumber, expectedGetEnumeratorCalls: 2);
        }

        public Reasonable<bool> VerifyThrice(int expectedVisitedItemsNumber)
        {
            return Verify(expectedVisitedItemsNumber, expectedGetEnumeratorCalls: 3);
        }

        public Reasonable<bool> VerifyOnceEnumerateWholeCollection<TItem>(
           IReadOnlyCollection<TItem> collection)
        {
            return VerifyOnce(collection.Count);
        }

        public Reasonable<bool> VerifyTwiceEnumerateWholeCollection<TItem>(
            IReadOnlyCollection<TItem> collection)
        {
            return VerifyTwice(collection.Count);
        }

        public Reasonable<bool> VerifyThriceEnumerateWholeCollection<TItem>(
            IReadOnlyCollection<TItem> collection)
        {
            return VerifyThrice(collection.Count);
        }

        public Reasonable<bool> VerifyNoIterationsNoGetEnumeratorCalls()
        {
            return Verify(expectedVisitedItemsNumber: 0, expectedGetEnumeratorCalls: 0);
        }

        public Reasonable<bool> VerifyNoIterationsButWithGetEnumerator(
            int expectedGetEnumeratorCalls)
        {
            return Verify(expectedVisitedItemsNumber: 0, expectedGetEnumeratorCalls);
        }

        public Reasonable<bool> VerifyNoIterationsButWithOnceGetEnumerator()
        {
            return VerifyNoIterationsButWithGetEnumerator(expectedGetEnumeratorCalls: 1);
        }

        public Reasonable<bool> VerifyNoIterationsButWithTwiceGetEnumerator()
        {
            return VerifyNoIterationsButWithGetEnumerator(expectedGetEnumeratorCalls: 2);
        }

        public Reasonable<bool> VerifyNoIterationsButWithThriceGetEnumerator()
        {
            return VerifyNoIterationsButWithGetEnumerator(expectedGetEnumeratorCalls: 3);
        }

        #endregion
    }

    public static class ExplosiveEnumerable
    {
        public static ExplosiveEnumerable<T> Create<T>(
            IEnumerable<T> originalEnumerable,
            int explosiveIndex)
        {
            return new ExplosiveEnumerable<T>(
                originalEnumerable: originalEnumerable,
                explosiveIndex: explosiveIndex
            );
        }

        public static ExplosiveEnumerable<T> CreateNotExplosive<T>(
           IEnumerable<T> originalEnumerable)
        {
            return new ExplosiveEnumerable<T>(
                originalEnumerable: originalEnumerable,
                explosiveIndex: Constants.NotFoundIndex
            );
        }
    }
}
