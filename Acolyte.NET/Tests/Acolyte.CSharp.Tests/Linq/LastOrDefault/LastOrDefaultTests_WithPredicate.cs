using System;
using System.Collections.Generic;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.LastOrDefault
{
    public sealed partial class LastOrDefaultTests
    {
        #region Null Values

        [Fact]
        public void LastOrDefault_WithPredicate_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.LastOrDefault(discard, defaultValue: default)
            );
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate",
                () => emptyCollection.LastOrDefault(predicate: null!, defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void LastOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;

            // Act.
            int actualValue = emptyCollection.LastOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void LastOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnLastItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.LastOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnLastItem(
            int count)
        {
            // Arrange.
            var collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems[collectionWithSomeItems.Count - 1];

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            var collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnLastOrDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(predicate, defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Count > 0
                ? System.Linq.Enumerable.Last(collectionWithRandomSize, predicate)
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            var collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItemAfterItFoundSomething()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;
            int expectedValue = collection[1];

            // Act.
            int actualValue = explosive.LastOrDefault(discard, expectedValue);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = collection[1];

            // Act.
            int actualValue = explosive.LastOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShoulReturnNullValueIfItIsTheFLastFoundValue()
        {
            // Arrange.
            // Do not use random because we should find exactly last item.
            IReadOnlyList<int?> collection = new int?[] { 1, 2, 3, null };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int? expectedValue = collection[^1];

            // Act.
            int? actualValue = explosive.LastOrDefault(
                i => i.Equals(expectedValue), defaultValue: 0
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
