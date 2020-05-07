using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Tests;
using System;

namespace Acolyte.Collections.Tests
{
    public sealed class EnumerableExtensionsTests
    {
        // Max collection size in C# is equal to 2_146_435_071.
        private const int MaxTestCollectionSize = 2_146_435;


        public EnumerableExtensionsTests()
        {
        }

        #region Tests for "Is Null Or Empty" section.

        [Fact]
        public void Call_IsNullOrEmpty_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.True(nullValue.IsNullOrEmpty());
        }

        [Fact]
        public void Call_IsNullOrEmpty_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.True(emptyCollection.IsNullOrEmpty());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10_000)]
        [InlineData(MaxTestCollectionSize)]
        public void Call_IsNullOrEmpty_ForCollectionWithSomeItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.False(collectionWithSomeItems.IsNullOrEmpty());
        }

        [Fact]
        public void Call_IsNullOrEmpty_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            bool expectedResult = !collectionWithRandomSize.Any();
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_IsNullOrEmpty_ForCollectionWithRandomNumberAndNullValues()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int?> collectionWithRandomSize = Enumerable
                .Range(1, count)
                .Select(i => TestDataCreator.CreateRandomNonNegativeInt32())
                // Convert all odd valies to null.
                .Select(value => (value & 1) == 0 ? value : (int?) null)
                .ToReadOnlyList();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            bool expectedResult = !collectionWithRandomSize.Any();
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "First Or Default" section.

        [Fact]
        public void Call_FirstOrDefault_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => nullValue.FirstOrDefault(default));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(
                () => nullValue.FirstOrDefault(_ => default, default)
            );
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> nullValue = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                () => nullValue.FirstOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_FirstOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.FirstOrDefault(expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.FirstOrDefault(_ => default, expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10_000)]
        [InlineData(MaxTestCollectionSize)]
        public void Call_FirstOrDefault_ForCollectionWithSomeItems_ShouldReturnFirstItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.First();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(default);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10_000)]
        [InlineData(MaxTestCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnFirstItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.First();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(
                i => i.Equals(expectedResult), default
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(10_000)]
        [InlineData(MaxTestCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(defaultResult);

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First()
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(
                i => i.Equals(i), defaultResult
            );

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First(i => i.Equals(i))
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "Index Of" section.

        // TODO: write tests.

        #endregion

        #region Tests for "To Read-Only Collections" section.

        // TODO: write tests.

        #endregion

        #region Tests for "Min/Max For Generic Types With Comparer" section.

        // TODO: write tests.

        #endregion

        #region Tests for "MinMax Methods" section.

        // TODO: write tests.

        #endregion

        #region Tests for "Min/Max By Key Methods" section.

        // TODO: write tests.

        #endregion

        #region Tests for "MinMax By Key Methods" section.

        // TODO: write tests.

        #endregion

        #region Tests for "Enmumerable To String" section.

        // TODO: write tests.

        #endregion

        #region Tests for "For Each Methods" section.

        // TODO: write tests.

        #endregion

        #region Tests for "Distinct By" section.

        // TODO: write tests.

        #endregion

        #region Tests for "Slicing" section.

        // TODO: write tests.

        #endregion
    }
}
