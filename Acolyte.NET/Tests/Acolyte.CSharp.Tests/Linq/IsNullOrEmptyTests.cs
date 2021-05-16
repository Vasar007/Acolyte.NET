using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class IsNullOrEmptyTests
    {
        public IsNullOrEmptyTests()
        {
        }

        #region Null Values

        [Fact]
        public void IsNullOrEmpty_ForNullValue_ShouldReturnTrue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.True(nullValue.IsNullOrEmpty());
        }

        #endregion

        #region Empty Values

        [Fact]
        public void IsNullOrEmpty_ForEmptyCollection_ShouldReturnTrue()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.True(emptyCollection.IsNullOrEmpty());
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void IsNullOrEmpty_ForPredefinedCollection_ShouldReturnFalse()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            // Act & Assert.
            Assert.False(predefinedCollection.IsNullOrEmpty());
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void IsNullOrEmpty_ForCollectionWithSomeItems_ShouldReturnFalse(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.False(collectionWithSomeItems.IsNullOrEmpty());
        }

        #endregion

        #region Random Values

        [Fact]
        public void IsNullOrEmpty_ForCollectionWithRandomSize_ShouldReturnProperResult()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            bool expectedResult = !collectionWithRandomSize.Any();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsNullOrEmpty_ForCollectionWithRandomNumberAndNullValues_ShouldReturnProperResult()
        {
            // Arrange.
            IEnumerable<int?> collectionWithRandomSize =
                TestDataCreator.CreateRandomNullableInt32List();
            bool expectedResult = !collectionWithRandomSize.Any();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void IsNullOrEmpty_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            const int count = 2;
            var explosive = ExplosiveEnumerable.Create(
                TestDataCreator.CreateRandomInt32List(count),
                explosiveIndex: Constants.FirstIndex + 1
            );
            bool expectedResult = !explosive.Any();

            // Act.
            bool actualResult = explosive.IsNullOrEmpty();

            // Assert.
            CustomAssert.True(explosive.VerifyTwice(expectedVisitedItemsNumber: 1));
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion
    }
}
