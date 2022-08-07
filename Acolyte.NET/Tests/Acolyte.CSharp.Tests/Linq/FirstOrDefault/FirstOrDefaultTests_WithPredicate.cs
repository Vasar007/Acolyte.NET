using System;
using System.Collections.Generic;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.FirstOrDefault
{
    public sealed partial class FirstOrDefaultTests
    {
        #region Null Values

        [Fact]
        public void FirstOrDefault_WithPredicate_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.FirstOrDefault(discard, defaultValue: default)
            );
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate",
                () => emptyCollection.FirstOrDefault(predicate: null!, defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void FirstOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void FirstOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnFirstItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnFirstItem(
            int count)
        {
            // Arrange.
            var collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems[0];

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnFirstOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);
            int expectedValue = collectionWithRandomSize.Count > 0
                ? System.Linq.Enumerable.First(collectionWithRandomSize, predicate)
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(predicate, defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            int expectedIndex = Constants.FirstIndex + 1;
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: expectedIndex + 1
            );
            int expectedValue = collection[expectedIndex];

            // Act.
            int actualValue = explosive.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 2));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = -1;

            // Act.
            int actualValue = explosive.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShoulReturnNullValueIfItIsTheFirstFoundValue()
        {
            // Arrange.
            // Do not use random because we should find exactly first item.
            int expectedIndex = Constants.FirstIndex;
            IReadOnlyList<int?> collection = new int?[] { null, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.Create(
               collection, explosiveIndex: expectedIndex + 1
           );
            int? expectedValue = collection[expectedIndex];

            // Act.
            int? actualValue = explosive.FirstOrDefault(
                i => i.Equals(expectedValue), defaultValue: 0
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 1));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
