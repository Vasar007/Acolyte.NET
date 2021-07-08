using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Xunit;

namespace Acolyte.Tests.Linq.Zip
{
    public sealed partial class ZipThreeTests
    {
        #region Null Values

        [Fact]
        public void ZipThree_WithSelector_ForNullFirstValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? first = null;
            IEnumerable<int> second = Enumerable.Empty<int>();
            IEnumerable<int> third = Enumerable.Empty<int>();
            var discard = DiscardFunction<int, int, int, int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "first", () => first!.ZipThree(second, third, discard)
            );
        }

        [Fact]
        public void ZipThree_WithSelector_ForNullSecondValue_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            const IEnumerable<int>? second = null;
            IEnumerable<int> third = Enumerable.Empty<int>();
            var discard = DiscardFunction<int, int, int, int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "second", () => first.ZipThree(second!, third, discard)
            );
        }

        [Fact]
        public void ZipThree_WithSelector_ForNullThirdValue_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<int> second = Enumerable.Empty<int>();
            const IEnumerable<int>? third = null;
            var discard = DiscardFunction<int, int, int, int>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "third", () => first.ZipThree(second, third!, discard)
            );
        }

        [Fact]
        public void ZipThree_WithSelector_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> first = Enumerable.Empty<int>();
            IEnumerable<int> second = Enumerable.Empty<int>();
            IEnumerable<int> third = Enumerable.Empty<int>();
            const Func<int, int, int, int>? selector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "resultSelector", () => first.ZipThree(second, third, selector!)
            );
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
