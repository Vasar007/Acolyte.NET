using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class MinForGenericTypesWithComparerTests
    {
        public MinForGenericTypesWithComparerTests()
        {
        }

        #region Null Values

        [Fact]
        public void Min_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            var comparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.Min(comparer));
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();

            // Act.
            int actualValue = collectionWithRandomSize.Min(comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var comparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.Min(comparer));
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;
            var comparer = MockComparer<int?>.Default;

            // Act.
            int? actualValue = emptyCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;
            var comparer = MockComparer<string>.Default;

            // Act.
            string? actualValue = emptyCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            comparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[0];
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            int actualValue = predefinedCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, predefinedCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void Min_WithComparer_ForCollectionWithSomeItems_ShouldReturnMin(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Min();
            var comparer = MockComparer.SetupDefaultFor(collectionWithSomeItems);

            // Act.
            int actualValue = collectionWithSomeItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithSomeItems);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void Min_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            int expectedValue = count;
            var comparer = MockComparer.SetupDefaultFor(collectionWithTheSameItems);

            // Act.
            int actualValue = collectionWithTheSameItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void Min_WithComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();
            var comparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithRandomSize.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void Min_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = explosive.Min();
            var comparer = MockComparer.SetupDefaultFor(collection);

            // Act.
            int actualValue = explosive.Min(comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collection);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMin<T>(MockComparer<T> comparer,
            IReadOnlyList<T> collection)
        {
            if (collection.Count > 0)
            {
                comparer.VerifyCompareCalls(times: collection.Count - 1);
            }
            else
            {
                comparer.VerifyNoCalls();
            }
        }

        #endregion
    }
}
