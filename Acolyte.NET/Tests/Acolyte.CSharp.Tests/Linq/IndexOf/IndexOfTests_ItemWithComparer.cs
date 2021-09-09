using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq.IndexOf
{
    public sealed partial class IndexOfTests
    {
        #region Null Values

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.IndexOf(value: default, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem, comparer: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void IndexOf_ItemWithComparer_ForEmptyCollection_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;
            var comparer = MockEqualityComparer<int>.Default;

            // Act.
            int actualIndex = emptyCollection.IndexOf(value: default, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
            comparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void IndexOf_ItemWithComparer_ForPredefinedCollection_ShouldReturnIndexOfSecondItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedIndex = Constants.FirstIndex + 1;
            int expectedItem = predefinedCollection[expectedIndex];
            var comparer = MockEqualityComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            int actualIndex = predefinedCollection.IndexOf(expectedItem, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
            comparer.VerifyEqualsCalls(times: expectedIndex + 1);
            comparer.VerifyGetHashCodeNoCalls();
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);
            var comparer = MockEqualityComparer.SetupDefaultFor(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(randomItem, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
            comparer.VerifyEqualsCalls(times: expectedIndex + 1);
            comparer.VerifyGetHashCodeNoCalls();
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IReadOnlyList<int?> collectionWithSomeItems = TestDataCreator
                 .CreateRandomInt32List(count)
                 .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;
            var comparer = MockEqualityComparer<int?>.Default;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(value: null, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
            comparer.VerifyEqualsCallsOnceForEach(collectionWithSomeItems);
            comparer.VerifyGetHashCodeNoCalls();
        }

        #endregion

        #region Random Values

        [Fact]
        public void IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);
            // Use default comparer to avoid long running tests.
            var comparer = EqualityComparer<int>.Default;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;
            // Use default comparer to avoid long running tests.
            var comparer = EqualityComparer<int?>.Default;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(value: null, comparer);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item (2).
            IReadOnlyList<int> collection = new[] { 1, 2, 2, 2 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = collection.Skip(1).First();
            const int expectedIndex = 1;
            var comparer = MockEqualityComparer.SetupDefaultFor(collection);

            // Act.
            int actualIndex = explosive.IndexOf(value, comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 2));
            Assert.Equal(expectedIndex, actualIndex);
            comparer.VerifyEqualsCalls(times: explosive.ExplosiveIndex);
            comparer.VerifyGetHashCodeNoCalls();
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;
            var comparer = MockEqualityComparer<int>.Default;

            // Act.
            int actualIndex = explosive.IndexOf(value: 0, comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualIndex);
            comparer.VerifyEqualsCallsOnceForEach(collection);
            comparer.VerifyGetHashCodeNoCalls();
        }

        #endregion
    }
}
