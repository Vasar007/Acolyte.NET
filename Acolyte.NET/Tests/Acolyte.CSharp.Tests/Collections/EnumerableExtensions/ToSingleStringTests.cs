#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Tests.Creators;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class ToSingleStringTests
    {
        public ToSingleStringTests()
        {
        }

        [Fact]
        public void ToSingleString_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const string expectedValue = "Collection is empty";

            // Act.
            string actualValue = nullValue.ToSingleString(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ForNullValueAndMessage_ShouldReturnDefaultEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString(emptyCollectionMessage: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const string expectedValue = "Collection is empty";
            string separator = Strings.DefaultItemSeparator;

            // Act.
            string actualValue = nullValue.ToSingleString(expectedValue, separator);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForNullValueAndMessage_ShouldReturnDefaultEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;
            string separator = Strings.DefaultItemSeparator;

            // Act.
            string actualValue = nullValue.ToSingleString(emptyCollectionMessage: null, separator);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForNullValueAndMessageAndSeparator_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString(
                emptyCollectionMessage: null, separator: null
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString(ToStingFunction<int>.Simple);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForNullValues_ShouldFailDueToNullSelector()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => nullValue.ToSingleString(selector: null!)
            );
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const string expectedValue = "Collection is empty";

            // Act.
            string actualValue = nullValue.ToSingleString(
                expectedValue, ToStingFunction<int>.Simple
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForNullValueAndMessage_ShouldReturnDefaultEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString(
                emptyCollectionMessage: null, ToStingFunction<int>.Simple
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForNullValues_ShouldFailDueToNullSelector()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => nullValue.ToSingleString(emptyCollectionMessage: null, selector: null!)
            );
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const string expectedValue = "Collection is empty"; ;
            string separator = Strings.DefaultItemSeparator;

            // Act.
            string actualValue = nullValue.ToSingleString(
                expectedValue, separator, ToStingFunction<int>.Simple
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForNullValueAndMessageAndSeparator_ShouldReturnDefaultEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = nullValue.ToSingleString(
                emptyCollectionMessage: null, separator: null, ToStingFunction<int>.Simple
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForNullValues_ShouldFailDueToNullSelector()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => nullValue.ToSingleString(
                    emptyCollectionMessage: null, separator: null, selector: null!
                )
            );
        }

        [Fact]
        public void ToSingleString_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = emptyCollection.ToSingleString();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = emptyCollection.ToSingleString(expectedValue);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;
            string separator = Strings.DefaultItemSeparator;

            // Act.
            string actualValue = emptyCollection.ToSingleString(expectedValue, separator);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = emptyCollection.ToSingleString(ToStingFunction<int>.Simple);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = emptyCollection.ToSingleString(expectedValue, ToStingFunction<int>.Simple);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForEmptyCollection_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
            string expectedValue = Strings.DefaultEmptyCollectionMessage;
            string separator = Strings.DefaultItemSeparator;

            // Act.
            string actualValue = emptyCollection.ToSingleString(
                expectedValue, separator, ToStingFunction<int>.Simple
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));

            // Act.
            string actualValue = predefinedCollection.ToSingleString();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = predefinedCollection.ToSingleString(emptyCollectionMessage);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            const string separator = " ";
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = predefinedCollection.ToSingleString(
                emptyCollectionMessage, separator
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));

            // Act.
            string actualValue = predefinedCollection.ToSingleString(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            string separator = Strings.DefaultItemSeparator;
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = predefinedCollection.ToSingleString(
                emptyCollectionMessage, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForPredefinedCollection_ShouldReturnPropperMessageWithItems()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            const string separator = " ";
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, predefinedCollection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = predefinedCollection.ToSingleString(
                emptyCollectionMessage, separator, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_WithEmptyCollectionMessage_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString(emptyCollectionMessage);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            const string separator = " ";
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString(
                emptyCollectionMessage, separator
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_WithSelector_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            string separator = Strings.DefaultItemSeparator;
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString(
                emptyCollectionMessage, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForCollectionWithSomeItems_ShouldReturnPropperMessageWithItems(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            const string separator = " ";
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, collectionWithSomeItems.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithSomeItems.ToSingleString(
                emptyCollectionMessage, separator, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString(emptyCollectionMessage);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            const string separator = " ";
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString(
                emptyCollectionMessage, separator
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            string separator = Strings.DefaultItemSeparator;
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString(
                emptyCollectionMessage, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ForCollectionWithRandomSize_ShouldReturnEmptyMessageOrPropperMessageWithItemsDependOnCollection()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomCountNumber();
            IEnumerable<int> collectionWithRandomSize =
                TestDataCreator.CreateRandomInt32List(count);
            const string separator = " ";
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(
                separator, collectionWithRandomSize.Select(selector)
            );
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = collectionWithRandomSize.ToSingleString(
                emptyCollectionMessage, separator, selector
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collection.Select(selector));

            // Act.
            string actualValue = explosiveCollection.ToSingleString();

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosiveCollection.ToSingleString(emptyCollectionMessage);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparator_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            const string separator = " ";
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosiveCollection.ToSingleString(
                emptyCollectionMessage, separator
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collection.Select(selector));

            // Act.
            string actualValue = explosiveCollection.ToSingleString(selector);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            string separator = Strings.DefaultItemSeparator;
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosiveCollection.ToSingleString(
                emptyCollectionMessage, selector
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            const string separator = " ";
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosiveCollection.ToSingleString(
                emptyCollectionMessage, separator, selector
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
