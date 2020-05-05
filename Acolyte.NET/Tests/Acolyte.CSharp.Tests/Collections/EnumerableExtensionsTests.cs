using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Tests;

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
        public void CallIsNullOrEmptyForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.True(nullValue.IsNullOrEmpty());
        }

        [Fact]
        public void CallIsNullOrEmptyForEmptyCollection()
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
        public void CallIsNullOrEmptyForCollectionWithSomeItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.False(collectionWithSomeItems.IsNullOrEmpty());
        }

        [Fact]
        public void CallIsNullOrEmptyForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int> collectionWithRandomSize = TestDataCreator.CreateRandomInt32List(count);

            // Act.
            bool collectionIsNullOrEmpty = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            if (collectionWithRandomSize.Any())
            {
                Assert.False(collectionIsNullOrEmpty);
            }
            else
            {
                Assert.True(collectionIsNullOrEmpty);
            }
        }

        [Fact]
        public void CallIsNullOrEmptyForCollectionWithRandomNumberAndNullValues()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(MaxTestCollectionSize);
            IEnumerable<int?> collectionWithRandomSize = Enumerable
                .Range(1, count)
                .Select(i => TestDataCreator.CreateRandomNonNegativeInt32())
                .Select(value => (value & 1) == 0 ? value : (int?) null) // Convert all odd valies to null.
                .ToReadOnlyList();

            // Act.
            bool collectionIsNullOrEmpty = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            if (collectionWithRandomSize.Any())
            {
                Assert.False(collectionIsNullOrEmpty);
            }
            else
            {
                Assert.True(collectionIsNullOrEmpty);
            }
        }

        #endregion

        #region Tests for "First Or Default" section.

        // TODO: write tests.

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
