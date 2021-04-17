﻿#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;
using Acolyte.Functions;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ForEachTests
    {
        public ForEachTests()
        {
        }

        #region Null Values

        [Fact]
        public void ForEach_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Action<int> discard = DiscardFunction<int>.Action;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.ForEach(discard));
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ForEach_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = emptyCollection.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            emptyCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ForEach_ForPredefinedCollection_ShouldDoActionForEachItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            IReadOnlyList<int> expectedCollection = predefinedCollection.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            predefinedCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
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
        public void ForEach_ForCollectionWithSomeItems_ShouldDoActionForEachItem(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> expectedCollection = collectionWithSomeItems.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            collectionWithSomeItems.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ForEach_ForCollectionWithRandomSize_ShouldDoActionForEachItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> expectedCollection = collectionWithRandomSize.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            collectionWithRandomSize.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ForEach_ForCollectionWithRandomNumberAndNullValues_ShouldDoActionForEachItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int?> collectionWithRandomSize =
                TestDataCreator.CreateRandomNullableInt32List(count);
            IReadOnlyList<int?> expectedCollection = collectionWithRandomSize.ToList();
            var actualCollection = new List<int?>();
            Action<int?> action = i => actualCollection.Add(i);

            // Act.
            collectionWithRandomSize.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ForEach_ShouldLookWholeCollectionToDoActionForEachItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            IReadOnlyList<int> expectedCollection = explosiveCollection.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            explosiveCollection.ForEach(action);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion
    }
}
