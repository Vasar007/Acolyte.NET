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
    public sealed class MinByKeyTests
    {
        public MinByKeyTests()
        {
        }

        #region Null Values

        [Fact]
        public void MinBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinBy(DiscardFunction<int>.Func)
            );
        }

        [Fact]
        public void MinBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinBy(keySelector!)
            );
        }

        [Fact]
        public void MinBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinBy(DiscardFunction<int>.Func, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MinBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinBy(keySelector: null!, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MinBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();

            // Act.
            int actualValue =
                collectionWithRandomSize.MinBy(IdentityFunction<int>.Instance, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinBy(IdentityFunction<int>.Instance)
            );
        }

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.MinBy(IdentityFunction<int?>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.MinBy(IdentityFunction<string>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinBy(IdentityFunction<int>.Instance, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.MinBy(
                IdentityFunction<int?>.Instance, Comparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.MinBy(
                IdentityFunction<string>.Instance, Comparer<string>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = predefinedCollection.MinBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]

        public void MinBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = predefinedCollection.MinBy(InverseFunction.ForInt32);

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
        public void MinBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(InverseFunction.ForInt32);

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
        public void MinBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(
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
        public void MinBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MinBy(InverseFunction.ForInt32);

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
        public void MinBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MinBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = explosiveCollection.MinBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(minValue);

            // Act.
            int actualValue = explosiveCollection.MinBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
