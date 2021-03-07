using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class MinForGenericTypesWithComparerTests
    {
        public MinForGenericTypesWithComparerTests()
        {
        }

        [Fact]
        public void Min_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.Min(Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Min_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();

            // Act.
            int actualValue = collectionWithRandomSize.Min(comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.Min(Comparer<int>.Default)
            );
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.Min(Comparer<int?>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.Min(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[0];

            // Act.
            int actualValue = predefinedCollection.Min(Comparer<int>.Default);

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
        public void Min_WithComparer_ForCollectionWithSomeItems_ShouldReturnMin(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Min();

            // Act.
            int actualValue = collectionWithSomeItems.Min(Comparer<int>.Default);

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
        public void Min_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();

            // Act.
            int actualValue = collectionWithRandomSize.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Min();

            // Act.
            int actualValue = explosiveCollection.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
