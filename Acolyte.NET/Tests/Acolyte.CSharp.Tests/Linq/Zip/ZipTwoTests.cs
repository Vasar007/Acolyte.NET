using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
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

        #endregion

        #region Some Values

        #endregion

        #region Random Values

        #endregion

        #region Extended Logical Coverage

        #endregion
    }
}
