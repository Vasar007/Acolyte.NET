using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class ToReadOnlyListTests
    {
        public ToReadOnlyListTests()
        {
        }

        #region Null Values

        [Fact]
        public void ToReadOnlyList_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.ToReadOnlyList());
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ToReadOnlyList_ForEmptyCollection_ShouldReturnEmptyList()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedList = emptyCollection.ToList();

            // Act.
            var actualList = emptyCollection.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualList);
            Assert.Empty(actualList);
            Assert.Equal(expectedList, actualList);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyList_ForPredefinedCollection_ShouldReturnFilledList()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            var expectedList = predefinedCollection.ToList();

            // Act.
            var actualList = predefinedCollection.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualList);
            Assert.NotEmpty(actualList);
            Assert.Equal(expectedList, actualList);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ToReadOnlyList_ForCollectionWithSomeItems_ShouldReturnFilledList(int count)
        {
            // Arrange.
            IReadOnlyCollection<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            var expectedList = collectionWithSomeItems.ToList();

            // Act.
            var actualList = collectionWithSomeItems.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualList);
            Assert.NotEmpty(actualList);
            Assert.Equal(expectedList, actualList);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ToReadOnlyList_ForCollectionWithRandomSize_ShouldReturnProperList()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyCollection<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var expectedList = collectionWithRandomSize.ToList();

            // Act.
            var actualList = collectionWithRandomSize.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualList);
            if (collectionWithRandomSize.Any())
            {
                Assert.NotEmpty(actualList);
            }
            else
            {
                Assert.Empty(actualList);
            }
            Assert.Equal(expectedList, actualList);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToReadOnlyList_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyCollection<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var expectedList = collectionWithRandomSize.ToList();

            // Act.
            var actualList = collectionWithRandomSize.ToReadOnlyList();

            // Assert.
            Assert.IsAssignableFrom<IList<int>>(actualList);
            if (actualList is IList<int> list)
            {
                Assert.True(list.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => list.Add(default));
                Assert.Throws<NotSupportedException>(() => list.Clear());
                Assert.Throws<NotSupportedException>(() => list.Remove(default));
                Assert.Throws<NotSupportedException>(() => list[default] = default);
                Assert.Throws<NotSupportedException>(() => list.Insert(default, default));
                Assert.Throws<NotSupportedException>(() => list.RemoveAt(default));
            }
            else
            {
                string message =
                     "Method 'ToReadOnlyList' " +
                    $"returns collection with invalid type '{actualList.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IList<int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        #endregion
    }
}
