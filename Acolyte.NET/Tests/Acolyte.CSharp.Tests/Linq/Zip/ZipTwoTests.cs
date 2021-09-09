using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.Zip
{
    public sealed class ZipTwoTests
    {
        public ZipTwoTests()
        {
        }

        #region Null Values

        [Fact]
        public void ZipTwo_ForNullFirstValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? first = null;
            IEnumerable<int> second = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("first", () => first!.ZipTwo(second));
        }

        [Fact]
        public void ZipTwo_ForNullSecondValue_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            const IEnumerable<int>? second = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("second", () => first.ZipTwo(second!));
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ZipTwo_ForEmptyFirstCollection_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IEnumerable<int> first = Enumerable.Empty<int>();
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<(int, int)> expectedCollection = Enumerable.Empty<(int, int)>()
                .ToReadOnlyList();

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ZipTwo_ForEmptySecondCollection_ShouldDoNothing()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(count);
            IEnumerable<int> second = Enumerable.Empty<int>();
            IReadOnlyList<(int, int)> expectedCollection = Enumerable.Empty<(int, int)>()
                  .ToReadOnlyList();

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ZipTwo_ForEmptyValues_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<int> second = Enumerable.Empty<int>();
            IReadOnlyList<(int, int)> expectedCollection = Enumerable.Empty<(int, int)>()
                  .ToReadOnlyList();

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ZipTwo_ForPredefinedCollection_ShouldMergeIntoSingleCollection()
        {
            // Arrange.
            IReadOnlyList<int> first = new[] { 1, 2, 3 };
            IReadOnlyList<int> second = new[] { 1, 3, 2 };
            var expectedCollection = GetExpectedCollection(first, second);

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ZipTwo_ForCollectionsWithSomeItems_ShouldMergeIntoSingleCollection(int count)
        {
            // Arrange.
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(count);
            var expectedCollection = GetExpectedCollection(first, second);

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ZipTwo_ForCollectionsWithRandomSize_ShouldMergeIntoSingleCollection()
        {
            // Arrange.
            int countFirst = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> first = TestDataCreator.CreateRandomInt32List(countFirst);
            int countSecond = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> second = TestDataCreator.CreateRandomInt32List(countSecond);
            var expectedCollection = GetExpectedCollection(first, second);

            // Act.
            var actualCollection = first.ZipTwo(second);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ZipTwo_ShouldLookCollectionWithSmallestSizeMergeIntoSingleCollection()
        {
            // Arrange.
            IReadOnlyList<int> first = new[] { 1, 2, 3, 4 };
            IReadOnlyList<int> second = new[] { 1, 3, 4, 2, 5 };
            var explosiveFirst = ExplosiveEnumerable.CreateNotExplosive(first);
            var explosiveSecond = ExplosiveEnumerable.Create(second, explosiveIndex: first.Count);
            var expectedCollection = GetExpectedCollection(first, second);

            // Act.
            var actualCollection = explosiveFirst.ZipTwo(explosiveSecond);

            // Assert.
            // Here we should use exactly first collection because it is the smallest one.
            Assert.Equal(expectedCollection, actualCollection);
            CustomAssert.True(explosiveFirst.VerifyOnceEnumerateWholeCollection(first));
            CustomAssert.True(explosiveSecond.VerifyOnceEnumerateWholeCollection(first));
        }

        #endregion

        #region Private Methods

        private static IReadOnlyList<(int first, int second)> GetExpectedCollection(
            IReadOnlyList<int> first, IReadOnlyList<int> second)
        {
            var minSize = Math.Min(first.Count, second.Count);
            var expectedCollection = new List<(int first, int second)>(minSize);
            for (int index = 0; index < minSize; ++index)
            {
                var item = ValueTuple.Create(first[index], second[index]);
                expectedCollection.Add(item);
            }

            return expectedCollection;
        }

        #endregion
    }
}
