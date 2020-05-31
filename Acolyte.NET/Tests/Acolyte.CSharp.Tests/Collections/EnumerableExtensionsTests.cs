using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Common;
using Acolyte.Tests;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Objects;

namespace Acolyte.Collections.Tests
{
    public sealed class EnumerableExtensionsTests
    {
        public EnumerableExtensionsTests()
        {
        }

        // TODO: write mock equality comprarer to test overloads with it.

        #region Tests for "Is Null Or Empty" section

        [Fact]
        public void Call_IsNullOrEmpty_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            bool expectedResult = !collectionWithRandomSize.Any();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_IsNullOrEmpty_ForCollectionWithRandomNumberAndNullValues()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int?> collectionWithRandomSize = Enumerable
                .Range(1, count)
                .Select(i => TestDataCreator.CreateRandomNonNegativeInt32())
                // Convert all even valies to null.
                .Select(value => TestDataCreator.IsEven(value) ? value : (int?) null)
                .ToReadOnlyList();
            bool expectedResult = !collectionWithRandomSize.Any();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsNullOrEmpty_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            const int count = 2;
            var explosiveCollection = ExplosiveCollection.Create(
                TestDataCreator.CreateRandomInt32List(count),
                explosiveIndex: Constants.FirstIndex + 1
            );
            bool expectedResult = !explosiveCollection.Any();

            // Act.
            bool actualResult = explosiveCollection.IsNullOrEmpty();

