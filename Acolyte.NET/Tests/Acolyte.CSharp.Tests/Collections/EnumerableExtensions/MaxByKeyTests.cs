﻿#pragma warning disable CS0618 // Type or member is obsolete

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
    public sealed class MaxByKeyTests
    {
        public MaxByKeyTests()
        {
        }

        #region Null Values

        [Fact]
        public void MaxBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MaxBy(DiscardFunction<int>.Func)
            );
        }

        [Fact]
        public void MaxBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MaxBy(keySelector!)
            );
        }

        [Fact]
        public void MaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MaxBy(DiscardFunction<int>.Func, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MaxBy(keySelector: null!, Comparer<int>.Default)
            );
        }

        [Fact]
        public void MaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
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


        #endregion

        #region Empty Values

        [Fact]
        public void MaxBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
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
        public void MaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
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
        public void MaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
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
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
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
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
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
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
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

        #endregion

        #region Predefined Values

        [Fact]
        public void MaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = predefinedCollection.MaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]

        public void MaxBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = predefinedCollection.MaxBy(InverseFunction.ForInt32);

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
        public void MaxBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(InverseFunction.ForInt32);

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
        public void MaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(
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
        public void MaxBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(InverseFunction.ForInt32);

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
        public void MaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MaxBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(
                InverseFunction.ForInt32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MaxBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = explosive.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = explosive.MaxBy(InverseFunction.ForInt32);

            // Assert.
            Assert.Equal(expected: collection.Count, explosive.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = explosive.Max(InverseFunction.ForInt32);
            int expectedValue = InverseFunction.ForInt32(maxValue);

            // Act.
            int actualValue = explosive.MaxBy(InverseFunction.ForInt32, Comparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Count, explosive.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
