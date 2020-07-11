using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Tests;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;

namespace Acolyte.Collections.Tests.EnumerableExtensions
{
    public sealed class ToReadOnlyCollectionsTests
    {
        public ToReadOnlyCollectionsTests()
        {
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForNullValue()
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
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForNullKeySelector()
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
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullValue()
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
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullKeySelector()
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
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForNullComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(KeyFunction<int>.Simple, null);

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectors_ForNullValue()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectors_ForNullKeySelector()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectors_ForNullElementSelector()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullValue()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullKeySelector()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullElementSelector()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForNullComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance, null
            );

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance, null
            );

            // Assert.
            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyList_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.ToReadOnlyList());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_ToReadOnlyCollection_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.ToReadOnlyCollection());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(KeyFunction<int>.Simple);

            // Assert.
            var expectedDictionary = emptyCollection.ToDictionary(KeyFunction<int>.Simple);

            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, EqualityComparer<Guid>.Default
            );

            // Assert.
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, EqualityComparer<Guid>.Default
            );

            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeyElementSelector_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance
            );

            // Assert.
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple, IdentityFunction<int>.Instance
            );

            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualDictionary = emptyCollection.ToReadOnlyDictionary(
                KeyFunction<int>.Simple,
                IdentityFunction<int>.Instance,
                EqualityComparer<Guid>.Default
            );

            // Assert.
            var expectedDictionary = emptyCollection.ToDictionary(
                KeyFunction<int>.Simple,
                IdentityFunction<int>.Instance,
                EqualityComparer<Guid>.Default
            );

            Assert.NotNull(actualDictionary);
            Assert.Empty(actualDictionary);
            Assert.Equal(expectedDictionary, actualDictionary);
        }

        [Fact]
        public void Call_ToReadOnlyList_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualList = emptyCollection.ToReadOnlyList();

            // Assert.
            var expectedList = emptyCollection.ToList();

            Assert.NotNull(actualList);
            Assert.Empty(actualList);
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void Call_ToReadOnlyCollection_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act.
            var actualCollection = emptyCollection.ToReadOnlyCollection();

            // Assert.
            IReadOnlyCollection<int> expectedCollection = emptyCollection.ToList();

            Assert.NotNull(actualCollection);
            Assert.Empty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForCollectionWithSomeItems(
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyDictionary_WithKeyElementSelector_ForCollectionWithSomeItems(
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForCollectionWithSomeItems(
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

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyList_ForCollectionWithSomeItems(int count)
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

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_ToReadOnlyCollection_ForCollectionWithSomeItems(int count)
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
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForCollectionWithRandomSize()
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
        public void Call_ToReadOnlyDictionary_WithKeySelectorAndComparer_ForCollectionWithRandomSize()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelector_ForCollectionWithRandomSize()
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
        public void Call_ToReadOnlyDictionary_WithKeyElementSelectorsAndComparer_ForCollectionWithRandomSize()
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
        public void Call_ToReadOnlyList_ForCollectionWithRandomSize()
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
        public void Call_ToReadOnlyCollection_ForCollectionWithRandomSize()
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(IDictionary<long, int>)}'.";
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
        }

        [Fact]
        public void ToReadOnlyDictionary_WithKeyElementSelector_ShouldReturnImmutableCollection()
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(IDictionary<long, int>)}'.";
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(IDictionary<long, int>)}'.";
                CustomAssert.Fail(message);
            }
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(IList<int>)}'.";
                CustomAssert.Fail(message);
            }
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
                    "returns collection with invalid type. Return type should be inherit from " +
                    $"'{nameof(ICollection<int>)}'.";
                CustomAssert.Fail(message);
            }
        }
    }
}
