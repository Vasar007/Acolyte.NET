using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq.ToReadOnlyCollection
{
    public sealed partial class ToReadOnlyDictionaryTests
    {
        #region Null Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.ToReadOnlyDictionary(discardKeySelector, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.ToReadOnlyDictionary(nullKeySelector!, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, comparer: null
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer: null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> keySelector = KeyFunction<int>.Simple;
            var comparer = MockEqualityComparer<Guid>.Default;
            var expectedDictionary = emptyCollection.ToDictionary(keySelector, comparer);

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(keySelector, comparer);

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
            comparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer
            );

            // Assert.
            // Fist check mock, than collections on equality.
            // Otherwise, comparer will be called during equality check.
            VerifyMethodsCallsForToReadOnlyDictionary(comparer, predefinedCollection);
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
      int count)
        {
            // Arrange.
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer
            );

            // Assert.
            // Fist check mock, than collections on equality.
            // Otherwise, comparer will be called during equality check.
            VerifyMethodsCallsForToReadOnlyDictionary(comparer, collectionWithSomeItems);
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Random Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForCollectionWithRandomSize_ShouldReturnProperDictionary()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            // Use default comparer to avoid long running tests.
            var comparer = EqualityComparer<long>.Default;
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer
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
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer
            );

            // Assert.
            // Fist check mock, than collections on equality.
            // Otherwise, comparer will be called during equality check.
            VerifyMethodsCallsForToReadOnlyDictionary(comparer, collectionWithSomeItems);
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

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ShouldUseEqualityComparerDuringEqualityCheck()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, comparer
            );

            // Assert.
            // Fist check mock, than collections on equality.
            // Otherwise, comparer will be called during equality check.
            VerifyMethodsCallsForToReadOnlyDictionary(comparer, collectionWithSomeItems);
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
            comparer.VerifyEqualsCallsTwiceForEach(collectionWithSomeItems);
        }

        #endregion
    }
}
