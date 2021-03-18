using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class IsNullOrEmptyTests
    {
        public IsNullOrEmptyTests()
        {
        }

        [Fact]
        public void IsNullOrEmpty_ForNullValue_ShouldReturnTrue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.True(nullValue.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ForEmptyCollection_ShouldReturnTrue()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.True(emptyCollection.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ForPredefinedCollection_ShouldReturnFalse()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            // Act & Assert.
            Assert.False(predefinedCollection.IsNullOrEmpty());
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void IsNullOrEmpty_ForCollectionWithSomeItems_ShouldReturnFalse(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.False(collectionWithSomeItems.IsNullOrEmpty());
        }

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

        [Fact]
        public void IsNullOrEmpty_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            const int count = 2;
            var explosiveCollection = ExplosiveCollection.Create(
                TestDataCreator.CreateRandomInt32List(count),
                explosiveIndex: Constants.FirstIndex + 1
            );
            bool expectedResult = !explosiveCollection.Any();

            // Act.
            bool actualResult = explosiveCollection.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expected: 1, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
