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

namespace Acolyte.Tests.Linq
{
    public sealed class MinByKeyTests
    {
        public MinByKeyTests()
        {
        }

        #region Null Values

        [Fact]
        public void MinBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinBy(discardKeySelector)
            );
        }

        [Fact]
        public void MinBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, int>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinBy(keySelector!)
            );
        }

        [Fact]
        public void MinBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Func;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinBy(discardKeySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinBy(keySelector: null!, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(keySelector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinBy(keySelector));
        }

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;

            // Act.
            int? actualValue = emptyCollection.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;
            Func<string, string> keySelector = IdentityFunction<string>.Instance;

            // Act.
            string? actualValue = emptyCollection.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinBy(keySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;
            var keyComparer = MockComparer<int?>.Default;

            // Act.
            int? actualValue = emptyCollection.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;
            Func<string, string> keySelector = IdentityFunction<string>.Instance;
            var keyComparer = MockComparer<string>.Default;

            // Act.
            string? actualValue = emptyCollection.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]

        public void MinBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int expectedValue = keySelector(minValue);

            // Act.
            int actualValue = predefinedCollection.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMin()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int expectedValue = keySelector(minValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = predefinedCollection.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(keyComparer, predefinedCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int expectedValue = keySelector(minValue);

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMin(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int expectedValue = keySelector(minValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithSomeItems.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(keyComparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            int expectedValue = count;
            Func<int, int> keySelector = InverseFunction.ForInt32;

            // Act.
            int actualValue = collectionWithTheSameItems.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
               .Repeat(count, count)
                .ToList();
            int expectedValue = count;
            Func<int, int> keySelector = InverseFunction.ForInt32;
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithTheSameItems.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(keyComparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int expectedValue = keySelector(minValue);

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMin()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int expectedValue = keySelector(minValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = collectionWithRandomSize.MinBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(keyComparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosive.Min(keySelector);
            int expectedValue = keySelector(minValue);

            // Act.
            int actualValue = explosive.MinBy(keySelector);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min with selector returns transformed value. Need to transform it back.
            int minValue = explosive.Min(keySelector);
            int expectedValue = keySelector(minValue);
            var keyComparer = MockComparer<int>.Default;

            // Act.
            int actualValue = explosive.MinBy(keySelector, keyComparer);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(keyComparer, collection);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMin<T>(MockComparer<T> comparer,
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
