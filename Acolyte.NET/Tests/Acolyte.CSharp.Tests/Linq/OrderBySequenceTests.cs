using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class OrderBySequenceTests
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

        [Fact]
        public void OrderBySequence_WithSourceKeySelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;

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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;

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

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;

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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;

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
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;

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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "orderKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder, discardSourceKeySelector, orderKeySelector: null!
                )
            );
        }

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

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    discardResultSelector,
                    comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullOrder_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "order",
                () => emptyCollection.OrderBySequence(
                    order: null!,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    discardResultSelector,
                    comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullSourceKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "sourceKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    sourceKeySelector: null!,
                    discardOrderKeySelector,
                    discardResultSelector,
                    comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullOrderKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Func;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "orderKeySelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    orderKeySelector: null!,
                    discardResultSelector,
                    comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullResultKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IEnumerable<int> emptyOrder = Enumerable.Empty<int>();
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Func;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Func;
            const Func<int, int, int>? nullResultSelector = null;
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "resultSelector",
                () => emptyCollection.OrderBySequence(
                    emptyOrder,
                    discardSourceKeySelector,
                    discardOrderKeySelector,
                    nullResultSelector!,
                    comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem, comparer: null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> reverseOrder = collectionWithRandomSize.Reverse().ToReadOnlyList();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> firstResultSelector = (source, order) => source;
            IReadOnlyList<int> expectedResult = reverseOrder;

            // Act & Assert.
            var actualResult = collectionWithRandomSize.OrderBySequence(
                reverseOrder,
                sourceKeySelector,
                orderKeySelector,
                firstResultSelector,
                comparer: null
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Empty Values

        #endregion

        #region Predefined Values

        #endregion

        #region Some Values

        #endregion

        #region Random Values

        #endregion

        #region Extended Logical Coverage

        #endregion
    }
}
