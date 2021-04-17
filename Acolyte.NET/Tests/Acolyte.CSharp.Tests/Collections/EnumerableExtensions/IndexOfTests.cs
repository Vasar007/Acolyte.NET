#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class IndexOfTests
    {
        public IndexOfTests()
        {
        }

        #region Null Values

        [Fact]
        public void IndexOf_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.IndexOf(discard));
        }

        [Fact]
        public void IndexOf_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.IndexOf(predicate: null!)
            );
        }

        [Fact]
        public void IndexOf_Item_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.IndexOf(value: default)
            );
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.IndexOf(value: default, EqualityComparer<int>.Default)
            );
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem, comparer: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void IndexOf_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(discard);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(value: default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(
                value: default, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void IndexOf_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(i => i.Equals(expectedItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(expectedItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(
                expectedItem, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
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
        public void IndexOf_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(
                randomItem, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void IndexOf_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(value: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems = TestDataCreator
                 .CreateRandomInt32List(count)
                 .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(
                value: null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Random Values

        [Fact]
        public void IndexOf_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(
                randomItem, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
               .CreateRandomInt32List(count)
               .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(value: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(
                value: null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void IndexOf_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int expectedValue = explosive.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosive.IndexOf(i => i.Equals(expectedValue));

            // Assert.
            Assert.Equal(expected: 2, explosive.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosive.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expected: collection.Count, explosive.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = explosive.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosive.IndexOf(value);

            // Assert.
            Assert.Equal(expected: 2, explosive.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosive.IndexOf(value: 0);

            // Assert.
            Assert.Equal(expected: collection.Count, explosive.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = explosive.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosive.IndexOf(
                value, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: 2, explosive.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosive.IndexOf(value: 0, EqualityComparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Count, explosive.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        #endregion
    }
}
