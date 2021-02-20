using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class MinMaxByKeyTests
    {
        public MinMaxByKeyTests()
        {
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMaxBy(IdentityFunction<int>.Instance)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinMaxBy(keySelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue.MinMaxBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinMaxBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int minValue, int maxValue) expectedValue =
                   (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = 
                collectionWithRandomSize.MinMaxBy(IdentityFunction<int>.Instance, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMaxBy(IdentityFunction<int>.Instance)
            );
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMaxBy(IdentityFunction<int?>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMaxBy(IdentityFunction<string>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMaxBy(
                    IdentityFunction<int>.Instance, Comparer<int>.Default
                )
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMaxBy(
                IdentityFunction<int?>.Instance, Comparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMaxBy(
                IdentityFunction<string>.Instance, Comparer<string>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.Int32);
            int maxValue = predefinedCollection.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.Int32);
            int maxValue = predefinedCollection.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(
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
        public void MinMaxBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.Int32);
            int maxValue = collectionWithSomeItems.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(InverseFunction.Int32);

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
        public void MinMaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.Int32);
            int maxValue = collectionWithSomeItems.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(
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
        public void MinMaxBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            (int minValue, int maxValue) expectedValue = (count, count);

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(InverseFunction.Int32);

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
        public void MinMaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            (int minValue, int maxValue) expectedValue = (count, count);

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMinMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.Int32);
            int maxValue = collectionWithRandomSize.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.Int32);
            int maxValue = collectionWithRandomSize.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.Int32);
            int maxValue = explosiveCollection.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = explosiveCollection.MinMaxBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.Int32);
            int maxValue = explosiveCollection.Max(InverseFunction.Int32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.Int32(minValue), InverseFunction.Int32(maxValue));

            // Act.
            var actualValue = explosiveCollection.MinMaxBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