            // Assert.
            Assert.Equal(expected: 1, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "First Or Default" section

        [Fact]
        public void Call_FirstOrDefault_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.FirstOrDefault(default));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.FirstOrDefault(default, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.FirstOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_FirstOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.FirstOrDefault(_ => default, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_FirstOrDefault_ForCollectionWithSomeItems_ShouldReturnFirstItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.First();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnFirstItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.First();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_FirstOrDefault_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First()
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = TestDataCreator.IsEven;
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First(predicate)
                : defaultResult;

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(predicate, defaultResult);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_ShouldLookOnlyAtFirstItemFromCollection()
        {
            // Arrange.
            const int count = 2;
            var explosiveCollection = ExplosiveCollection.Create(
                TestDataCreator.CreateRandomInt32List(count),
                explosiveIndex: Constants.FirstIndex + 1
            );
            int expectedValue = explosiveCollection.First();

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(default);

            // Assert.
            Assert.Equal(expected: 1, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void FirstOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act.
            int actualValue = explosiveCollection.FirstOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Tests for "Last Or Default" section

        [Fact]
        public void Call_LastOrDefault_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.LastOrDefault(default));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.LastOrDefault(default, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.LastOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_LastOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.LastOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.LastOrDefault(_ => default, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_LastOrDefault_ForCollectionWithSomeItems_ShouldReturnLastItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Last();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnLastItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Last();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_LastOrDefault_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last()
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = TestDataCreator.IsEven;

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(predicate, defaultResult);

            // Assert.
            int expectedValue = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last(predicate)
                : defaultResult;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItemAfterItFoundSomething()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act.
            int actualValue = explosiveCollection.LastOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LastOrDefault_WithPredicate_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act.
            int actualValue = explosiveCollection.LastOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Tests for "Single Or Default" section

        [Fact]
        public void Call_SingleOrDefault_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.SingleOrDefault(default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.SingleOrDefault(default, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => emptyCollection.SingleOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_SingleOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.SingleOrDefault(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = emptyCollection.SingleOrDefault(_ => default, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_SingleOrDefault_ForCollectionWithSingleItem_ShouldReturnFirstItem()
        {
            // Arrange.
            IEnumerable<int> collectionWithSingleItem =
                TestDataCreator.CreateRandomInt32List(TestHelper.OneCollectionSize);
            int expectedValue = collectionWithSingleItem.Single();

            // Act.
            int actualValue = collectionWithSingleItem.SingleOrDefault(default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForCollectionWithSingleItem_ShouldReturnFirstItem()
        {
            // Arrange.
            IEnumerable<int> collectionWithSingleItem =
                TestDataCreator.CreateRandomInt32List(TestHelper.OneCollectionSize);
            int expectedValue = collectionWithSingleItem.Single();

            // Act.
            int actualValue = collectionWithSingleItem.SingleOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_SingleOrDefault_ForCollectionWithSomeItems_ShouldThrow(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => collectionWithSomeItems.SingleOrDefault(default)
            );
        }

        [Theory]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldThrow(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => collectionWithSomeItems.SingleOrDefault(_ => true, default)
            );
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithSomeItems.SingleOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_SingleOrDefault_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act & Assert.
            if (collectionWithRandomSize.Count > 1)
            {
                Assert.Throws(
                     Error.MoreThanOneElement().GetType(),
                     () => collectionWithRandomSize.SingleOrDefault(defaultResult)
                 );
            }
            else
            {
                int actualValue = collectionWithRandomSize.SingleOrDefault(defaultResult);

                int expectedValue = collectionWithRandomSize.Any()
                     ? collectionWithRandomSize.Single()
                     : defaultResult;

                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = TestDataCreator.IsEven;

            // Act & Assert.
            int foundValuesCount = collectionWithRandomSize.Count(predicate);
            if (foundValuesCount > 1)
            {
                Assert.Throws(
                     Error.MoreThanOneElement().GetType(),
                     () => collectionWithRandomSize.SingleOrDefault(predicate, defaultResult)
                 );
            }
            else
            {
                int actualValue = collectionWithRandomSize.SingleOrDefault(predicate, defaultResult);

                // Collection cannot be empty if we found one value.
                int expectedValue = foundValuesCount == 1
                    ? collectionWithRandomSize.Single(predicate)
                    : defaultResult;

                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void Call_SingleOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.SingleOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SingleOrDefault_ShouldLookOnlyAtFirstAndSecondItemsFromCollectionBeforeThrow()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int expectedValue = explosiveCollection.First();

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => explosiveCollection.SingleOrDefault(default)
            );

            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ShouldLookWholeCollectionToEnsureAppropriateItemIsSingle()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act.
            int actualValue = explosiveCollection.SingleOrDefault(
                i => i.Equals(expectedValue), default
            );

            // Assert.
            Assert.Equal(collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ShouldThrowAsSoonAsSecondAppropriateItemWasFound()
        {
            // Arrange.
            // Do not use random because we should find exactly two equal items.
            var collection = new[] { 1, 2, 3, 2, 5 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Skip(1).First();

            // Act & Assert.
            Assert.Throws(
             Error.MoreThanOneElement().GetType(),
             () => explosiveCollection.SingleOrDefault(i => i.Equals(expectedValue), default)
         );

            Assert.Equal(expected: 4, explosiveCollection.VisitedItemsNumber);
        }

        #endregion

        #region Tests for "Index Of" section

        [Fact]
        public void Call_IndexOf_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.IndexOf(_ => default));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("predicate", () => emptyCollection.IndexOf(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_Item_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.IndexOf(default(int)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_ItemWithComparer_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.IndexOf(default, EqualityComparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_ItemWithComparer_ForNullComparer()
        {
            // Arrange.
            IEnumerable<int> nullValue = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "comparer", () => nullValue.IndexOf(default, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(_ => default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_Item_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(default(int));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_ItemWithComparer_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = emptyCollection.IndexOf(default, EqualityComparer<int>.Default);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(
                randomItem, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_Item_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf((int?) null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_IndexOf_ItemWithComparer_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems = TestDataCreator
                 .CreateRandomInt32List(count)
                 .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithSomeItems.IndexOf(
                null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            // Count should be positive.
            int count = TestDataCreator.CreateRandomInt32(1, TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            // Count should be positive.
            int count = TestDataCreator.CreateRandomInt32(1, TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(randomItem);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            // Count should be positive.
            int count = TestDataCreator.CreateRandomInt32(1, TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedIndex) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(
                randomItem, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_Item_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
               .CreateRandomInt32List(count)
               .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf((int?) null);

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void Call_IndexOf_ItemWithComparer_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            int expectedIndex = Constants.NotFoundIndex;

            // Act.
            int actualIndex = collectionWithRandomSize.IndexOf(
                null, EqualityComparer<int?>.Default
            );

            // Assert.
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int expectedValue = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(i => i.Equals(expectedValue));

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value);

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_Item_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value: 0);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldStopAfterFoundItemFromCollection()
        {
            // Arrange.
            // Do not use random because we should find exactly second item.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );
            int value = explosiveCollection.Skip(1).First();
            const int expectedIndex = 1;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(
                value, EqualityComparer<int>.Default
            );

            // Assert.
            Assert.Equal(expected: 2, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IndexOf_ItemWithComparer_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = Constants.NotFoundIndex;

            // Act.
            int actualIndex = explosiveCollection.IndexOf(value: 0, EqualityComparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualIndex);
        }

        #endregion

        #region Tests for "To Read-Only Collections" section

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
            Assert.Throws<ArgumentNullException>("source", () => nullValue.ToReadOnlyList());
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
            var actualCollection = emptyCollection.ToReadOnlyList();

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
            var actualCollection = collectionWithSomeItems.ToReadOnlyList();

            // Assert.
            Assert.NotNull(actualCollection);
            Assert.NotEmpty(actualCollection);
            Assert.Equal(expectedCollection, actualCollection);
        }

        [Fact]
        public void Call_ToReadOnlyDictionary_WithKeySelector_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
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
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            IReadOnlyCollection<int> expectedCollection = collectionWithRandomSize.ToList();

            // Act.
            var actualCollection = collectionWithRandomSize.ToReadOnlyList();

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

        #region Tests for "Min/Max For Generic Types With Comparer" section

        [Fact]
        public void Call_Min_WithComparer_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.Min(Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_Min_WithComparer_ForNullComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("comparer", () => emptyCollection.Min(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_Max_WithComparer_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.Max(Comparer<int>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_Max_WithComparer_ForNullValueComparer()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("comparer", () => emptyCollection.Max(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_Min_WithComparer_ForEmptyCollection_ShouldThrowForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.Max(Comparer<int>.Default)
            );
        }

        [Fact]
        public void Call_Min_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.Min(Comparer<int?>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_Min_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.Min(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_Max_WithComparer_ForEmptyCollection_ShouldThrowForValueTypes()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.Max(Comparer<int>.Default)
            );
        }

        [Fact]
        public void Call_Max_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            int? expectedValue = null;

            // Act.
            int? actualValue = emptyCollection.Min(Comparer<int?>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_Max_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            const string? expectedValue = null;

            // Act.
            string? actualValue = emptyCollection.Max(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_Min_WithCompare_ForCollectionWithSomeItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Min();

            // Act.
            int actualValue = collectionWithSomeItems.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_Max_WithCompare_ForCollectionWithSomeItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithSomeItems.Max();

            // Act.
            int actualValue = collectionWithSomeItems.Max(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_Min_WithCompare_ForCollectionWithTheSameItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_Max_WithCompare_ForCollectionWithTheSameItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithTheSameItems = Enumerable
                .Range(1, count)
                .Select(_ => count);
            int expectedValue = count;

            // Act.
            int actualValue = collectionWithTheSameItems.Max(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_Min_WithCompare_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Min();

            // Act.
            int actualValue = collectionWithRandomSize.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_Max_WithCompare_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = collectionWithRandomSize.Max();

            // Act.
            int actualValue = collectionWithRandomSize.Max(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithCompare_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Min();

            // Act.
            int actualValue = explosiveCollection.Min(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Max_WithCompare_ShouldLookWholeCollectionToFindItem()
        {
            // Arrange.
            var collection = new[] { 4, 3, 2, 1 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            int expectedValue = explosiveCollection.Max();

            // Act.
            int actualValue = explosiveCollection.Max(Comparer<int>.Default);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Tests for "MinMax" section

        #region MinMax Overloads Without Selector

        #region MinMax For Int32

        [Fact]
        public void Call_MinMax_Int32_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_NullableInt32_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_Int32_ForEmptyCollection_ShouldThrow()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax()
            );
        }

        [Fact]
        public void Call_MinMax_NullableInt32_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Int64

        [Fact]
        public void Call_MinMax_Int64_ForNullValue()
        {
            // Arrange.
            const IEnumerable<long>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_NullableInt64_ForNullValue()
        {
            // Arrange.
            const IEnumerable<long?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_Int64_ForEmptyCollection_ShouldThrow()
        {
            // Arrange.
            IEnumerable<long> emptyCollection = Enumerable.Empty<long>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax()
            );
        }

        [Fact]
        public void Call_MinMax_NullableInt64_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();
            (long? minValue, long? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Single

        [Fact]
        public void Call_MinMax_Single_ForNullValue()
        {
            // Arrange.
            const IEnumerable<float>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_NullableSingle_ForNullValue()
        {
            // Arrange.
            const IEnumerable<float?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_Single_ForEmptyCollection_ShouldThrow()
        {
            // Arrange.
            IEnumerable<float> emptyCollection = Enumerable.Empty<float>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax()
            );
        }

        [Fact]
        public void Call_MinMax_NullableSingle_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();
            (float? minValue, float? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Double

        [Fact]
        public void Call_MinMax_Double_ForNullValue()
        {
            // Arrange.
            const IEnumerable<double>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_NullableDouble_ForNullValue()
        {
            // Arrange.
            const IEnumerable<double?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_Double_ForEmptyCollection_ShouldThrow()
        {
            // Arrange.
            IEnumerable<double> emptyCollection = Enumerable.Empty<double>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax()
            );
        }

        [Fact]
        public void Call_MinMax_NullableDouble_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();
            (double? minValue, double? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Generic Types

        [Fact]
        public void Call_MinMax_GenericTypes_ForNullValue()
        {
            // Arrange.
            const IEnumerable<string>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax()
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_GenericTypes_WithComparer_ForNullValue()
        {
            // Arrange.
            const IEnumerable<string>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(Comparer<string>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_GenericTypes_WithComparer_ForNullComparer()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "comparer", () => emptyCollection.MinMax(comparer: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_GenericTypes_ForEmptyCollection_ShouldThrowForValueTypes()
        {
            // Arrange.
            // Using user-defined struct to test generic overload.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax()
            );
        }

        [Fact]
        public void Call_MinMax_GenericTypes_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            // Using user-defined struct to test generic overload.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MinMax_GenericTypes_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldThrowForValueTypes()
        {
            // Arrange.
            // Using user-defined struct to test generic overload.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(Comparer<DummyStruct>.Default)
            );
        }

        [Fact]
        public void Call_MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            // Using user-defined struct to test generic overload.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(Comparer<DummyStruct?>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Call_MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();
            (string? minValue, string? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #endregion

        #region Tests for "Min/Max By Key" section

        // TODO: write tests.

        #endregion

        #region Tests for "MinMax By Key" section

        // TODO: write tests.

        #endregion

        #region Tests for "To Single String" section

        // TODO: write tests.

        #endregion

        #region Tests for "For Each" section

        // TODO: write tests.

        #endregion

        #region Tests for "Distinct By" section

        // TODO: write tests.

        #endregion

        #region Tests for "Slicing" section

        // TODO: write tests.

        #endregion
    }
}
