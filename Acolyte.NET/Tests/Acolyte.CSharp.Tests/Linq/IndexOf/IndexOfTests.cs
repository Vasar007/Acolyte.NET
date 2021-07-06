using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.IndexOf
{
    public sealed partial class IndexOfTests
    {
        public IndexOfTests()
        {
        }

        #region Null Values

        [Fact]
        public void IndexOf_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.IndexOf(discard));
        }

        [Fact]
        public void IndexOf_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.IndexOf(predicate: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void IndexOf_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> discard = DiscardFunction<int, bool>.Func;
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(discard);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void IndexOf_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(i => i.Equals(expectedItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Random Values

        [Fact]
        public void IndexOf_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void IndexOf_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int expectedValue = collection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosive.IndexOf(i => i.Equals(expectedValue));

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 2));
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosive.IndexOf(_ => false);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualIndex);
        }

        #endregion
    }
}
