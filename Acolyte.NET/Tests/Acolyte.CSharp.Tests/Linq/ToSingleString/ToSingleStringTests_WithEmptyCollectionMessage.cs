using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Xunit;

namespace Acolyte.Tests.Linq.ToSingleString
{
    public sealed partial class ToSingleStringTests
    {
        #region Null Values

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

        #endregion

        #region Empty Values

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

        #endregion

        #region Predefined Values

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

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
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

        #endregion

        #region Random Values

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

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessage_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.WithSingleQuotes;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosive.ToSingleString(emptyCollectionMessage);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
