using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.ForEach
{
    public sealed partial class ForEachTests
    {
        #region Null Values

        [Fact]
        public void ForEach_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Action<int, int> discard = DiscardAction<int, int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.ForEach(discard));
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ForEach_WithIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = emptyCollection.ToReadOnlyList();
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            emptyCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ForEach_WithIndex_ForPredefinedCollection_ShouldDoActionForEachItem()
        {
            // Arrange.
            IReadOnlyList<int> expectedCollection = new[] { 1, 2, 3 };
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            expectedCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ForEach_WithIndex_ForCollectionWithSomeItems_ShouldDoActionForEachItem(int count)
        {
            // Arrange.
            IReadOnlyList<int> expectedCollection =
                TestDataCreator.CreateRandomInt32List(count);
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            expectedCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ForEach_WithIndex_ForCollectionWithRandomSize_ShouldDoActionForEachItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> expectedCollection = TestDataCreator.CreateRandomInt32List(count);
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            expectedCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ForEach_WithIndex_ForCollectionWithRandomNumberAndNullValues_ShouldDoActionForEachItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int?> expectedCollection =
                TestDataCreator.CreateRandomNullableInt32List(count);
            var actualCollection = new List<int?>();
            Action<int?, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            expectedCollection.ForEach(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ForEach_WithIndex_ShouldLookWholeCollectionToDoActionForEachItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            IReadOnlyList<int> expectedCollection = collection.ToList();
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(item);

            // Act.
            explosive.ForEach(action);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ForEach_WithIndex_ShouldPassToPredicateValidIndices()
        {
            // Arrange. indexes
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            IReadOnlyList<int> expectedCollection = Enumerable.Range(0, collection.Count).ToList();
            var actualCollection = new List<int>();
            Action<int, int> action = (item, index) => actualCollection.Add(index);

            // Act.
            explosive.ForEach(action);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion
    }
}
