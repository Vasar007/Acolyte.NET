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

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderKeySelectors_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = TestDataCreator.CreateRandomInt32List(count);
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
            IReadOnlyList<int> someSource = TestDataCreator.CreateRandomInt32List(count);
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

        #endregion

        #region Some Values

        #endregion

        #region Random Values

        #endregion

        #region Extended Logical Coverage

        #endregion
    }
}
