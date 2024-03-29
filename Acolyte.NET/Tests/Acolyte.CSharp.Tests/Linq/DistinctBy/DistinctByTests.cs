﻿using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Ranges;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.DistinctBy
{
    public sealed partial class DistinctByTests
    {
        public DistinctByTests()
        {
        }

        #region Null Values

        [Fact]
        public void DistinctBy_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discardKeySelector = DiscardFunction<int, bool>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.DistinctBy(discardKeySelector).ToList()
            );
        }

        [Fact]
        public void DistinctBy_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, bool>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.DistinctBy(keySelector!).ToList()
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void DistinctBy_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);

            // Act.
            var actualResult = emptyCollection.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(emptyCollection, actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void DistinctBy_ForPredefinedCollection_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            var expectedResult = GetExpectedResult(predefinedCollection, keySelector);

            // Act.
            var actualResult = predefinedCollection.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void DistinctBy_ForCollectionWithSomeItems_ShouldSelectUniqueItemsByKeySelector(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            var expectedResult = GetExpectedResult(collectionWithSomeItems, keySelector);

            // Act.
            var actualResult = collectionWithSomeItems.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Random Values

        [Fact]
        public void DistinctBy_ForCollectionWithRandomSize_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            var expectedResult = GetExpectedResult(collectionWithRandomSize, keySelector);

            // Act.
            var actualResult = collectionWithRandomSize.DistinctBy(keySelector);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void DistinctBy_ShouldLookWholeCollectionToSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(collection, keySelector).ToList();

            // Act.
            var actualResult = explosive.DistinctBy(keySelector).ToList();

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Private Methods

        private static IEnumerable<TSource> GetExpectedResult<TSource, TKey>(
            IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element))) yield return element;
            }
        }

        private static IReadOnlyList<long> CreateRange()
        {
            const int rangeCount = 50;
            IEnumerable<long> range = RangeFactory.StartsWith(1L, rangeCount);
            return range.ToList();
        }

        private static long MapToValueInRange(int sourceItem, IReadOnlyList<long> range)
        {
            if (sourceItem < 0)
            {
                sourceItem = -sourceItem;
            }

            return range[sourceItem % range.Count];
        }

        #endregion
    }
}
