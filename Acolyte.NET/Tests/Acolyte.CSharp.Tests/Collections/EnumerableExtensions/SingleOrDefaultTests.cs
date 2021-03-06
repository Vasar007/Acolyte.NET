﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class SingleOrDefaultTests
    {
        public SingleOrDefaultTests()
        {
        }

        [Fact]
        public void SingleOrDefault_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.SingleOrDefault(default(int))
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForNullValue_ShouldFail()
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
        public void SingleOrDefault_WithPredicate_ForNullPredicate_ShouldFail()
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
        public void SingleOrDefault_ForEmptyCollection_ShouldReturnDefaultItem()
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
        public void SingleOrDefault_WithPredicate_ForEmptyCollection_ShouldReturnDefaultItem()
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
        public void SingleOrDefault_ForCollectionWithSingleItem_ShouldReturnFirstItem()
        {
            // Arrange.
            IEnumerable<int> collectionWithSingleItem =
                TestDataCreator.CreateRandomInt32List(TestHelper.OneCollectionSize);
            int expectedValue = collectionWithSingleItem.Single();

            // Act.
            int actualValue = collectionWithSingleItem.SingleOrDefault(default(int));

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForCollectionWithSingleItem_ShouldReturnFirstItem()
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

        [Fact]
        public void SingleOrDefault_ForPredefinedCollection_ShouldFail()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => predefinedCollection.SingleOrDefault(default(int))
            );
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForPredefinedCollection_ShouldFailIfFoundTwoAndMoreItmsAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 1 };
            int expectedValue = predefinedCollection[0];

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => predefinedCollection.SingleOrDefault(i => i.Equals(expectedValue), default)
            );
        }

        [Fact]
        public void SingleOrDefault_WithPredicate_ForPredefinedCollection_ShouldReturnSingleItemAccordingToPredicate()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            int expectedValue = predefinedCollection[1];

            // Act.
            int actualValue = predefinedCollection.SingleOrDefault(
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
        public void SingleOrDefault_ForCollectionWithSomeItems_ShouldFail(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => collectionWithSomeItems.SingleOrDefault(default(int))
            );
        }

        [Theory]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldFailIfFoundTwoAndMoreItmsAccordingToPredicate(
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
        public void SingleOrDefault_WithPredicate_ForCollectionWithSomeItems_ShouldReturnDefaultItemIfFoundNoItemsAccordingToPredicate(
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
        public void SingleOrDefault_ForCollectionWithRandomSize_ShouldReturnSingleOrDefaultItemOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
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
        public void SingleOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnSingleOrDefaultItemOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IReadOnlyList<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int defaultResult = TestDataCreator.CreateRandomInt32();
            Func<int, bool> predicate = i => NumberParityFunction.IsEven(i);

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
        public void SingleOrDefault_WithPredicate_ForCollectionWithRandomSize_ShouldReturnDefaultItem()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            int expectedValue = TestDataCreator.CreateRandomInt32();

            // Act.
            int actualValue = collectionWithRandomSize.SingleOrDefault(_ => false, expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void SingleOrDefault_ShouldLookOnlyAtFirstAndSecondItemsFromCollectionBeforeFail()
        {
            // Arrange.
            var collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.Create(
                collection, explosiveIndex: Constants.FirstIndex + 2
            );

            // Act & Assert.
            Assert.Throws(
                Error.MoreThanOneElement().GetType(),
                () => explosiveCollection.SingleOrDefault(default(int))
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
        public void SingleOrDefault_WithPredicate_ShouldFailAsSoonAsSecondAppropriateItemWasFound()
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
    }
}
