﻿using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Creators;
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

        #endregion

        #region Some Values

        #endregion

        #region Random Values

        #endregion

        #region Extended Logical Coverage

        #endregion

        #region Private Methods

        private static Func<T1, T2, T1> GetSourceResultSelector<T1, T2>()
        {
            return (source, order) => source;
        }

        #endregion
    }
}