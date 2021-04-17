#pragma warning disable CS0618 // Type or member is obsolete

using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
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
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
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
            Assert.Equal(expected: 1, explosive.VisitedItemsNumber);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion
    }
}
