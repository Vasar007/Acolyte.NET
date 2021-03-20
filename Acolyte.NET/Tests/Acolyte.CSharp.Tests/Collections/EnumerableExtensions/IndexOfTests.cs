using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class IndexOfTests
    {
        public IndexOfTests()
        {
        }

        [Fact]
        public void IndexOf_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.IndexOf(_ => default));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void IndexOf_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("predicate", () => emptyCollection.IndexOf(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void IndexOf_Item_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.IndexOf(default(int)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.IndexOf(default, EqualityComparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
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

        [Fact]
        public void IndexOf_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(_ => default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(default(int));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(default, EqualityComparer<int>.Default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Common.Constants.FirstIndex + 1;
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
            int expectedIndex = Common.Constants.FirstIndex + 1;
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
            int expectedIndex = Common.Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(
                expectedItem, EqualityComparer<int>.Default
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
            int expectedIndex = Common.Constants.NotFoundIndex;

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
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf((int?) null);

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
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(
                null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

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
            int expectedIndex = Common.Constants.NotFoundIndex;

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
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf((int?) null);

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
            int expectedIndex = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(
                null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Common.Constants.FirstIndex + 2
            );
            int expectedValue = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(i => i.Equals(expectedValue));

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Common.Constants.FirstIndex + 2
            );
            int value = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value);

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value: 0);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Common.Constants.FirstIndex + 2
            );
            int value = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(
                value, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Common.Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value: 0, EqualityComparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }
    }
}
