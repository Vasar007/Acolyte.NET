using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
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
