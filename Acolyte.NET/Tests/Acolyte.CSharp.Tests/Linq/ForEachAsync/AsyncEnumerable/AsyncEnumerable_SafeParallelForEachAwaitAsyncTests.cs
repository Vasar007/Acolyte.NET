#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_ENUMERABLE

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acolyte.Functions;
using Acolyte.Linq;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class AsyncEnumerable_SafeParallelForEachAwaitAsyncTests
    {
        public AsyncEnumerable_SafeParallelForEachAwaitAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public void SafeParallelForEachAwaitAsync_AsyncEnumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.InstanceAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_AsyncEnumerable_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, int, Task> discard = DiscardFunction<int>.InstanceWithIndexAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_AsyncEnumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.InstanceAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.SafeParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public void SafeParallelForEachAwaitAsync_AsyncEnumerable_WithSelectorAndIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, int, Task<bool>> discard = DiscardFunction<int, bool>.InstanceWithIndexAsync;

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

#endif
