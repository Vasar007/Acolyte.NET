using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class LastOrDefaultTests
    {
        public LastOrDefaultTests()
        {
        }

        [Fact]
        public void LastOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.LastOrDefault(default(int))
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.LastOrDefault(default, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.LastOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void LastOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.LastOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.LastOrDefault(_ => default, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_ForPredefinedCollection_ShouldReturnLastItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[^1];

            // Act.
            int actualValue = predefinedCollection.LastOrDefault(default(int));

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnLastItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.LastOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void LastOrDefault_ForCollectionWithSomeItems_ShouldReturnLastItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Last();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(default(int));

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnLastItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Last();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_ForCollectionWithRandomSize_ShouldReturnLastOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last()
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnLastOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(predicate, defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last(predicate)
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_ShouldLookWholeCollectionToFindLastItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: collection.Count
            );
            int expectedValue = collection[^1];

            // Act.
            int actualValue = explosiveCollection.LastOrDefault(default(int));

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItemAfterItFoundSomething()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = collection[1];

            // Act.
            int actualValue = explosiveCollection.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = collection[1];

            // Act.
            int actualValue = explosiveCollection.LastOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
