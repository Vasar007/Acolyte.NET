using System;
using System.Collections.Generic;
using Acolyte.Common;
using Acolyte.Functions;
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
        #region Null Values

        [Fact]
        public void SingleOrDefault_WithPredicate_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.SingleOrDefault(predicate: null!, defaultValue: default)
            );
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "predicate",
                () => emptyCollection.SingleOrDefault(predicate: null!, defaultValue: default)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void SingleOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = EnumerableHelper.Empty<int>();
            Func<int, bool> discard = DiscardFunction<int, bool>.Instance;
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.SingleOrDefault(discard, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Single Values

        [Fact]
        public void SingleOrDefault_WithPredicate_ForCollectionWithSingleItem_ShouldReturnFirstItem()
        {
            // Arrange.
            var collectionWithSingleItem = TestDataCreator.CreateRandomInt32List(TestConstants._1);
            int expectedValue = collectionWithSingleItem[0];

            // Act.
            int actualValue = collectionWithSingleItem.SingleOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void SingleOrDefault_WithPredicate_ForPredefinedCollection_ShouldFailIfFoundTwoAndMoreItmsAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 1 };
            int expectedValue = predefinedCollection[0];

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => predefinedCollection.SingleOrDefault(
                    i => i.Equals(expectedValue), defaultValue: default
                )
            );
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnSingleItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.SingleOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveWithoutOneTestCase))]
        public void SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldFailIfFoundTwoAndMoreItmsAccordingToPredicate(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => collectionWithSomeItems.SingleOrDefault(_ => true, defaultValue: default)
            );
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItemIfFoundNoItemsAccordingToPredicate(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.SingleOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void SingleOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnSingleOrDefaultItemOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);

            // Act & Assert.
            int foundValuesCount = System.Linq.Enumerable.Count(collectionWithRandomSize, predicate);
            if (foundValuesCount > 1)
            {
                Assert.Throws(
                     Error.MoreThanOneElement().GetType(),
                     () => collectionWithRandomSize.SingleOrDefault(predicate, defaultResult)
                 );
            }
            else
            {
                int actualValue = collectionWithRandomSize.SingleOrDefault(predicate, defaultResult);

                // Collection cannot be empty if we found one value.
                int expectedValue = foundValuesCount == 1
                    ? System.Linq.Enumerable.Single(collectionWithRandomSize, predicate)
                    : defaultResult;

                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.SingleOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void SingleOrDefault_WithPredicate_ShouldLookWholeCollectionToEnsureAppropriateItemIsSingle()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = collection[1];

            // Act.
            int actualValue = explosive.SingleOrDefault(
                i => i.Equals(expectedValue), defaultValue: default
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ShouldFailAsSoonAsSecondAppropriateItemWasFound()
        {
            // Arrange.
            // Do not use random because we should find exactly two equal items.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 2, 5 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int expectedValue = collection[1];

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => explosive.SingleOrDefault(
                    i => i.Equals(expectedValue), defaultValue: default
                )
            );

            CustomAssert.True(explosive.VerifyOnce(expectedVisitedItemsNumber: 4));
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ShoulReturnNullValueIfItIsTheSingleFoundValue()
        {
            // Arrange.
            // Do not use random because we should find exactly first item.
            int expectedIndex = Constants.FirstIndex;
            IReadOnlyList<int?> collection = new int?[] { null, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            int? expectedValue = collection[expectedIndex];

            // Act.
            int? actualValue = explosive.SingleOrDefault(
                i => i.Equals(expectedValue), defaultValue: 0
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
