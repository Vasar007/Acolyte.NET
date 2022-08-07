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
using Acolyte.Tests.Objects;
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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Instance;
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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Instance;
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
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Instance;
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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int, int> discardResultSelector = DiscardFunction<int, int, int>.Instance;
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
            Func<int, int> discardSourceKeySelector = DiscardFunction<int>.Instance;
            Func<int, int> discardOrderKeySelector = DiscardFunction<int>.Instance;
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
            IReadOnlyList<int> collectionWithRandomSize = PrepareCollectionToUseInTests(count);
            IReadOnlyList<int> reverseOrder = Enumerable.Reverse(collectionWithRandomSize).ToReadOnlyList();
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
            VerifyCallsForOrderBySequence(
                comparer, emptyCollection, someOrder, emptyCollection
           );
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
            VerifyCallsForOrderBySequence(
                comparer, collectionWithRandomSize, emptyOrder, collectionWithRandomSize
           );
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
            VerifyCallsForOrderBySequence(
                comparer, emptyCollection, emptyOrder, emptyCollection
           );
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
            VerifyCallsForOrderBySequence(
                 comparer, predefinedCollection, predefinedOrder, expectedCollection
            );
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
            VerifyCallsForOrderBySequence(
                 comparer, collectionWithSomeItems, randomOrder, expectedCollection
             );
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ForCollectionWithSomeItems_ShouldOrderSourceAndRemainItemsWhichIncludeInBothCollections(
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
            int orderCount = TestDataCreator.CreateRandomNonNegativeInt32(count);
            IReadOnlyList<int> randomOrder = collectionWithSomeItems
                .Take(orderCount)
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
            VerifyCallsForOrderBySequence(
                comparer, collectionWithSomeItems, randomOrder, expectedCollection
            );
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
            VerifyCallsForOrderBySequence(
                comparer, collectionWithSomeItems, randomOrder, expectedCollection
            );
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ShouldLookWholeCollectionsToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> source = new[] { 1, 2, 3, 4 };
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> resultSelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            Func<int, int, int> sourceResultSelector = (source, order) => resultSelector(source);
            IReadOnlyList<int> expectedCollection = order
                .Select(resultSelector)
                .ToReadOnlyList();
            var comparer = MockEqualityComparer.SetupDefaultFor(expectedCollection);
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(
                 explosiveOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
             );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, source, order, expectedCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ShouldPreserveDuplicatesInSource()
        {
            // Arrange.
            const int countToRepeat = 2;
            IReadOnlyList<int> initialSource = new[] { 1, 2, 3, 4 };
            IReadOnlyList<int> source = initialSource
                .SelectMany(item => Enumerable.Repeat(item, countToRepeat))
                .ToReadOnlyList();
            Func<int, int> sourceKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> orderKeySelector = MultiplyFunction.RedoubleInt32;
            Func<int, int> resultSelector = MultiplyFunction.RedoubleInt32;
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            Func<int, int, int> sourceResultSelector = (source, order) => resultSelector(source);
            IReadOnlyList<int> expectedCollection = order
                .Select(resultSelector)
                .SelectMany(item => Enumerable.Repeat(item, countToRepeat))
                .ToReadOnlyList();
            var comparer = MockEqualityComparer.SetupDefaultFor(expectedCollection);
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(
                explosiveOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, source, order, expectedCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        [Fact]
        public void OrderBySequence_WithSourceAndOrderAndResultKeySelectorsAndComparer_ShouldPreserveDuplicatesInSourceAsStableSort()
        {
            // Arrange.
            IReadOnlyList<DummyClassId> source = new[]
            {
                new DummyClassId(Guid.NewGuid(), 1),
                new DummyClassId(Guid.NewGuid(), 1),
                new DummyClassId(Guid.NewGuid(), 2),
                new DummyClassId(Guid.NewGuid(), 2),
                new DummyClassId(Guid.NewGuid(), 3),
                new DummyClassId(Guid.NewGuid(), 3),
            };
            Func<int, int> keySelector = MultiplyFunction.RedoubleInt32;
            Func<DummyClassId?, DummyClassId> sourceKeySelector =
                item => new DummyClassId(item!.Id, keySelector(item.Value));
            Func<DummyClassId?, DummyClassId> orderKeySelector =
                item => new DummyClassId(item!.Id, keySelector(item.Value));
            IReadOnlyList<DummyClassId> order = new[]
            {
                source[2],
                source[0],
                source[4]
            };
            Func<DummyClassId?, DummyClassId> resultSelector =
                item => new DummyClassId(item!.Id, keySelector(item.Value));
            Func<DummyClassId?, DummyClassId?, DummyClassId> sourceResultSelector =
                (source, order) => resultSelector(source);
            IReadOnlyList<DummyClassId> expectedCollection = new[]
            {
                source[2],
                source[3],
                source[0],
                source[1],
                source[4],
                source[5]
            }
            .Select(resultSelector)
            .ToReadOnlyList();

            // Using special comparer to take into account only "Value" property.
            var comparer = MockEqualityComparer.Setup(
                DummyClassIdValueOnlyEqualityComparer.Instance, expectedCollection
            );
            var explosiveSource = ExplosiveEnumerable.CreateNotExplosive(source);
            var explosiveOrder = ExplosiveEnumerable.CreateNotExplosive(order);

            // Act.
            var actualCollection = explosiveSource.OrderBySequence(
                explosiveOrder, sourceKeySelector, orderKeySelector, sourceResultSelector, comparer
            );

            // Assert.
            // Using default comparer to ensure that objects preserve their places (stable sort).
            Assert.Equal(expectedCollection, actualCollection);
            VerifyCallsForOrderBySequence(comparer, source, order, expectedCollection);
            CustomAssert.True(explosiveSource.VerifyOnceEnumerateWholeCollection(source));
            CustomAssert.True(explosiveOrder.VerifyOnceEnumerateWholeCollection(order));
        }

        #endregion

        #region Private Methods

        private static void VerifyCallsForOrderBySequence<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order,
            IReadOnlyList<T> expected)
        {
            if (TestDotNetPlatformHelper.IsNetFramework())
            {
                VerifyCallsForOrderBySequenceForNetFramework(comparer, source, order, expected);
            }
            else
            {
                VerifyCallsForOrderBySequenceForNetCore(comparer, source, order, expected);
            }
        }

        private static void VerifyCallsForOrderBySequenceForNetFramework<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order,
            IReadOnlyList<T> expected)
        {
            if (expected.Count > 0 && order.Count > 0)
            {
                int expectedNumberOfCalls = Math.Max(expected.Count, order.Count);
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
            MockEqualityComparer<T> comparer, IReadOnlyList<T> source, IReadOnlyList<T> order,
            IReadOnlyList<T> expected)
        {
            if (expected.Count > 0 && order.Count > 0)
            {
                int expectedNumberOfCalls = Math.Max(expected.Count, order.Count);
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
