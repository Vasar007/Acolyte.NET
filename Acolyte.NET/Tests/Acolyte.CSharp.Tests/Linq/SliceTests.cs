using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class SliceTests
    {
        public SliceTests()
        {
        }

        #region Null Values

        [Fact]
        public void SliceIfRequired_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.SliceIfRequired(startIndex: default, count: default)
            );
        }

        #endregion

        #region Empty Values

        [Theory]
        [ClassData(typeof(CartesianProductOfPositiveAndNegativeWithZeroTestCases))]
        public void SliceIfRequired_ForEmptyCollection_ShouldDoNothing(int? startIndex, int? count)
        {
            // Arrange.
            var emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualCollection = emptyCollection.SliceIfRequired(startIndex, count);

            // Assert.
            Assert.Equal(emptyCollection, actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void SliceIfRequired_ForPredefinedCollection_ShouldIgnoreNullParameters()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int? startIndex = null;
            int? count = null;
            IReadOnlyList<int> expectedCollection = predefinedCollection.ToList();

            // Act.
            var actualCollection = predefinedCollection.SliceIfRequired(startIndex, count);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void SliceIfRequired_ForPredefinedCollection_ShouldIgnoreNullStartIndex()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int? startIndex = null;
            const int count = 2;
            IReadOnlyList<int> expectedCollection = predefinedCollection
                .Take(count)
                .ToList();

            // Act.
            var actualCollection = predefinedCollection.SliceIfRequired(startIndex, count);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void SliceIfRequired_ForPredefinedCollection_ShouldIgnoreNullCount()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            const int startIndex = 1;
            int? count = null;
            IReadOnlyList<int> expectedCollection = predefinedCollection
                .Skip(startIndex)
                .ToList();

            // Act.
            var actualCollection = predefinedCollection.SliceIfRequired(startIndex, count);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void SliceIfRequired_ForPredefinedCollection_ShouldSliceByIndexAndCount()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            const int startIndex = 1;
            const int count = 2;
            IReadOnlyList<int> expectedCollection = predefinedCollection
                .Skip(startIndex)
                .Take(count)
                .ToList();

            // Act.
            var actualCollection = predefinedCollection.SliceIfRequired(startIndex, count);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void SliceIfRequired_ForCollectionWithSomeItems_ShouldSliceByIndex(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);

            int countParameter = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> expectedCollection = collectionWithSomeItems
                .Take(countParameter)
                .ToList();

            // Act.
            var actualCollection = collectionWithSomeItems
                .SliceIfRequired(startIndex: null, countParameter);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void SliceIfRequired_ForCollectionWithSomeItems_ShouldSliceByCount(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);

            int startIndex = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> expectedCollection = collectionWithSomeItems
                .Skip(startIndex)
                .ToList();

            // Act.
            var actualCollection = collectionWithSomeItems
                .SliceIfRequired(startIndex, count: null);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void SliceIfRequired_ForCollectionWithSomeItems_ShouldSliceByIndexAndCount(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);

            int startIndex = TestDataCreator.CreateRandomNonNegativeInt32(count);
            int countParameter = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> expectedCollection = collectionWithSomeItems
                .Skip(startIndex)
                .Take(countParameter)
                .ToList();

            // Act.
            var actualCollection = collectionWithSomeItems
                .SliceIfRequired(startIndex, countParameter);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void SliceIfRequired_ForCollectionWithRandomSize_ShouldSliceByIndexAndCount()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);

            int startIndex = TestDataCreator.CreateRandomNonNegativeInt32(count);
            int countParameter = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> expectedCollection = collectionWithRandomSize
                .Skip(startIndex)
                .Take(countParameter)
                .ToList();

            // Act.
            var actualCollection = collectionWithRandomSize
                .SliceIfRequired(startIndex, countParameter);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void SliceIfRequired_ShouldLookWholeCollectionToSlice()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            const int startIndex = 1;
            const int count = 2;
            IReadOnlyList<int> expectedCollection = explosive
                .Skip(startIndex)
                .Take(count)
                .ToList();

            // Act.
            var actualCollection = explosive.SliceIfRequired(startIndex, count);

            // Assert.
            // We should take into account "Skip" and "Take" methods.
            // When "Take" method finished enumeration, we ignore the remaining items.
            int expectedVisitedItemsNumber = expectedCollection.Count + startIndex;
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber));
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion
    }
}
