using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.Zip
{
    public sealed partial class ZipThreeTests
    {
        public ZipThreeTests()
        {
        }

        #region Null Values

        [Fact]
        public void ZipThree_ForNullFirstValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? first = null;
            IEnumerable<int> second = Enumerable.Empty<int>();
            IEnumerable<int> third = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("first", () => first!.ZipThree(second, third));
        }

        [Fact]
        public void ZipThree_ForNullSecondValue_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            const IEnumerable<int>? second = null;
            IEnumerable<int> third = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("second", () => first.ZipThree(second!, third));
        }

        [Fact]
        public void ZipThree_ForNullThirdValue_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<int> second = Enumerable.Empty<int>();
            const IEnumerable<int>? third = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("third", () => first.ZipThree(second, third!));
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ZipThree_ForEmptyFirstCollection_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IEnumerable<int> first = Enumerable.Empty<int>();
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> third = TestDataCreator.CreateRandomInt32List(count);

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Empty(actualCollection);
        }

        [Fact]
        public void ZipThree_ForEmptySecondCollection_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(count);
            IEnumerable<int> second = Enumerable.Empty<int>();
            IReadOnlyList<int> third = TestDataCreator.CreateRandomInt32List(count);

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Empty(actualCollection);
        }

        [Fact]
        public void ZipThree_ForEmptyThirdCollection_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(count);
            IEnumerable<int> third = Enumerable.Empty<int>();

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Empty(actualCollection);
        }

        [Fact]
        public void ZipThree_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<int> second = Enumerable.Empty<int>();
            IEnumerable<int> third = Enumerable.Empty<int>();

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Empty(actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ZipThree_ForPredefinedCollection_ShouldUniteToSingleCollection()
        {
            // Arrange.
            IReadOnlyList<int> first = new[] { 1, 2, 3 };
            IReadOnlyList<int> second = new[] { 1, 3, 1 };
            IReadOnlyList<int> third = new[] { 2, 3, 1 };
            var expectedCollection = GetExpectedCollection(first, second, third);

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ZipThree_ForCollectionsWithSomeItems_ShouldUniteToSingleCollection(int count)
        {
            // Arrange.
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> third = TestDataCreator.CreateRandomInt32List(count);
            var expectedCollection = GetExpectedCollection(first, second, third);

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ZipThree_ForCollectionsWithRandomSize_ShouldUniteToSingleCollection()
        {
            // Arrange.
            int countFirst = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(countFirst);
            int countSecond = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(countSecond);
            int countThird = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> third = TestDataCreator.CreateRandomInt32List(countThird);
            var expectedCollection = GetExpectedCollection(first, second, third);

            // Act.
            var actualCollection = first.ZipThree(second, third);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        #endregion

        #region Private Methods

        private static IReadOnlyList<(int first, int second, int third)> GetExpectedCollection(
            IReadOnlyList<int> first, IReadOnlyList<int> second, IReadOnlyList<int> third)
        {
            var minSize = Math.Min(first.Count, Math.Min(second.Count, third.Count));
            var expectedCollection = new List<(int first, int second, int third)>(minSize);
            for (int index = 0; index < minSize; ++index)
            {
                var item = ValueTuple.Create(first[index], second[index], third[index]);
                expectedCollection.Add(item);
            }

            return expectedCollection;
        }

        #endregion
    }
}
