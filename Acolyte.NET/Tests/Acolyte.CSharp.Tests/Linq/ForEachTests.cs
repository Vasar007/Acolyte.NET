using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq
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
        [ClassData(typeof(PositiveTestConstants))]
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            IReadOnlyList<int> expectedCollection = explosive.ToList();
            var actualCollection = new List<int>();
            Action<int> action = i => actualCollection.Add(i);

            // Act.
            explosive.ForEach(action);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion
    }
}
