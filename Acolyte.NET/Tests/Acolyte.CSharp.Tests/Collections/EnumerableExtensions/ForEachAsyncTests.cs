﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Threading;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ParallelForEachAwaitAsyncTests
    {
        public ParallelForEachAwaitAsyncTests()
        {
        }

        #region Null Values

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task> discard = DiscardFunction<int>.FuncWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithSelectorAndIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, int, Task<bool>> discard = DiscardFunction<int, bool>.FuncWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

#if NETSTANDARD2_1

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task> discard = DiscardFunction<int>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, int, Task> discard = DiscardFunction<int>.FuncWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithSelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, Task<bool>> discard = DiscardFunction<int, bool>.FuncAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithSelectorAndIndex_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IAsyncEnumerable<int>? nullValue = null;
            Func<int, int, Task<bool>> discard = DiscardFunction<int, bool>.FuncWithIndexAsync;

            // Act & Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(
                "source", () => nullValue!.ParallelForEachAwaitAsync(discard)
            );
        }

#endif

        #endregion

        #region Empty Values

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int> select = item => item;
            int expectedValue = emptyCollection.Select(select).Sum();

            var actual = new CounterInt32();
            Func<int, Task> action = item =>
            {
                actual.Advance(item);
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedValue, actual.Value);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int, int> select = (item, index) => item * (index + 1);
            int expectedValue = emptyCollection.Select(select).Sum();

            var actual = new CounterInt32();
            Func<int, int, Task> action = (item, index) =>
            {
                actual.Advance(select(item, index));
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedValue, actual.Value);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithSelector_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, bool> transform = item => NumberParityFunction.IsEven(item);
            IReadOnlyList<bool> expectedCollection = emptyCollection
                .Select(transform)
                .ToList();
            Func<int, Task<bool>> action = item => Task.FromResult(transform(item));

            // Act.
            IReadOnlyList<bool> actualCollection =
                await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_Enumerable_WithSelectorAndIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, int, bool> transform =
                (item, index) => NumberParityFunction.IsEven(item + index);
            IReadOnlyList<bool> expectedCollection = emptyCollection
                .Select(transform)
                .ToList();
            Func<int, int, Task<bool>> action =
                (item, index) => Task.FromResult(transform(item, index));

            // Act.
            IReadOnlyList<bool> actualCollection =
                await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

#if NETSTANDARD2_1

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, int, int> select = (item, index) => item * (index + 1);
            int expectedValue = await emptyCollection.Select(select).SumAsync();

            var actual = new CounterInt32();
            Func<int, int, Task> action = (item, index) =>
            {
                actual.Advance(select(item, index));
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedValue, actual.Value);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, int, int> select = (item, index) => item * (index + 1);
            int expectedValue = await emptyCollection.Select(select).SumAsync();

            var actual = new CounterInt32();
            Func<int, int, Task> action = (item, index) =>
            {
                actual.Advance(select(item, index));
                return Task.CompletedTask;
            };

            // Act.
            await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedValue, actual.Value);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithSelector_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, bool> transform = item => NumberParityFunction.IsEven(item);
            IReadOnlyList<bool> expectedCollection = await emptyCollection
                .Select(transform)
                .ToListAsync();
            Func<int, Task<bool>> action = item => Task.FromResult(transform(item));

            // Act.
            IReadOnlyList<bool> actualCollection = await emptyCollection.ParallelForEachAwaitAsync(action);

            // Assert.
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public async Task ParallelForEachAwaitAsync_AsyncEnumerable_WithSelectorAndIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, int, bool> transform =
                (item, index) => NumberParityFunction.IsEven(item + index);
            IReadOnlyList<bool> expectedCollection = await emptyCollection
                .Select(transform)
                .ToListAsync();
            Func<int, int, Task<bool>> action =
                (item, index) => Task.FromResult(transform(item, index));

            // Act.
            IReadOnlyList<bool> actualCollection =
                await emptyCollection.ParallelForEachAwaitAsync(action);

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
