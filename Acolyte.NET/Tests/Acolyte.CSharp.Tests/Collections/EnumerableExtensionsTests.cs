using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Tests;
using System;
using Acolyte.Common;

namespace Acolyte.Collections.Tests
{
    public sealed class EnumerableExtensionsTests
    {
        public EnumerableExtensionsTests()
        {
        }

        #region Tests for "Is Null Or Empty" section

        [Fact]
        public void Call_IsNullOrEmpty_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

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
        [InlineData(TestHelper.MaxCollectionSize)]
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

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            bool expectedResult = !collectionWithRandomSize.Any();
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
                // Convert all odd valies to null.
                .Select(value => (value & 1) == 0 ? value : (int?) null)
                .ToReadOnlyList();

            // Act.
            bool actualResult = collectionWithRandomSize.IsNullOrEmpty();

            // Assert.
            bool expectedResult = !collectionWithRandomSize.Any();
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "First Or Default" section

        [Fact]
        public void Call_FirstOrDefault_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.FirstOrDefault(default));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.FirstOrDefault(_ => default, default)
            );
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> nullValue = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => nullValue.FirstOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_FirstOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.FirstOrDefault(expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.FirstOrDefault(_ => default, expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_FirstOrDefault_ForCollectionWithSomeItems_ShouldReturnFirstItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.First();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(default);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnFirstItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.First();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(
                i => i.Equals(expectedResult), default
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithSomeItems.FirstOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(defaultResult);

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First()
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(
                i => i.Equals(i), defaultResult
            );

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.First(i => i.Equals(i))
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_FirstOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.FirstOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "Last Or Default" section

        [Fact]
        public void Call_LastOrDefault_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.LastOrDefault(default));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.LastOrDefault(_ => default, default)
            );
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> nullValue = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "predicate", () => nullValue.LastOrDefault(null, default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_LastOrDefault_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.LastOrDefault(expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = emptyCollection.LastOrDefault(_ => default, expectedResult);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_LastOrDefault_ForCollectionWithSomeItems_ShouldReturnLastItem(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.Last();

            // Act.
            int actualResult = collectionWithSomeItems.LastOrDefault(default);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnLastItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = collectionWithSomeItems.Last();

            // Act.
            int actualResult = collectionWithSomeItems.LastOrDefault(
                i => i.Equals(expectedResult), default
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItem(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithSomeItems.LastOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
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
            int actualResult = collectionWithRandomSize.LastOrDefault(defaultResult);

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last()
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.LastOrDefault(
                i => i.Equals(i), defaultResult
            );

            // Assert.
            int expectedResult = collectionWithRandomSize.Any()
                ? collectionWithRandomSize.Last(i => i.Equals(i))
                : defaultResult;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_LastOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualResult = collectionWithRandomSize.LastOrDefault(
                _ => false, expectedResult
            );

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        #endregion

        #region Tests for "Single Or Default" section

        // TODO: write tests.

        #endregion

        #region Tests for "Index Of" section

        [Fact]
        public void Call_IndexOf_ForNullValue()
        {
            // Arrange.
            IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>("source", () => nullValue.IndexOf(_ => default));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void Call_IndexOf_ForNullPredicate()
        {
            // Arrange.
            IEnumerable<int> nullValue = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>("predicate", () => nullValue.IndexOf(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_IndexOf_ForEmptyCollection()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            int expectedResult = Constants.NotFoundIndex;

            // Act.
            int actualResult = emptyCollection.IndexOf(_ => default);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_IndexOf_ForCollectionWithSomeItems_ShouldReturnIndexOfRandomlySelectedItem(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedResult) =
                 TestDataCreator.ChoiceWithIndex(collectionWithSomeItems);

            // Act.
            int actualResult = collectionWithSomeItems.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        [InlineData(TestHelper.MaxCollectionSize)]
        public void Call_IndexOf_ForCollectionWithSomeItems_ShouldReturnNotFoundIndex(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = Constants.NotFoundIndex;

            // Act.
            int actualResult = collectionWithSomeItems.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_IndexOf_ForCollectionWithRandomSize_ShouldReturnIndexOfRandomlySelectedItem()
        {
            // Arrange.
            // Count should be positive.
            int count = TestDataCreator.CreateRandomInt32(1, TestHelper.MaxCollectionSize);
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            (int randomItem, int expectedResult) =
                TestDataCreator.ChoiceWithIndex(collectionWithRandomSize);

            // Act.
            int actualResult = collectionWithRandomSize.IndexOf(i => i.Equals(randomItem));

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Call_IndexOf_ForCollectionWithRandomSize_ShouldReturnNotFoundIndex()
        {
            // Arrange.
            int count = TestDataCreator.CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize);
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedResult = Constants.NotFoundIndex;

            // Act.
            int actualResult = collectionWithRandomSize.IndexOf(_ => false);

            // Assert.
            Assert.Equal(expectedResult, actualResult);
        }

        // TODO: write tests for the second and third overloads of "IndexOf" method.

        #endregion

        #region Tests for "To Read-Only Collections" section

        // TODO: write tests.

        #endregion

        #region Tests for "Min/Max For Generic Types With Comparer" section

        // TODO: write tests.

        #endregion

        #region Tests for "MinMax Methods" section

        // TODO: write tests.

        #endregion

        #region Tests for "Min/Max By Key Methods" section

        // TODO: write tests.

        #endregion

        #region Tests for "MinMax By Key Methods" section

        // TODO: write tests.

        #endregion

        #region Tests for "Enmumerable To String" section

        // TODO: write tests.

        #endregion

        #region Tests for "For Each Methods" section

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
