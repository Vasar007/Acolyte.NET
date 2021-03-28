using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class FirstOrDefaultTests
    {
        public FirstOrDefaultTests()
        {
        }

        [Fact]
        public void FirstOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.FirstOrDefault(defaultValue: default)
            );
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.FirstOrDefault(discard, defaultValue: default)
            );
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate",
                () => emptyCollection.FirstOrDefault(predicate: null!, defaultValue: default)
            );
        }

        [Fact]
        public void FirstOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_ForPredefinedCollection_ShouldReturnFirstItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[0];

            // Act.
            int actualValue = predefinedCollection.FirstOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnFirstItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void FirstOrDefault_ForCollectionWithSomeItems_ShouldReturnFirstItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.First();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnFirstItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.First();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_ForCollectionWithRandomSize_ShouldReturnFirstOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First()
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnFirstOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First(predicate)
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(predicate, defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            const int count = 2;
            var explosiveCollection = ExplosiveCollection.Create(
                TestDataCreator.CreateRandomInt32List(count),
                explosiveIndex: Constants.FirstIndex + 1
            );
            int expectedValue = explosiveCollection.First();

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expected: 1, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            int expectedIndex = Constants.FirstIndex + 1;
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: expectedIndex + 1
            );
            int expectedValue = collection[expectedIndex];

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = -1;

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
