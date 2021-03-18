﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class MinByKeyTests
    {
        public MinByKeyTests()
        {
        }

        [Fact]
        public void MinBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinBy(IdentityFunction<int>.Instance)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            const Func<int, int>? keySelector = null;
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinBy(keySelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void MinBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinBy(keySelector: default, Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
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

        [Fact]
        public void MinBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = predefinedCollection.MinBy(
                InverseFunction.Int32, Comparer<int>.Default
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
            int minValue = predefinedCollection.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = predefinedCollection.MinBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void MinBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void MinBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void MinBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.MinBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
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
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(InverseFunction.Int32);

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
            int minValue = collectionWithRandomSize.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = explosiveCollection.MinBy(InverseFunction.Int32);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosiveCollection.Min(InverseFunction.Int32);
            int expectedValue = InverseFunction.Int32(minValue);

            // Act.
            int actualValue = explosiveCollection.MinBy(
                InverseFunction.Int32, Comparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
