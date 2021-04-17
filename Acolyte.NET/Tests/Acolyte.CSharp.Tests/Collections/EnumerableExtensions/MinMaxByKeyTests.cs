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
    public sealed class MinMaxByKeyTests
    {
        public MinMaxByKeyTests()
        {
        }

        #region Null Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(DiscardFunction<int>.Func)
            );
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinMaxBy(keySelector!)
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.MinMaxBy(DiscardFunction<int>.Func, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinMaxBy(keySelector: null!, Comparer<int>.Default)
            );
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

        #endregion

        #region Empty Values

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

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.ForInt32);
            int maxValue = predefinedCollection.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.ForInt32);
            int maxValue = predefinedCollection.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
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
        public void MinMaxBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.ForInt32);
            int maxValue = collectionWithSomeItems.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(InverseFunction.ForInt32);

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
        public void MinMaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.ForInt32);
            int maxValue = collectionWithSomeItems.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
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
        public void MinMaxBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            (int minValue, int maxValue) expectedValue = (count, count);

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(InverseFunction.ForInt32);

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
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMinMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.ForInt32);
            int maxValue = collectionWithRandomSize.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(InverseFunction.ForInt32);

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
            int minValue = collectionWithRandomSize.Min(InverseFunction.ForInt32);
            int maxValue = collectionWithRandomSize.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMaxBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.ForInt32);
            int maxValue = explosiveCollection.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = explosiveCollection.MinMaxBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.ForInt32);
            int maxValue = explosiveCollection.Max(InverseFunction.ForInt32);
            (int minValue, int maxValue) expectedValue =
                (InverseFunction.ForInt32(minValue), InverseFunction.ForInt32(maxValue));

            // Act.
            var actualValue = explosiveCollection.MinMaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
