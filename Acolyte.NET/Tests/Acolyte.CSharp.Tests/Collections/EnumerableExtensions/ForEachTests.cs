using System;
using System.Collections.Generic;
using Xunit;
using Acolyte.Collections;
using Acolyte.Functions;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ForEachTests
    {
        public ForEachTests()
        {
        }

        [Fact]
        public void ForEach_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Action<int> discard = DiscardFunction<int>.Action;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.ForEach(discard));
        }
    }
}
