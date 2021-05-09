using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class ToReadOnlyCollectionsTests
    {
        public ToReadOnlyCollectionsTests()
        {
        }

        #region ToReadOnlyDictionary

        #region Null Values

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;

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

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.ToReadOnlyDictionary(discardKeySelector, discardElementSelector)
            );
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.ToReadOnlyDictionary(nullKeySelector!, discardElementSelector)
            );
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullElementSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "elementSelector",
                () => emptyCollection.ToReadOnlyDictionary(discardKeySelector, nullElementSelector!)
            );
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Func;
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
            Func<int, int> discardElementSelector = DiscardFunction<int, int>.Func;
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
            Func<int, Guid> discardKeySelector = DiscardFunction<int, Guid>.Func;
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

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelector_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> keySelector = KeyFunction<int>.Simple;
            Func<int, int> elementSelector = IdentityFunction<int>.Instance;
            var expectedDictionary = emptyCollection.ToDictionary(keySelector, elementSelector);

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                keySelector, elementSelector
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            Func<int, Guid> keySelector = KeyFunction<int>.Simple;
            Func<int, int> elementSelector = IdentityFunction<int>.Instance;
            var comparer = MockEqualityComparer<Guid>.Default;
            var expectedDictionary = emptyCollection.ToDictionary(
                keySelector, elementSelector, comparer
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                keySelector, elementSelector, comparer
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
            comparer.VerifyNoCalls();
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

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var comparer = MockEqualityComparer<Guid>.Default;
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

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
        [ClassData(typeof(PositiveTestConstants))]
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

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
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

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
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

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForCollectionWithRandomSize_ShouldReturnProperDictionary()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<long> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector
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

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IReadOnlyList<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            var keyGenerator = new IncrementalKeyGenerator<long>();
            Func<long, long> elementSelector = IdentityFunction<long>.Instance;
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, elementSelector
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, elementSelector
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

        #region Private Methods

        private static void VerifyMethodsCallsForToReadOnlyDictionary<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> collection)
        {
            comparer.VerifyEqualsNoCalls();
            comparer.VerifyGetHashCodeCallsTwiceForEach(collection);
        }

        #endregion

        #endregion

        #region ToReadOnlyList

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
        [ClassData(typeof(PositiveTestConstants))]
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

        #endregion

        #region ToReadOnlyCollection

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
            IReadOnlyCollection<int> expectedCollection = emptyCollection.ToList();

            // Act.
            var actualCollection = emptyCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.Empty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ToReadOnlyCollection_ForPredefinedCollection_ShouldReturnFilledCollection()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            IReadOnlyCollection<int> expectedCollection = predefinedCollection.ToList();

            // Act.
            var actualCollection = predefinedCollection.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.NotEmpty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void ToReadOnlyCollection_ForCollectionWithSomeItems_ShouldReturnFilledCollection(
            int count)
        {
            // Arrange.
            IReadOnlyCollection<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyCollection<int> expectedCollection = collectionWithSomeItems.ToList();

            // Act.
            var actualCollection = collectionWithSomeItems.ToReadOnlyCollection();

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
            IReadOnlyCollection<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyCollection<int> expectedCollection = collectionWithRandomSize.ToList();

            // Act.
            var actualCollection = collectionWithRandomSize.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            if (collectionWithRandomSize.Any())
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
            IReadOnlyCollection<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyCollection<int> expectedCollection = collectionWithRandomSize.ToList();

            // Act.
            var actualCollection = collectionWithRandomSize.ToReadOnlyCollection();

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

        #endregion
    }
}
