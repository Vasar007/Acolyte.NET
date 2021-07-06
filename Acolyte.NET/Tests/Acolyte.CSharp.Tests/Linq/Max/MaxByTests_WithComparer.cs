using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Null Values

        [Fact]
        public void MaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Func;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MaxBy(discardKeySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MaxBy(keySelector: null!, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Max();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(keySelector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MaxBy(keySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;
            var keyComparer = MockComparer<int?>.Default;

            // Act.
            int? actualValue = emptyCollection.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;
            Func<string, string> keySelector = IdentityFunction<string>.Instance;
            var keyComparer = MockComparer<string>.Default;

            // Act.
            string? actualValue = emptyCollection.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = predefinedCollection.Max(keySelector);
            int expectedValue = keySelector(maxValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = predefinedCollection.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMax(keyComparer, predefinedCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMax(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithSomeItems.Max(keySelector);
            int expectedValue = keySelector(maxValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithSomeItems.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMax(keyComparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            int expectedValue = count;
            Func<int, int> keySelector = InverseFunction.ForInt32;
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithTheSameItems.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMax(keyComparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collectionWithRandomSize.Max(keySelector);
            int expectedValue = keySelector(maxValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithRandomSize.MaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMax(keyComparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Max with selector returns transformed value. Need to transform it back.
            int maxValue = collection.Max(keySelector);
            int expectedValue = keySelector(maxValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = explosive.MaxBy(keySelector, keyComparer);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMax(keyComparer, collection);
        }

        #endregion
    }
}
