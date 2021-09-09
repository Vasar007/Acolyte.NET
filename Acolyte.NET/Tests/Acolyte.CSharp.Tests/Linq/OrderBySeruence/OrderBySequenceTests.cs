using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using MoreLinq;
using Xunit;

namespace Acolyte.Tests.Linq.OrderBySeruence
{
    public sealed partial class OrderBySequenceTests
    {
        public OrderBySequenceTests()
        {
        }

        #region Null Values

        [Fact]
        public void OrderBySequence_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.OrderBySequence(emptyOrder)
            );
        }

        [Fact]
        public void OrderBySequence_ForNullValueOrder_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "order", () => emptyCollection.OrderBySequence(order: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = PrepareCollectionToUseInTests(count);

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(someOrder);

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someSource = PrepareCollectionToUseInTests(count);
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();

            // Act & Assert.
            var actualResult = someSource.OrderBySequence(emptyOrder);

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(emptyOrder);

            Assert.Empty(actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void OrderBySequence_ForPredefinedCollection_ShouldOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            IReadOnlyList<int> predefinedOrder = new[] { 2, 1, 3 };
            IReadOnlyList<int> expectedCollection = predefinedOrder;

            // Act.
            var actualCollection = predefinedCollection.OrderBySequence(predefinedOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_ForCollectionWithSomeItems_ShouldOrderSource(int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = randomOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(randomOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_ForCollectionWithSomeItems_ShouldOrderSourceAndRemainItemsWhichIncludeInBothCollections(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count);
            int orderCount = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Take(orderCount)
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = randomOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(randomOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void OrderBySequence_ForCollectionWithRandomSize_ShouldOrderSource()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = randomOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(randomOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void OrderBySequence_ShouldLookWholeCollectionsToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> source = new[] { 1, 2, 3, 4 };
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> expectedCollection = order;
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(explosiveOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        [Fact]
        public void OrderBySequence_ShouldPreserveDuplicatesInSource()
        {
            // Arrange.
            const int countToRepeat = 2;
            IReadOnlyList<int> initialSource = new[] { 1, 2, 3, 4 };
            IReadOnlyList<int> source = initialSource
                .SelectMany(item => Enumerable.Repeat(item, countToRepeat))
                .ToReadOnlyList();
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> expectedCollection = order
                .SelectMany(item => Enumerable.Repeat(item, countToRepeat))
                .ToReadOnlyList();
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(explosiveOrder);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        #endregion

        #region Private Methods

        private static Func<T1, T2, T1> GetSourceResultSelector<T1, T2>()
        {
            return (source, order) => source;
        }

        private static IReadOnlyList<int> PrepareCollectionToUseInTests(int count)
        {
            // Do not use duplicated values for "Enumerable.Join" method (called in
            // "OrderBySequence").
            // See https://github.com/dotnet/runtime/issues/55219
            return Enumerable.Range(0, count)
                .ToReadOnlyList();
        }

        #endregion
    }
}
