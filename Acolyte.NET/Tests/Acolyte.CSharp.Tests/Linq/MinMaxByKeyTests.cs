using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class MinMaxByKeyTests
    {
        public MinMaxByKeyTests()
        {
        }

        #region Null Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector)
            );
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, int>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinMaxBy(keySelector!)
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int> discardKeySelector = DiscardFunction<int>.Func;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinMaxBy(keySelector: null!, Comparer<int>.Default)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int minValue, int maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMaxBy(keySelector)
            );
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithoutComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);
            Func<string, string> keySelector = IdentityFunction<string>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> keySelector = IdentityFunction<int>.Instance;
            var keyComparer = MockComparer<int>.Default;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMaxBy(keySelector, keyComparer)
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);
            Func<int?, int?> keySelector = IdentityFunction<int?>.Instance;
            var keyComparer = MockComparer<int?>.Default;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);
            Func<string, string> keySelector = IdentityFunction<string>.Instance;
            var keyComparer = MockComparer<string>.Default;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 2, 2, 3, 1 };
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, predefinedCollection);
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
        public void MinMaxBy_WithoutComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int maxValue = collectionWithSomeItems.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMax(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int maxValue = collectionWithSomeItems.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithSomeItems);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMaxBy_WithoutComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            (int minValue, int maxValue) expectedValue = (count, count);
            Func<int, int> keySelector = InverseFunction.ForInt32;

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            (int minValue, int maxValue) expectedValue = (count, count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMaxBy_WithoutComparer_ForCollectionWithRandomSize_ShouldReturnMinMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int maxValue = collectionWithRandomSize.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMax()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int maxValue = collectionWithRandomSize.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMaxBy_WithoutComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosive.Min(keySelector);
            int maxValue = explosive.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = explosive.Min(keySelector);
            int maxValue = explosive.Max(keySelector);
            (int minValue, int maxValue) expectedValue =
                (keySelector(minValue), keySelector(maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector, keyComparer);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collection);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMinMax<T>(MockComparer<T> comparer,
            IReadOnlyList<T> collection)
        {
            comparer.VerifyCompareCalls(times: (collection.Count - 1) * 2);
        }

        #endregion
    }
}
