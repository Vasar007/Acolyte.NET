using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Acolyte.Assertions;
using Moq;

namespace Acolyte.Tests.Mocked
{
    public sealed class MockComparer<T> : IComparer<T>
    {
        private readonly Mock<IComparer<T>> _mockedComparer;

        private readonly IEnumerable<T>? _source;

        private readonly IEqualityComparer<T> _sourceComparer;

        public static MockComparer<T> Default => MockComparer.Setup(Comparer<T>.Default);


        internal MockComparer(
            Mock<IComparer<T>> mockedComparer,
            IEnumerable<T>? source,
            IEqualityComparer<T> sourceComparer)
        {
            _mockedComparer = mockedComparer.ThrowIfNull(nameof(mockedComparer));
            _source = source;
            _sourceComparer = sourceComparer.ThrowIfNull(nameof(sourceComparer));
        }

        #region IComparer<T> Implementation

        public int Compare(T x, T y)
        {
            return _mockedComparer.Object.Compare(x, y);
        }

        #endregion

        #region Verify Methods

        public void VerifyNoCalls()
        {
            VerifyCompareNoCalls();
        }

        #region Compare

        private void VerifyCompareCallsInternal(Times times)
        {
            _mockedComparer.Verify(
                MockComparer.GetSetupCompareFunction(_source, _sourceComparer), times
            );
        }

        public void VerifyCompareNoCalls()
        {
            VerifyCompareCallsInternal(Times.Never());
        }

        public void VerifyCompareCalls(int times)
        {
            VerifyCompareCallsInternal(Times.Exactly(times));
        }

        public void VerifyCompareCallsOnceForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyCompareCallsInternal(Times.Exactly(collection.Count));
        }

        public void VerifyCompareCallsTwiceForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyCompareCallsInternal(Times.Exactly(collection.Count * 2));
        }

        #endregion

        #endregion
    }

    public static class MockComparer
    {
        public static MockComparer<T> SetupDefaultFor<T>(
            IEnumerable<T> source,
            IEqualityComparer<T>? sourceComparer = null)
        {
            source.ThrowIfNull(nameof(source));
            return Setup(Comparer<T>.Default, source, sourceComparer);
        }

        public static MockComparer<T> Setup<T>(
            IComparer<T> originalComparer,
            IEnumerable<T>? source = null,
            IEqualityComparer<T>? sourceComparer = null)
        {
            originalComparer.ThrowIfNull(nameof(originalComparer));
            sourceComparer ??= EqualityComparer<T>.Default;

            source = source?.ToArray();
            var mock = new Mock<IComparer<T>>(MockBehavior.Strict);

            // Setup functions.
            mock.Setup(GetSetupCompareFunction(source, sourceComparer))
                .Returns((T x, T y) => originalComparer.Compare(x, y));

            // Return wrapper.
            return new MockComparer<T>(mock, source, sourceComparer);
        }

        #region Setup Functions

        internal static Expression<Func<IComparer<T>, int>> GetSetupCompareFunction<T>(
            IEnumerable<T>? source, IEqualityComparer<T> sourceComparer)
        {
            if (source is null)
            {
                return comparer => comparer.Compare(It.IsAny<T>(), It.IsAny<T>());
            }

            return comparer => comparer.Compare(
                It.IsIn(source, sourceComparer), It.IsIn(source, sourceComparer)
            );
        }

        #endregion
    }
}
