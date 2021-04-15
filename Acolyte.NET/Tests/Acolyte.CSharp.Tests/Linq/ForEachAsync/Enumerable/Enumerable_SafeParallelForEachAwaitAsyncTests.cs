using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acolyte.Functions;
using Acolyte.Linq;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class Enumerable_SafeParallelForEachAwaitAsyncTests
    {
        public Enumerable_SafeParallelForEachAwaitAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public void SafeParallelForEachAwaitAsync_Enumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_Enumerable_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task> discard = DiscardFunction<int>.FuncWithIndexAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_Enumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_Enumerable_WithSelectorAndIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task<bool>> discard = DiscardFunction<int, bool>.FuncWithIndexAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
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
