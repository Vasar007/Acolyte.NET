using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;

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
            string actualValue = nullValue.ToSingleString(i => i.ToString());

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToSingleString_WithSelector_ForNullValues_ShouldFailDueToNullSelector()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => nullValue.ToSingleString(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSelector_ForNullValue_ShouldReturnEmptyCollectionMessage()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;
            const string expectedValue = "Collection is empty";

            // Act.
            string actualValue = nullValue.ToSingleString(expectedValue, i => i.ToString());

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
                emptyCollectionMessage: null, i => i.ToString()
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => nullValue.ToSingleString(emptyCollectionMessage: null, selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
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
                expectedValue, separator, i => i.ToString()
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
                emptyCollectionMessage: null, separator: null, i => i.ToString()
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => nullValue.ToSingleString(
                    emptyCollectionMessage: null, separator: null, selector: null
                )
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
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
            string actualValue = emptyCollection.ToSingleString(i => i.ToString());

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
            string actualValue = emptyCollection.ToSingleString(expectedValue, i => i.ToString());

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
                expectedValue, separator, i => i.ToString()
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
