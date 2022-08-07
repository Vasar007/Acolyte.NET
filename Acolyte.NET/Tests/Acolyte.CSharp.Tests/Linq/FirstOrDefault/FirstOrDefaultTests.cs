using System;
using System.Collections.Generic;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.FirstOrDefault
{
    public sealed partial class FirstOrDefaultTests
    {
        public FirstOrDefaultTests()
        {
        }

        #region Null Values

        [Fact]
        public void FirstOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.FirstOrDefault(defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void FirstOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void FirstOrDefault_ForPredefinedCollection_ShouldReturnFirstItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[0];

            // Act.
            int actualValue = predefinedCollection.FirstOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void FirstOrDefault_ForCollectionWithSomeItems_ShouldReturnFirstItem(int count)
        {
            // Arrange.
            var collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems[0];

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void FirstOrDefault_ForCollectionWithRandomSize_ShouldReturnFirstOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            int expectedValue = collectionWithRandomSize.Count > 0
                ? collectionWithRandomSize[0]
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void FirstOrDefault_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 1
            );
            int expectedValue = collection[0];

            // Act.
            int actualValue = explosive.FirstOrDefault(defaultValue: default);

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 1));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_ShoulReturnNullValueIfItIsTheFirstFoundValue()
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
            int? actualValue = explosive.FirstOrDefault(defaultValue: 0);

            // Assert.
            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 1));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
