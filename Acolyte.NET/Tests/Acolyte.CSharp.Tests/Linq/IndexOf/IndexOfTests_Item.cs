using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.IndexOf
{
    public sealed partial class IndexOfTests
    {
        #region Null Values

        [Fact]
        public void IndexOf_Item_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.IndexOf(value: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void IndexOf_Item_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(value: default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void IndexOf_Item_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];

            // Act.
            int actualIndex = predefinedCollection.IndexOf(expectedItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IReadOnlyList<int?> collectionWithSomeItems = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(value: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Random Values

        [Fact]
        public void IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int?> collectionWithRandomSize = TestDataCreator
               .CreateRandomInt32List(count)
               .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(value: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void IndexOf_Item_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = collection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosive.IndexOf(value);

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 2));
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosive.IndexOf(value: 0);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualIndex);
        }

        #endregion
    }
}
