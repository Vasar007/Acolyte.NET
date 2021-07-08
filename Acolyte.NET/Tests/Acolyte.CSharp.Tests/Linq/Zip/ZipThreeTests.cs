using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
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
