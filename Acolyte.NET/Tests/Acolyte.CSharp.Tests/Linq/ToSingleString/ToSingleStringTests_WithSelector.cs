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

        #endregion

        #region Empty Values

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

        #endregion

        #region Predefined Values

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

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
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

        #endregion

        #region Random Values

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

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void ToSingleString_WithSelector_ShouldLookWholeCollectionToConstrtuctMessage()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<int, string> selector = ToStingFunction<int>.Simple;
            string separator = Strings.DefaultItemSeparator;
            string expectedValue = string.Join(separator, collection.Select(selector));

            // Act.
            string actualValue = explosive.ToSingleString(selector);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
