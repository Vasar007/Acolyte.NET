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

        #endregion

        #region Empty Values

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

        #endregion

        #region Predefined Values

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

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
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

        #endregion

        #region Random Values

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

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToSingleString_WithEmptyCollectionMessageAndSeparatorAndSelector_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            const string separator = " ";
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string expectedValue = string.Join(separator, collection.Select(selector));
            string emptyCollectionMessage = Strings.DefaultEmptyCollectionMessage;

            // Act.
            string actualValue = explosive.ToSingleString(
                emptyCollectionMessage, separator, selector
            );

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
