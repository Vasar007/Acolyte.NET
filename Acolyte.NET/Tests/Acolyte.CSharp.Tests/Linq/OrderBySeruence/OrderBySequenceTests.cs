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
            IReadOnlyList<int> someOrder = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            var actualResult = emptyCollection.OrderBySequence(someOrder);

            Assert.Empty(actualResult);
        }

        [Fact]
        public void OrderBySequence_ForEmptyOrder_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> someSource = TestDataCreator.CreateRandomInt32List(count);
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
                TestDataCreator.CreateRandomInt32List(count);
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

        #region Random Values

        [Fact]
        public void OrderBySequence_ForCollectionWithRandomSize_ShouldOrderSource()
        {
            // Arrange.
            // "Enumerable.Join" has some issues when called for large collections.
            // See https://github.com/dotnet/runtime/issues/55219
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
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
        public void OrderBySequence_ShouldLookWholeCollectionToOrderSource()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            IReadOnlyList<int> order = new[] { 2, 1, 3, 4 };
            IReadOnlyList<int> expectedCollection = order;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);

            // Act.
            var actualCollection = explosive.OrderBySequence(order);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
        }

        #endregion

        #region Private Methods

        private static Func<T1, T2, T1> GetSourceResultSelector<T1, T2>()
        {
            return (source, order) => source;
        }

        #endregion
    }
}
