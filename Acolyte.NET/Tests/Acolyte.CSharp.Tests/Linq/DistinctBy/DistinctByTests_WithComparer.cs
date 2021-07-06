using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq.DistinctBy
{
    public sealed partial class DistinctByTests
    {
        #region Null Values

        [Fact]
        public void DistinctBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discardKeySelector = DiscardFunction<int, bool>.Func;
            var keyComparer = MockEqualityComparer<bool>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.DistinctBy(discardKeySelector, keyComparer).ToList()
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void DistinctBy_WithComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, bool>? keySelector = null;
            var keyComparer = MockEqualityComparer<bool>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.DistinctBy(keySelector!, keyComparer).ToList()
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void DistinctBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var expectedResult = GetExpectedResult(collectionWithRandomSize, keySelector);

            // Act.
            var actualResult = collectionWithRandomSize.DistinctBy(keySelector, keyComparer: null);

            // Assert.
            Assert.NotNull(actualResult);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void DistinctBy_WithComparer_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = MockEqualityComparer<bool>.Default;

            // Act.
            var actualResult = emptyCollection.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(emptyCollection, actualResult);
            keyComparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void DistinctBy_WithComparer_ForPredefinedCollection_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            var keyComparer = MockEqualityComparer.SetupDefaultFor(range);
            var expectedResult = GetExpectedResult(predefinedCollection, keySelector);

            // Act.
            var actualResult = predefinedCollection.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
            keyComparer.VerifyGetHashCodeCallsOnceForEach(predefinedCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void DistinctBy_WithComparer_ForCollectionWithSomeItems_ShouldSelectUniqueItemsByKeySelector(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
               TestDataCreator.CreateRandomInt32List(count);

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            var keyComparer = MockEqualityComparer.SetupDefaultFor(range);
            var expectedResult = GetExpectedResult(collectionWithSomeItems, keySelector);

            // Act.
            var actualResult = collectionWithSomeItems.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
            keyComparer.VerifyGetHashCodeCallsOnceForEach(collectionWithSomeItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void DistinctBy_WithComparer_ForCollectionWithRandomSize_ShouldSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);

            var range = CreateRange();
            Func<int, long> keySelector = item => MapToValueInRange(item, range);
            // Use default comparer to avoid long running tests.
            var keyComparer = EqualityComparer<long>.Default;
            var expectedResult = GetExpectedResult(collectionWithRandomSize, keySelector);

            // Act.
            var actualResult = collectionWithRandomSize.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void DistinctBy_WithComparer_ShouldLookWholeCollectionToSelectUniqueItemsByKeySelector()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, bool> keySelector = item => NumberParityFunction.IsEven(item);
            var keyComparer = MockEqualityComparer<bool>.Default;
            var expectedResult = GetExpectedResult(collection, keySelector);

            // Act.
            var actualResult = explosive.DistinctBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            keyComparer.VerifyGetHashCodeCallsOnceForEach(collection);
        }

        #endregion
    }
}
