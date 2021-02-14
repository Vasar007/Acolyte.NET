using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Common;
using Acolyte.Tests;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;

namespace Acolyte.Collections.Tests.EnumerableExtensions
{
    public sealed class MaxByKeyTests
    {
        public MaxByKeyTests()
        {
        }

        [Fact]
        public void Call_MaxBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MaxBy(IdentityFunction<int>.Instance)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MaxBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MaxBy(keySelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MaxBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MaxBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Max();

            // Act.
            int actualValue =
                collectionWithRandomSize.MaxBy(IdentityFunction<int>.Instance, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void Call_MaxBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MaxBy(IdentityFunction<int>.Instance)
            );
        }

        [Fact]
        public void Call_MaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.MaxBy(IdentityFunction<int?>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.MaxBy(IdentityFunction<string>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MaxBy(IdentityFunction<int>.Instance, Comparer<int>.Default)
            );
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.MaxBy(
                IdentityFunction<int?>.Instance, Comparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.MaxBy(
                IdentityFunction<string>.Instance, Comparer<string>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void Call_MaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = predefinedCollection.MaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]

        public void Call_MaxBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = predefinedCollection.MaxBy(InverseFunction.Int32);

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
        public void Call_MaxBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMax(
    int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(InverseFunction.Int32);

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
        public void Call_MaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(
                InverseFunction.Int32, Comparer<int>.Default
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
        public void Call_MaxBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(InverseFunction.Int32);

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
        public void Call_MaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MaxBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MaxBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = explosiveCollection.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = explosiveCollection.MaxBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = explosiveCollection.Max(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(maxValue);

            // Act.
            int actualValue = explosiveCollection.MaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
