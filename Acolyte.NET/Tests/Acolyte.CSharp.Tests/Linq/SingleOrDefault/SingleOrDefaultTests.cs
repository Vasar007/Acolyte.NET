using System;
using System.Collections.Generic;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.SingleOrDefault
{
    public sealed partial class SingleOrDefaultTests
    {
        public SingleOrDefaultTests()
        {
        }

        #region Null Values

        [Fact]
        public void SingleOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.SingleOrDefault(defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void SingleOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.SingleOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Single Values

        [Fact]
        public void SingleOrDefault_ForCollectionWithSingleItem_ShouldReturnFirstItem()
        {
            // Arrange.
            var collectionWithSingleItem = TestDataCreator.CreateRandomInt32List(TestConstants._1);
            int expectedValue = collectionWithSingleItem[0];

            // Act.
            int actualValue = collectionWithSingleItem.SingleOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void SingleOrDefault_ForPredefinedCollection_ShouldFail()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => predefinedCollection.SingleOrDefault(defaultValue: default)
            );
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveWithoutOneTestCase))]
        public void SingleOrDefault_ForCollectionWithSomeItems_ShouldFail(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => collectionWithSomeItems.SingleOrDefault(defaultValue: default)
            );
        }

        #endregion

        #region Random Values

        [Fact]
        public void SingleOrDefault_ForCollectionWithRandomSize_ShouldReturnSingleOrDefaultItemOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act & Assert.
            if (collectionWithRandomSize.Count > 1)
            {
                Assert.Throws(
                     Error.MoreThanOneElement().GetType(),
                     () => collectionWithRandomSize.SingleOrDefault(defaultResult)
                 );
            }
            else
            {
                int actualValue = collectionWithRandomSize.SingleOrDefault(defaultResult);

                int expectedValue = collectionWithRandomSize.Count > 0
                     ? collectionWithRandomSize[0]
                     : defaultResult;

                Assert.Equal(expectedValue, actualValue);
            }
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void SingleOrDefault_ShouldLookOnlyAtFirstAndSecondItemsFromCollectionBeforeFail()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => explosive.SingleOrDefault(defaultValue: default)
            );

            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 2));
        }

        [Fact]
        public void SingleOrDefault_ShoulReturnNullValueIfItIsTheSingleFoundValue()
        {
            // Arrange.
            // Do not use random because we should find exactly first item.
            int expectedIndex = Constants.FirstIndex;
            IReadOnlyList<int?> collection = new int?[] { null };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int? expectedValue = collection[expectedIndex];

            // Act.
            int? actualValue = explosive.SingleOrDefault(defaultValue: 0);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
