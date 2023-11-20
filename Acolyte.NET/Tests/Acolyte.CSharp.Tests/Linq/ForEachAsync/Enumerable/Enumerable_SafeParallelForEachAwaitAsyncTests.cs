using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acolyte.Functions;
using Acolyte.Linq;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class Enumerable_SafeParallelForEachAwaitAsyncTests
    {
        public Enumerable_SafeParallelForEachAwaitAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_Enumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.InstanceAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_Enumerable_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task> discard = DiscardFunction<int>.InstanceWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_Enumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.InstanceAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_Enumerable_WithSelectorAndIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task<bool>> discard = DiscardFunction<int, bool>.InstanceWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
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
