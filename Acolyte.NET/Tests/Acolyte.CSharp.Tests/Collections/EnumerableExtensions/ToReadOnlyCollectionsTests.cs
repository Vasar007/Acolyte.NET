using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ToReadOnlyCollectionsTests
    {
        public ToReadOnlyCollectionsTests()
        {
        }

        #region ToReadOnlyDictionary

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const Func<int, Guid>? nullKeySelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.ToReadOnlyDictionary(nullKeySelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.ToReadOnlyDictionary(nullKeySelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const Func<int, Guid>? nullKeySelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.ToReadOnlyDictionary(nullKeySelector, null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.ToReadOnlyDictionary(nullKeySelector, null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, comparer: null
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, comparer: null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const Func<int, Guid>? nullKeySelector = null;
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.ToReadOnlyDictionary(nullKeySelector, nullElementSelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.ToReadOnlyDictionary(nullKeySelector, nullElementSelector)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForNullElementSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "elementSelector",
                () => emptyCollection.ToReadOnlyDictionary(
                    KeyFunction<int>.Simple, nullElementSelector
                )
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const Func<int, Guid>? nullKeySelector = null;
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue.ToReadOnlyDictionary(nullKeySelector, nullElementSelector, null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullKeySelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, Guid>? nullKeySelector = null;
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.ToReadOnlyDictionary(
                    nullKeySelector, nullElementSelector, null
                )
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullElementSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            const Func<int, int>? nullElementSelector = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "elementSelector",
                () => emptyCollection.ToReadOnlyDictionary(
                    KeyFunction<int>.Simple, nullElementSelector, null
                )
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance, comparer: null
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance, comparer: null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(KeyFunction<int>.Simple);

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(KeyFunction<int>.Simple);

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
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, EqualityComparer<Guid>.Default
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, EqualityComparer<Guid>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelector_ForEmptyCollection_ShouldReturnEmptyDictionary()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance
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
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple,
                IdentityFunction<int>.Instance,
                EqualityComparer<Guid>.Default
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple,
                IdentityFunction<int>.Instance,
                EqualityComparer<Guid>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            var keyGenerator = new IncrementalKeyGenerator<int>();
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
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForPredefinedCollection_ShouldReturnFilledDictionary()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
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
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = predefinedCollection.ToDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = predefinedCollection.ToReadOnlyDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyDictionary_WithKeySelector_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
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
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForCollectionWithSomeItems_ShouldReturnFilledDictionary(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithSomeItems.ToDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithSomeItems.ToReadOnlyDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.NotEmpty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelector_ForCollectionWithRandomSize_ShouldReturnProperDictionary()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
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
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
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
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
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
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
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
        public void ToReadOnlyDictionary_WithKeySelector_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(keyGenerator.GetKey);

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey
            );

            // Assert.
            Assert.IsAssignableFrom<IDictionary<long, int>>(actualDictionary);
            if (actualDictionary is IDictionary<long, int> dictionary)
            {
                Assert.True(dictionary.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default));
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default, default));
                Assert.Throws<NotSupportedException>(() => dictionary.Clear());
                Assert.Throws<NotSupportedException>(
                    () => dictionary.Remove(default(KeyValuePair<long, int>))
                );
                Assert.Throws<NotSupportedException>(() => dictionary.Remove(default));
                Assert.Throws<NotSupportedException>(() => dictionary[default] = default);
            }
            else
            {
                string message =
                    "Method 'ToReadOnlyDictionary' " +
                    $"returns collection with invalid type '{actualDictionary.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeySelectorAndComparer_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, EqualityComparer<long>.Default
            );

            // Assert.
            Assert.IsAssignableFrom<IDictionary<long, int>>(actualDictionary);
            if (actualDictionary is IDictionary<long, int> dictionary)
            {
                Assert.True(dictionary.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default));
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default, default));
                Assert.Throws<NotSupportedException>(() => dictionary.Clear());
                Assert.Throws<NotSupportedException>(
                    () => dictionary.Remove(default(KeyValuePair<long, int>))
                );
                Assert.Throws<NotSupportedException>(() => dictionary.Remove(default));
                Assert.Throws<NotSupportedException>(() => dictionary[default] = default);
            }
            else
            {
                string message =
                    "Method 'ToReadOnlyDictionary' " +
                    $"returns collection with invalid type '{actualDictionary.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectors_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey, IdentityFunction<int>.Instance
            );

            // Assert.
            Assert.IsAssignableFrom<IDictionary<long, int>>(actualDictionary);
            if (actualDictionary is IDictionary<long, int> dictionary)
            {
                Assert.True(dictionary.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default));
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default, default));
                Assert.Throws<NotSupportedException>(() => dictionary.Clear());
                Assert.Throws<NotSupportedException>(
                    () => dictionary.Remove(default(KeyValuePair<long, int>))
                );
                Assert.Throws<NotSupportedException>(() => dictionary.Remove(default));
                Assert.Throws<NotSupportedException>(() => dictionary[default] = default);
            }
            else
            {
                string message =
                    "Method 'ToReadOnlyDictionary' " +
                    $"returns collection with invalid type '{actualDictionary.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            var keyGenerator = new IncrementalKeyGenerator<int>();
            var expectedDictionary = collectionWithRandomSize.ToDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Act.
            keyGenerator.Reset();
            var actualDictionary = collectionWithRandomSize.ToReadOnlyDictionary(
                keyGenerator.GetKey,
                IdentityFunction<int>.Instance,
                EqualityComparer<long>.Default
            );

            // Assert.
            Assert.IsAssignableFrom<IDictionary<long, int>>(actualDictionary);
            if (actualDictionary is IDictionary<long, int> dictionary)
            {
                Assert.True(dictionary.IsReadOnly);
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default));
                Assert.Throws<NotSupportedException>(() => dictionary.Add(default, default));
                Assert.Throws<NotSupportedException>(() => dictionary.Clear());
                Assert.Throws<NotSupportedException>(
                    () => dictionary.Remove(default(KeyValuePair<long, int>))
                );
                Assert.Throws<NotSupportedException>(() => dictionary.Remove(default));
                Assert.Throws<NotSupportedException>(() => dictionary[default] = default);
            }
            else
            {
                string message =
                    "Method 'ToReadOnlyDictionary' " +
                    $"returns collection with invalid type '{actualDictionary.GetType().Name}'. " +
                    $"Return type should be inherit from '{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        #endregion

        #region ToReadOnlyList

        [Fact]
        public void ToReadOnlyList_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.ToReadOnlyList());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

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

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyList_ForCollectionWithSomeItems_ShouldReturnFilledList(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            var expectedList = collectionWithSomeItems.ToList();

            // Act.
            var actualList = collectionWithSomeItems.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualList);
            Assert.NotEmpty(actualList);
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void ToReadOnlyList_ForCollectionWithRandomSize_ShouldReturnProperList()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
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

        [Fact]
        public void ToReadOnlyList_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
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

        #region ToReadOnlyCollection

        [Fact]
        public void ToReadOnlyCollection_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.ToReadOnlyCollection());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

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

        [Theory]
        [InlineData(Consts._1)]
        [InlineData(Consts._2)]
        [InlineData(Consts._5)]
        [InlineData(Consts._10)]
        [InlineData(Consts._100)]
        [InlineData(Consts._10_000)]
        public void ToReadOnlyCollection_ForCollectionWithSomeItems_ShouldReturnFilledCollection(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyCollection<int> expectedCollection = collectionWithSomeItems.ToList();

            // Act.
            var actualCollection = collectionWithSomeItems.ToReadOnlyCollection();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.NotEmpty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void ToReadOnlyCollection_ForCollectionWithRandomSize_ShouldReturnProperCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
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

        [Fact]
        public void ToReadOnlyCollection_ShouldReturnImmutableCollection()
        {
            // Arrange.
            const int count = 5;
            IEnumerable<int> collectionWithRandomSize =
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
    }
}
