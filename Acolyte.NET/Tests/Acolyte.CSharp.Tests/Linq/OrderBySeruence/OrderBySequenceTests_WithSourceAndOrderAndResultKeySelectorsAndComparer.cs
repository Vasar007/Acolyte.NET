using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Internal;
using Acolyte.Tests.Mocked;
using MoreLinq;
using Xunit;

namespace Acolyte.Tests.Linq.OrderBySeruence
{
    public sealed partial class OrderBySequenceTests
    {
        #region Null Values

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
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> reverseOrder = collectionWithRandomSize.Reverse().ToReadOnlyList();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            IReadOnlyList<int> expectedResult = reverseOrder;

            // Act & Assert.
            var actualResult = collectionWithRandomSize.OrderBySequence(
                reverseOrder,
                sourceKeySelector,
                orderKeySelector,
                sourceResultSelector,
                comparer: null
            );

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForEmptySource_ShouldDoNothing()
        {
            // Arrange.
            IReadOnlyList<int> emptyCollection = Array.Empty<int>();
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someOrder = PrepareCollectionToUseInTests(count);
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                someOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            Assert.Empty(actualResult);
            VerifyCallsForOrderBySequence(comparer, emptyCollection, someOrder);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> emptyOrder = Array.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            var actualResult = collectionWithRandomSize.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            Assert.Empty(actualResult);
            VerifyCallsForOrderBySequence(comparer, collectionWithRandomSize, emptyOrder);
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IReadOnlyList<int> emptyCollection = Array.Empty<int>();
            IReadOnlyList<int> emptyOrder = Array.Empty<int>();
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            var comparer = MockEqualityComparer<int>.Default;

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(
                emptyOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            Assert.Empty(actualResult);
            VerifyCallsForOrderBySequence(comparer, emptyCollection, emptyOrder);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForPredefinedCollection_ShouldOrderSource()
        {
            // Arrange.
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> resultSelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            IReadOnlyList<int> predefinedOrder = new[] { 2, 1, 3 };
            Func<int, int, int> sourceResultSelector = (source, order) => resultSelector(source);
            IReadOnlyList<int> expectedCollection = predefinedOrder
                .Select(resultSelector)
                .ToReadOnlyList();
            var comparer = MockEqualityComparer<int>.Default;

            // Act.
            var actualCollection = predefinedCollection.OrderBySequence(
                predefinedOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, expectedCollection, predefinedOrder);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForCollectionWithSomeItems_ShouldOrderSource(
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
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            IReadOnlyList<int> expectedCollection = randomOrder;
            var comparer = MockEqualityComparer<int>.Default;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, expectedCollection, randomOrder);
        }

        #endregion

        #region Random Values

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForCollectionWithRandomSize_ShouldOrderSource()
        {
            // Arrange
            int count = TestDataCreator.GetRandomSmallCountNumber();
            // Using identity function to avoid int overflowing.
            Func<int, int> sourceKeySelector = IdentityFunction<int>.Instance;
            Func<int, int> orderKeySelector = IdentityFunction<int>.Instance;
            IReadOnlyList<int> collectionWithSomeItems =
                PrepareCollectionToUseInTests(count)
                .Select(sourceKeySelector)
                .ToReadOnlyList();
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Shuffle()
                .ToReadOnlyList();
            Func<int, int, int> sourceResultSelector = GetSourceResultSelector<int, int>();
            IReadOnlyList<int> expectedCollection = randomOrder;
            var comparer = MockEqualityComparer<int>.Default;

            // Act.
            var actualCollection = collectionWithSomeItems.OrderBySequence(
               randomOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
           );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, expectedCollection, randomOrder);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ShouldLookWholeCollectionToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> resultSelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            Func<int, int, int> sourceResultSelector = (source, order) => resultSelector(source);
            IReadOnlyList<int> expectedCollection = order
                .Select(resultSelector)
                .ToReadOnlyList();
            var comparer = MockEqualityComparer<int>.Default;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);

            // Act.
            var actualCollection = explosive.OrderBySequence(
                 order, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
             );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, expectedCollection, order);
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
        }

        #endregion

        #region Private Methods

        private static void VerifyCallsForOrderBySequence<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order)
        {
            if (TestDotNetPlatformHelper.IsNetFramework())
            {
                VerifyCallsForOrderBySequenceForNetFramework(comparer, source, order);
            }
            else
            {
                VerifyCallsForOrderBySequenceForNetCore(comparer, source, order);
            }
        }

        private static void VerifyCallsForOrderBySequenceForNetFramework<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order)
        {
            if (source.Count > 0 && order.Count > 0)
            {
                int expectedNumberOfCalls = Math.Max(source.Count, order.Count);
                comparer.VerifyEqualsCalls(expectedNumberOfCalls);
            }
            else
            {
                comparer.VerifyEqualsNoCalls();
            }

            if (source.Count > 0 || order.Count > 0)
            {
                int expectedNumberOfCalls = source.Count + order.Count;
                comparer.VerifyGetHashCodeCalls(expectedNumberOfCalls);
            }
            else
            {
                comparer.VerifyGetHashCodeNoCalls();
            }
        }

        private static void VerifyCallsForOrderBySequenceForNetCore<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order)
        {
            if (source.Count > 0 && order.Count > 0)
            {
                int expectedNumberOfCalls = Math.Max(source.Count, order.Count);
                comparer.VerifyEqualsCalls(expectedNumberOfCalls);
            }
            else
            {
                comparer.VerifyEqualsNoCalls();
            }

            // .NET Core optimize cases for empty collections.
            if (source.Count > 0 && order.Count > 0)
            {
                int expectedNumberOfCalls = source.Count + order.Count;
                comparer.VerifyGetHashCodeCalls(expectedNumberOfCalls);
            }
            else
            {
                comparer.VerifyGetHashCodeNoCalls();
            }
        }

        #endregion
    }
}
