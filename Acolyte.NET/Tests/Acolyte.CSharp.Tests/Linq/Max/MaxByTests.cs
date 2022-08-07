using System;
using System.Collections.Generic;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq.Max
{
    public sealed partial class MaxByTests
    {
        public MaxByTests()
        {
        }

        #region Null Values

        [Fact]
        public void MaxBy_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MaxBy(discardKeySelector)
            );
        }

        [Fact]
        public void MaxBy_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            const Func<int, int>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MaxBy(keySelector!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MaxBy_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MaxBy(keySelector));
        }

        [Fact]
        public void MaxBy_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = EnumerableHelper.Empty<int?>();
            int? expectedValue = null;
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;

            // Act.
            int? actualValue = emptyCollection.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MaxBy_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = EnumerableHelper.Empty<string>();
            const string? expectedValue = null;
            Func<string, string> keySelector = IdentityFunction<string>.Instance;

            // Act.
            string? actualValue = emptyCollection.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MaxBy_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(keySelector);
            int expectedValue = keySelector(maxValue);

            // Act.
            int actualValue = predefinedCollection.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MaxBy_ForCollectionWithSomeItems_ShouldReturnProperMax(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(keySelector);
            int expectedValue = keySelector(maxValue);

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MaxBy_ForCollectionWithTheSameItems_ShouldReturnThatItem(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = EnumerableHelper
                .Repeat(count, count)
                .ToReadOnlyList();
            int expectedValue = count;
            Func<int, int> keySelector = InverseFunction.ForInt32;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MaxBy_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(keySelector);
            int expectedValue = keySelector(maxValue);

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MaxBy_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collection.Max(keySelector);
            int expectedValue = keySelector(maxValue);

            // Act.
            int actualValue = explosive.MaxBy(keySelector);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMax<T>(MockComparer<T> comparer,
            IReadOnlyList<T> collection)
        {
            if (collection.Count > 0)
            {
                comparer.VerifyCompareCalls(times: collection.Count - 1);
            }
            else
            {
                comparer.VerifyNoCalls();
            }
        }

        #endregion
    }
}
