#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using Acolyte.Collections;
using Acolyte.Functions;
using Xunit;

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
