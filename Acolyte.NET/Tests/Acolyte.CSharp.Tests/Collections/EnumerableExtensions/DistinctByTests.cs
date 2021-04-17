#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;
using Acolyte.Functions;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class DistinctByTests
    {
        public DistinctByTests()
        {
        }

        #region Null Values

        [Fact]
        public void DistinctBy_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discardKeySelector = DiscardFunction<int, bool>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.DistinctBy(discardKeySelector).ToList()
            );
        }

        [Fact]
        public void DistinctBy_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, bool>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.DistinctBy(keySelector!).ToList()
            );
        }

        [Fact]
        public void DistinctBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discardKeySelector = DiscardFunction<int, bool>.Func;
            var keyComparer = EqualityComparer<bool>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.DistinctBy(discardKeySelector, keyComparer).ToList()
            );
        }

        [Fact]
        public void DistinctBy_WithComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, bool>? keySelector = null;
            var keyComparer = EqualityComparer<bool>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.DistinctBy(keySelector!, keyComparer).ToList()
            );
        }

        [Fact]
        public void DistinctBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> discardKeySelector = DiscardFunction<int, bool>.Func;
            var expectedResult = Enumerable.Empty<int>();

            // Act.
            var actualResult = emptyCollection.DistinctBy(discardKeySelector, keyComparer: null);

            // Assert.
            Assert.NotNull(actualResult);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void DistinctBy_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = Enumerable.Empty<int>();

            // Act.
            var actualResult = emptyCollection.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DistinctBy_WithComparer_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = Enumerable.Empty<int>();
            var keyComparer = EqualityComparer<bool>.Default;

            // Act.
            var actualResult = emptyCollection.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void DistinctBy_ForPredefinedCollection_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(predefinedCollection, keySelector);

            // Act.
            var actualResult = predefinedCollection.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DistinctBy_WithComparer_ForPredefinedCollection_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = EqualityComparer<bool>.Default;
            var expectedResult = GetExpectedResult(predefinedCollection, keySelector, keyComparer);

            // Act.
            var actualResult = predefinedCollection.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
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
        public void DistinctBy_ForCollectionWithSomeItems_ShouldSelectUniqueItemsByKeySelector(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(collectionWithSomeItems, keySelector);

            // Act.
            var actualResult = collectionWithSomeItems.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void DistinctBy_WithComparer_ForCollectionWithSomeItems_ShouldSelectUniqueItemsByKeySelector(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = EqualityComparer<bool>.Default;
            var expectedResult = GetExpectedResult(
                collectionWithSomeItems, keySelector, keyComparer
            );

            // Act.
            var actualResult = collectionWithSomeItems.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Random Values

        [Fact]
        public void DistinctBy_ForCollectionWithRandomSize_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(collectionWithRandomSize, keySelector);

            // Act.
            var actualResult = collectionWithRandomSize.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DistinctBy_WithComparer_ForCollectionWithRandomSize_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = EqualityComparer<bool>.Default;
            var expectedResult = GetExpectedResult(
                collectionWithRandomSize, keySelector, keyComparer
            );

            // Act.
            var actualResult = collectionWithRandomSize.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void DistinctBy_ShouldLookWholeCollectionToSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(explosiveCollection, keySelector).ToList();

            // Act.
            var actualResult = explosiveCollection.DistinctBy(keySelector).ToList();

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DistinctBy_WithComparer_ShouldLookWholeCollectionToSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = EqualityComparer<bool>.Default;
            var expectedResult =
                GetExpectedResult(explosiveCollection, keySelector, keyComparer).ToList();

            // Act.
            var actualResult = explosiveCollection.DistinctBy(keySelector, keyComparer).ToList();

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        private static IEnumerable<TSource> GetExpectedResult<TSource, TKey>(
            IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey>? keyComparer = null)
        {
            var hashSet = new HashSet<TKey>(keyComparer);
            return source.Where(item => hashSet.Add(keySelector(item)));
        }
    }
}
