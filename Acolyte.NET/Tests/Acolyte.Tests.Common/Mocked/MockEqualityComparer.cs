using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Acolyte.Assertions;
using Moq;

namespace Acolyte.Tests.Mocked
{
    public sealed class MockEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Mock<IEqualityComparer<T>> _mockedComparer;

        private readonly IEnumerable<T>? _source;

        private readonly IEqualityComparer<T> _sourceComparer;

        public static MockEqualityComparer<T> Default =>
            MockEqualityComparer.Setup(EqualityComparer<T>.Default);


        internal MockEqualityComparer(
            Mock<IEqualityComparer<T>> mockedComparer,
            IEnumerable<T>? source,
            IEqualityComparer<T> sourceComparer)
        {
            _mockedComparer = mockedComparer.ThrowIfNull(nameof(mockedComparer));
            _source = source;
            _sourceComparer = sourceComparer.ThrowIfNull(nameof(sourceComparer));
        }

        #region IEqualityComparer<T> Implementation

        public bool Equals(T x, T y)
        {
            return _mockedComparer.Object.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _mockedComparer.Object.GetHashCode(obj);
        }

        #endregion

        #region Verify Methods

        public void VerifyNoCalls()
        {
            VerifyEqualsNoCalls();
            VerifyGetHashCodeNoCalls();
        }

        #region Equals

        private void VerifyEqualsCallsInternal(Times times)
        {
            _mockedComparer.Verify(
                MockEqualityComparer.GetSetupEqualsFunction(_source, _sourceComparer), times
            );
        }

        public void VerifyEqualsNoCalls()
        {
            VerifyEqualsCallsInternal(Times.Never());
        }

        public void VerifyEqualsCalls(int times)
        {
            VerifyEqualsCallsInternal(Times.Exactly(times));
        }

        public void VerifyEqualsCallsForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyEqualsCallsInternal(Times.Exactly(collection.Count));
        }

        public void VerifyEqualsCallsTwiceForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyEqualsCallsInternal(Times.Exactly(collection.Count * 2));
        }

        #endregion

        #region GetHashCode

        private void VerifyGetHashCodeCallsInternal(Times times)
        {
            _mockedComparer.Verify(
                MockEqualityComparer.GetSetupGetHashCodeFunction(_source, _sourceComparer), times
            );
        }

        public void VerifyGetHashCodeNoCalls()
        {
            VerifyGetHashCodeCallsInternal(Times.Never());
        }

        public void VerifyGetHashCodeCalls(int times)
        {
            VerifyGetHashCodeCallsInternal(Times.Exactly(times));
        }

        public void VerifyGetHashCodeCallsForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyGetHashCodeCallsInternal(Times.Exactly(collection.Count));
        }

        public void VerifyGetHashCodeCallsTwiceForEach<TItem>(IReadOnlyCollection<TItem> collection)
        {
            VerifyGetHashCodeCallsInternal(Times.Exactly(collection.Count * 2));
        }

        #endregion

        #endregion
    }

    public static class MockEqualityComparer
    {
        public static MockEqualityComparer<T> SetupDefaultFor<T>(
           IEnumerable<T> source)
        {
            source.ThrowIfNull(nameof(source));
            return Setup(EqualityComparer<T>.Default, source);
        }

        public static MockEqualityComparer<T> Setup<T>(
            IEqualityComparer<T> originalComparer,
            IEnumerable<T>? source = null)
        {
            originalComparer.ThrowIfNull(nameof(originalComparer));

            source = source?.ToArray();
            var mock = new Mock<IEqualityComparer<T>>(MockBehavior.Strict);

            // Setup functions.
            mock.Setup(GetSetupEqualsFunction(source, originalComparer))
                .Returns((T x, T y) => originalComparer.Equals(x, y));

            mock.Setup(GetSetupGetHashCodeFunction(source, originalComparer))
                .Returns((T obj) => originalComparer.GetHashCode(obj));

            // Return wrapper.
            return new MockEqualityComparer<T>(mock, source, originalComparer);
        }

        #region Setup Functions

        internal static Expression<Func<IEqualityComparer<T>, bool>> GetSetupEqualsFunction<T>(
            IEnumerable<T>? source, IEqualityComparer<T> sourceComparer)
        {
            if (source is null)
            {
                return comparer => comparer.Equals(It.IsAny<T>(), It.IsAny<T>());
            }

            return comparer => comparer.Equals(
                It.IsIn(source, sourceComparer), It.IsIn(source, sourceComparer)
            );
        }

        internal static Expression<Func<IEqualityComparer<T>, int>> GetSetupGetHashCodeFunction<T>(
             IEnumerable<T>? source, IEqualityComparer<T> sourceComparer)
        {
            if (source is null)
            {
                return comparer => comparer.GetHashCode(It.IsAny<T>());
            }

            return comparer => comparer.GetHashCode(It.IsIn(source, sourceComparer));
        }

        #endregion
    }
}
