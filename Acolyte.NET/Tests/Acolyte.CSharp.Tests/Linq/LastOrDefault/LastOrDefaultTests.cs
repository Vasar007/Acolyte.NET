using System;
using System.Collections.Generic;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.LastOrDefault
{
    public sealed partial class LastOrDefaultTests
    {
        public LastOrDefaultTests()
        {
        }

        #region Null Values

        [Fact]
        public void LastOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.LastOrDefault(defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void LastOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.LastOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void LastOrDefault_ForPredefinedCollection_ShouldReturnLastItem()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[^1];

            // Act.
            int actualValue = predefinedCollection.LastOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void LastOrDefault_ForCollectionWithSomeItems_ShouldReturnLastItem(int count)
        {
            // Arrange.
            var collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems[collectionWithSomeItems.Count - 1];

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(defaultValue: default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void LastOrDefault_ForCollectionWithRandomSize_ShouldReturnLastOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Count > 0
                ? collectionWithRandomSize[collectionWithRandomSize.Count - 1]
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void LastOrDefault_ShouldLookWholeCollectionToFindLastItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.Create(
                collection, explosiveIndex: collection.Count
            );
            int expectedValue = collection[^1];

            // Act.
            int actualValue = explosive.LastOrDefault(defaultValue: default);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_ShoulReturnNullValueIfItIsTheLastFoundValue()
        {
            // Arrange.
            // Do not use random because we should find exactly last item.
            IReadOnlyList<int?> collection = new int?[] { 1, 2, 3, null };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int? expectedValue = collection[^1];

            // Act.
            int? actualValue = explosive.LastOrDefault(defaultValue: 0);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
