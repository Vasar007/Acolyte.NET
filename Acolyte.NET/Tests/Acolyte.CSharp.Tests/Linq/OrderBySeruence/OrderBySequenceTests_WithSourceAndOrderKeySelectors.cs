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
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.OrderBySequence(
                    emptyOrder, discardSourceKeySelector, discardOrderKeySelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForNullOrder_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "order",
                () => emptyCollection.OrderBySequence(
                    order: null!, discardSourceKeySelector, discardOrderKeySelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForNullSourceKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "sourceKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder, sourceKeySelector: null!, discardOrderKeySelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForNullOrderKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "orderKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder, discardSourceKeySelector, orderKeySelector: null!
                )
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = PrepareCollectionToUseInTests(count);
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                someOrder, sourceKeySelector, orderKeySelector
            );

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someSource = PrepareCollectionToUseInTests(count);
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = someSource.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector
            );

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector
            );

            Assert.Empty(actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForPredefinedCollection_ShouldOrderSource()
        {
            // Arrange.
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            IReadOnlyList<int> predefinedOrder = new[] { 2, 1, 3 };
            IReadOnlyList<int> expectedCollection = predefinedOrder;

            // Act.
            var actualCollection = predefinedCollection.OrderBySequence(
                predefinedOrder, sourceKeySelector, orderKeySelector
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForCollectionWithSomeItems_ShouldOrderSource(
            int count)
        {
            // Arrange.
            // Using identity function to avoid int overflowing.
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count)
                .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> sourceOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> randomOrder = sourceOrder
                .Select(orderKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = sourceOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector, orderKeySelector
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForCollectionWithRandomSize_ShouldOrderSource()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            // Using identity function to avoid int overflowing.
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count)
                .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> sourceOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            IReadOnlyList<int> randomOrder = sourceOrder
                .Select(orderKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> expectedCollection = sourceOrder;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector, orderKeySelector
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ShouldLookWholeCollectionsToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> source = new[] { 1, 2, 3, 4 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> expectedCollection = order;
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(
                explosiveOrder, sourceKeySelector, orderKeySelector
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ShouldPreserveDuplicatesInSource()
        {
            // Arrange.
            IReadOnlyList<int> source = new[] { 1, 1, 2, 2, 3, 3, 4, 4 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> expectedCollection = order
                .SelectMany(item => Enumerable.Repeat(item, 2))
                .ToReadOnlyList();
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(
                explosiveOrder, sourceKeySelector, orderKeySelector
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        #endregion
    }
}
