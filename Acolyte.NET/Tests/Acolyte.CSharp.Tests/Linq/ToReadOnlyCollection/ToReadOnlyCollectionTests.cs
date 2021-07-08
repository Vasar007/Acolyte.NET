using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class ToReadOnlyCollectionTests
    {
        public ToReadOnlyCollectionTests()
        {
        }

        #region Null Values

        [Fact]
        public void ToReadOnlyCollection_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.ToReadOnlyCollection());
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ToReadOnlyCollection_ForEmptyCollection_ShouldReturnEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualCollection = emptyCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.Empty(actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyCollection_ForPredefinedCollection_ShouldReturnFilledCollection()
        {
            // Arrange.
            IReadOnlyList<int> expectedCollection = new[] { 1, 2, 3 };

            // Act.
            var actualCollection = expectedCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.NotEmpty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ToReadOnlyCollection_ForCollectionWithSomeItems_ShouldReturnFilledCollection(
            int count)
        {
            // Arrange.
            IReadOnlyCollection<int> expectedCollection =
                TestDataCreator.CreateRandomInt32List(count);

            // Act.
            var actualCollection = expectedCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.NotEmpty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ToReadOnlyCollection_ForCollectionWithRandomSize_ShouldReturnProperCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyCollection<int> expectedCollection =
                TestDataCreator.CreateRandomInt32List(count);

            // Act.
            var actualCollection = expectedCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            if (expectedCollection.Any())
            {
                Assert.NotEmpty(actualCollection);
            }
            else
            {
                Assert.Empty(actualCollection);
            }
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToReadOnlyCollection_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyCollection<int> expectedCollection =
                TestDataCreator.CreateRandomInt32List(count);

            // Act.
            var actualCollection = expectedCollection.ToReadOnlyCollection();

            // Assert.
            Assert.IsAssignableFrom<ICollection<int>>(actualCollection);
            if (actualCollection is ICollection<int> collection)
            {
                Assert.True(collection.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => collection.Add(default));
                Assert.Throws<NotSupportedException>(() => collection.Clear());
                Assert.Throws<NotSupportedException>(() => collection.Remove(default));
            }
            else
            {
                string message =
                     "Method 'ToReadOnlyCollection' " +
                    $"returns collection with invalid type '{actualCollection.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(ICollection<int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        #endregion
    }
}
