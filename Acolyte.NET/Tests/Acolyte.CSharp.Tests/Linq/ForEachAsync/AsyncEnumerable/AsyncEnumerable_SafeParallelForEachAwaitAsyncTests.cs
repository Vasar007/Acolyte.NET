#pragma warning disable format // dotnet format fails indentation for regions :(

#if ASYNC_ENUMERABLE

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Threading;
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

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            IReadOnlyList<int> expectedCollection = await emptyCollection.ToArrayAsync();

            var actual = new ConcurrentBag<int>();
            Func<int, Task> action = item =>
            {
                actual.Add(item);
                return Task.CompletedTask;
            };

            // Act.
            Result<NoneResult, Exception>[] result =
                await emptyCollection.SafeParallelForEachAwaitAsync(action);

            IReadOnlyList<Exception> exceptions = result.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(exceptions);
            Assert.Equal(expectedCollection, actual.ToArray());
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithIndex_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, int, int> select = (item, index) => item * (index + 1);
            IReadOnlyList<int> expectedCollection = await emptyCollection
                .Select(select)
                .ToArrayAsync();

            var actual = new ConcurrentBag<int>();
            Func<int, int, Task> action = (item, index) =>
            {
                actual.Add(select(item, index));
                return Task.CompletedTask;
            };

            // Act.
            Result<NoneResult, Exception>[] result =
                await emptyCollection.SafeParallelForEachAwaitAsync(action);

            IReadOnlyList<Exception> exceptions = result.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(exceptions);
            Assert.Equal(expectedCollection, actual.ToArray());
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithSelector_ForEmptyCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> emptyCollection = AsyncEnumerable.Empty<int>();
            Func<int, bool> transform = item => NumberParityFunction.IsEven(item);
            IReadOnlyList<bool> expectedCollection = await emptyCollection
                .Select(transform)
                .ToListAsync();
            Func<int, Task<bool>> action = item => Task.FromResult(transform(item));

            // Act.
            Result<bool, Exception>[] actualCollection =
                await emptyCollection.SafeParallelForEachAwaitAsync(action);

            (IReadOnlyList<bool> taskResults, IReadOnlyList<Exception> taskExceptions) =
                actualCollection.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(taskExceptions);
            Assert.Equal(expectedCollection, taskResults);
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithSelectorAndIndex_ForEmptyCollection_ShouldDoNothing()
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
            Result<bool, Exception>[] actualCollection =
                await emptyCollection.SafeParallelForEachAwaitAsync(action);

            (IReadOnlyList<bool> taskResults, IReadOnlyList<Exception> taskExceptions) =
                actualCollection.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(taskExceptions);
            Assert.Equal(expectedCollection, taskResults);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_ForPredefinedCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> predefinedCollection = AsyncEnumerable.Range(1, 3);
            IReadOnlyList<int> expectedCollection = await predefinedCollection.ToArrayAsync();

            var actual = new ConcurrentBag<int>();
            Func<int, Task> action = item =>
            {
                actual.Add(item);
                return Task.CompletedTask;
            };

            // Act.
            Result<NoneResult, Exception>[] result =
                await predefinedCollection.SafeParallelForEachAwaitAsync(action);

            IReadOnlyList<Exception> exceptions = result.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(exceptions);
            Assert.NotStrictEqual(expectedCollection, actual.ToArray());
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithIndex_ForPredefinedCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> predefinedCollection = AsyncEnumerable.Range(1, 3);
            Func<int, int, int> select = (item, index) => item * (index + 1);
            IReadOnlyList<int> expectedCollection = await predefinedCollection
                .Select(select)
                .ToArrayAsync();

            var actual = new ConcurrentBag<int>();
            Func<int, int, Task> action = (item, index) =>
            {
                actual.Add(select(item, index));
                return Task.CompletedTask;
            };

            // Act.
            Result<NoneResult, Exception>[] result =
                await predefinedCollection.SafeParallelForEachAwaitAsync(action);

            IReadOnlyList<Exception> exceptions = result.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(exceptions);
            Assert.NotStrictEqual(expectedCollection, actual.ToArray());
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithSelector_ForPredefinedCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> predefinedCollection = AsyncEnumerable.Range(1, 3);
            Func<int, bool> transform = item => NumberParityFunction.IsEven(item);
            IReadOnlyList<bool> expectedCollection = await predefinedCollection
                .Select(transform)
                .ToListAsync();
            Func<int, Task<bool>> action = item => Task.FromResult(transform(item));

            // Act.
            Result<bool, Exception>[] actualCollection =
                await predefinedCollection.SafeParallelForEachAwaitAsync(action);

            (IReadOnlyList<bool> taskResults, IReadOnlyList<Exception> taskExceptions) =
                actualCollection.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(taskExceptions);
            Assert.NotStrictEqual(expectedCollection, taskResults);
        }

        [Fact]
        public async Task SafeParallelForEachAwaitAsync_WithSelectorAndIndex_ForPredefinedCollection_ShouldDoNothing()
        {
            // Arrange.
            IAsyncEnumerable<int> predefinedCollection = AsyncEnumerable.Range(1, 3);
            Func<int, int, bool> transform =
                (item, index) => NumberParityFunction.IsEven(item + index);
            IReadOnlyList<bool> expectedCollection = await predefinedCollection
                .Select(transform)
                .ToListAsync();
            Func<int, int, Task<bool>> action =
                (item, index) => Task.FromResult(transform(item, index));

            // Act.
            Result<bool, Exception>[] actualCollection =
                await predefinedCollection.SafeParallelForEachAwaitAsync(action);

            (IReadOnlyList<bool> taskResults, IReadOnlyList<Exception> taskExceptions) =
                actualCollection.UnwrapResultsOrExceptions();

            // Assert.
            Assert.Empty(taskExceptions);
            Assert.NotStrictEqual(expectedCollection, taskResults);
        }

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
