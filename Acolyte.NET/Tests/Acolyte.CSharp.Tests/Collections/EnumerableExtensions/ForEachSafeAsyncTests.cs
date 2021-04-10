#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Collections;
using Acolyte.Functions;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ForEachSafeAsyncTests
    {
        public ForEachSafeAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public void ForEachSafeAsync_Enumerable_WithToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public void ForEachSafeAsync_Enumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard)
            );
        }

        [Fact]
        public void ForEachSafeAsync_Enumerable_WithSelectorAndToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public void ForEachSafeAsync_Enumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard)
            );
        }

#if NETSTANDARD2_1

        [Fact]
        public void ForEachSafeAsync_AsyncEnumerable_WithToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public void ForEachSafeAsync_AsyncEnumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard)
            );
        }

        [Fact]
        public void ForEachSafeAsync_AsyncEnumerable_WithSelectorAndToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public void ForEachSafeAsync_AsyncEnumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachSafeAsync(discard)
            );
        }

#endif

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
