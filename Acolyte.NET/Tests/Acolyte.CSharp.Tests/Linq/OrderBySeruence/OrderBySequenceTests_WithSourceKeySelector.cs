using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
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
        #region Null Values

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.OrderBySequence(emptyOrder, discardSourceKeySelector)
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForNullOrder_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "order",
                () => emptyCollection.OrderBySequence(order: null!, discardSourceKeySelector)
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForNullSourceKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "sourceKeySelector",
                () => emptyCollection.OrderBySequence(emptyOrder, sourceKeySelector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = PrepareCollectionToUseInTests(count);
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(someOrder, sourceKeySelector);

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someSource = PrepareCollectionToUseInTests(count);
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = someSource.OrderBySequence(emptyOrder, sourceKeySelector);

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(emptyOrder, sourceKeySelector);

            Assert.Empty(actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForPredefinedCollection_ShouldOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> sourceOrder = new[] { 2, 1, 3 };
            IReadOnlyList<int> predefinedOrder = sourceOrder
                .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = sourceOrder;

            // Act.
            var actualCollection = predefinedCollection.OrderBySequence(
                predefinedOrder, sourceKeySelector
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_WithSourceKeySelector_ForCollectionWithSomeItems_ShouldOrderSource(
            int count)
        {
            // Arrange.
            // Using identity function to avoid int overflowing.
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = randomOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForCollectionWithRandomSize_ShouldOrderSource()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            // Using identity function to avoid int overflowing.
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = randomOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ShouldLookWholeCollectionToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> sourceOrder = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> order = sourceOrder
                .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = sourceOrder;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);

            // Act.
            var actualCollection = explosive.OrderBySequence(order, sourceKeySelector);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
        }

        #endregion
    }
}
