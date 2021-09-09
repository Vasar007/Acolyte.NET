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
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Instance;
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Instance;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.ToReadOnlyDictionary(
                    discardKeySelector, discardElementSelector, comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Instance;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.ToReadOnlyDictionary(
                    nullKeySelector!, discardElementSelector, comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullElementSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Instance;
            const Func<int, int>? nullElementSelector = null;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "elementSelector",
                () => emptyCollection.ToReadOnlyDictionary(
                    discardKeySelector, nullElementSelector!, comparer
                )
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer: null
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer: null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> keySelector = KeyFunction<int>.Simple;
            Func<int, int> elementSelector = IdentityFunction<int>.Instance;
            var comparer = MockEqualityComparer<Guid>.Default;

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                keySelector, elementSelector, comparer
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            comparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer
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
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer
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
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForCollectionWithRandomSize_ShouldReturnProperDictionary()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            // Use default comparer to avoid long running tests.
            var comparer = EqualityComparer<long>.Default;
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer
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
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Assert.
            // Fist check mock, than collections on equality.
            // Otherwise, comparer will be called during equality check.
            VerifyMethodsCallsForToReadOnlyDictionary(comparer, collectionWithSomeItems);
            Assert.IsAssignableFrom<IDictionary<long, long>>(actualDictionary);
            if (actualDictionary is IDictionary<long, long> dictionary)
            {
                Assert.Equal(expectedDictionary, actualDictionary);
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
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ShouldUseEqualityComparerDuringEqualityCheck()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var comparer = MockEqualityComparer<long>.Default;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, elementSelector, comparer
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector, comparer
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
