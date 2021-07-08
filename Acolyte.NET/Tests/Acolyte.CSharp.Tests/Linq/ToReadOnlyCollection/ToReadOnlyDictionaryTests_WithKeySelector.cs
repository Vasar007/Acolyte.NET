using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.ToReadOnlyCollection
{
    public sealed partial class ToReadOnlyDictionaryTests
    {
        #region Null Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Instance;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.ToReadOnlyDictionary(discardKeySelector)
            );
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.ToReadOnlyDictionary(nullKeySelector!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> keySelector = KeyFunction<int>.Simple;
            var expectedDictionary = emptyCollection.ToDictionary(keySelector);

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(keySelector);

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var expectedDictionary = predefinedCollection.ToDictionary(keyGenerator.GetKey);

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ToReadOnlyDictionary_WithKeySelector_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var expectedDictionary = collectionWithSomeItems.ToDictionary(keyGenerator.GetKey);

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForCollectionWithRandomSize_ShouldReturnProperDictionary()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(keyGenerator.GetKey);

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            if (collectionWithRandomSize.Any())
            {
                Assert.NotEmpty(actualDictionary);
            }
            else
            {
                Assert.Empty(actualDictionary);
            }
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var expectedDictionary = collectionWithSomeItems.ToDictionary(keyGenerator.GetKey);

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey
            );

            // Assert.
            Assert.IsAssignableFrom<IDictionary<long, long>>(actualDictionary);
            if (actualDictionary is IDictionary<long, long> dictionary)
            {
                Assert.True(dictionary.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default));
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default, default));
                Assert.Throws<NotSupportedException>(() => dictionary.Clear());
                Assert.Throws<NotSupportedException>(
                    () => dictionary.Remove(default(KeyValuePair<long, long>))
                );
                Assert.Throws<NotSupportedException>(() => dictionary.Remove(default));
                Assert.Throws<NotSupportedException>(() => dictionary[default] = default);
            }
            else
            {
                string message =
                     "Method 'ToReadOnlyDictionary' " +
                    $"returns collection with invalid type '{actualDictionary.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IDictionary<long, long>)}'.";
                CustomAssert.Fail(message);
            }
        }

        #endregion
    }
}
