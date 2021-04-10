#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Collections;
using Acolyte.Functions;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ForEachAsyncTests
    {
        public ForEachAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public async Task ForEachAsync_Enumerable_WithToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard)
            );
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_WithSelectorAndToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard)
            );
        }

#if NETSTANDARD2_1

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard)
            );
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithSelectorAndToken_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;
            var cancellationToken = CancellationToken.None;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard, cancellationToken)
            );
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ForEachAsync(discard)
            );
        }

#endif

        #endregion

        #region Empty Values

        [Fact]
        public async Task ForEachAsync_Enumerable_WithToken_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = emptyCollection.ToList();
            var actualCollection = new List<int>();
            Func<int, Task> action = i =>
            {
                actualCollection.Add(i);
                return Task.CompletedTask;
            };
            var cancellationToken = CancellationToken.None;

            // Act.
            await emptyCollection.ForEachAsync(action, cancellationToken);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = emptyCollection.ToList();
            var actualCollection = new List<int>();
            Func<int, Task> action = i =>
            {
                actualCollection.Add(i);
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ForEachAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_WithSelectorAndToken_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> transform = i => NumberParityFunction.IsEven(i);
            IReadOnlyList<bool> expectedCollection = emptyCollection.Select(transform).ToList();
            Func<int, Task<bool>> action = i => Task.FromResult(transform(i));
            var cancellationToken = CancellationToken.None;

            // Act.
            IReadOnlyList<bool> actualCollection = await emptyCollection.ForEachAsync(
                action, cancellationToken
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_Enumerable_WithSelector_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> transform = i => NumberParityFunction.IsEven(i);
            IReadOnlyList<bool> expectedCollection = emptyCollection.Select(transform).ToList();
            Func<int, Task<bool>> action = i => Task.FromResult(transform(i));

            // Act.
            IReadOnlyList<bool> actualCollection = await emptyCollection.ForEachAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

#if NETSTANDARD2_1

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithToken_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = await emptyCollection.ToListAsync();
            var actualCollection = new List<int>();
            Func<int, Task> action = i =>
            {
                actualCollection.Add(i);
                return Task.CompletedTask;
            };
            var cancellationToken = CancellationToken.None;

            // Act.
            await emptyCollection.ForEachAsync(action, cancellationToken);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = await emptyCollection.ToListAsync();
            var actualCollection = new List<int>();
            Func<int, Task> action = i =>
            {
                actualCollection.Add(i);
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ForEachAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithSelectorAndToken_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, bool> transform = i => NumberParityFunction.IsEven(i);
            IReadOnlyList<bool> expectedCollection = await emptyCollection
                .Select(transform)
                .ToListAsync();
            Func<int, Task<bool>> action = i => Task.FromResult(transform(i));
            var cancellationToken = CancellationToken.None;

            // Act.
            IReadOnlyList<bool> actualCollection = await emptyCollection.ForEachAsync(
                action, cancellationToken
            );

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ForEachAsync_AsyncEnumerable_WithSelector_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, bool> transform = i => NumberParityFunction.IsEven(i);
            IReadOnlyList<bool> expectedCollection = await emptyCollection
                .Select(transform)
                .ToListAsync();
            Func<int, Task<bool>> action = i => Task.FromResult(transform(i));

            // Act.
            IReadOnlyList<bool> actualCollection = await emptyCollection.ForEachAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

#endif

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
