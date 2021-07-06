using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.OrderBySeruence
{
    public sealed partial class OrderBySequenceTests
    {
        #region Null Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    discardResultSelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForNullOrder_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "order",
                () => emptyCollection.OrderBySequence(
                    order: null!,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    discardResultSelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForNullSourceKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "sourceKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    sourceKeySelector: null!,
                    discardOrderKeySelector,
                    discardResultSelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForNullOrderKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "orderKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    orderKeySelector: null!,
                    discardResultSelector
                )
            );
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForNullResultKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            const Func<int, int, int>? nullResultSelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "resultSelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    nullResultSelector!
                )
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = TestDataCreator.CreateRandomInt32List(count);
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                someOrder, sourceKeySelector, orderKeySelector, sourceResultSelector
            );

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someSource = TestDataCreator.CreateRandomInt32List(count);
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();

            // Act & Assert.
            var actualResult = someSource.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector, sourceResultSelector
            );

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector, sourceResultSelector
            );

            Assert.Empty(actualResult);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectors_ForPredefinedCollection_ShouldOrderSource()
        {
            // Arrange.
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 }
                  .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> predefinedOrder = new[] { 2, 1, 3 }
                .Select(orderKeySelector)
                .ToReadOnlyList();
            Func<int, int, int> sourceResultSelector = GetRedoubledSourceResultSelector<int>();
            IReadOnlyList<int> expectedCollection = predefinedOrder
                .Select(MultiplyFunction.RedoubleInt32)
                .ToReadOnlyList();

            // Act.
            var actualCollection = predefinedCollection.OrderBySequence(
                predefinedOrder, sourceKeySelector, orderKeySelector, sourceResultSelector
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        #endregion

        #region Random Values

        #endregion

        #region Extended Logical Coverage

        #endregion
    }
}
