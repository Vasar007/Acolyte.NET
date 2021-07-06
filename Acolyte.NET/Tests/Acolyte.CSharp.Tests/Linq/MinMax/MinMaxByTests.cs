using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Acolyte.Tests.Objects;
using Xunit;

namespace Acolyte.Tests.Linq.MinMax
{
    public sealed partial class MinMaxByTests
    {
        public MinMaxByTests()
        {
        }

        // Using user-defined struct and class to test generic overload.

        #region Null Values

        [Fact]
        public void MinMaxBy_ForNullValue_ShouldFailForValueTypes()
        {
            // Arrange.
            const IEnumerable<DummyStruct>? nullValue = null;
            Func<DummyStruct, DummyStruct> discardKeySelector = DiscardFunction<DummyStruct>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector)
            );
        }

        [Fact]
        public void MinMaxBy_ForNullValue_ShouldFailForReferenceTypes()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;
            Func<DummyClass, DummyClass?> discardKeySelector = DiscardFunction<DummyClass>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector)
            );
        }

        [Fact]
        public void MinMaxBy_ForNullSelector_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            const Func<DummyStruct, DummyStruct>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinMaxBy(keySelector!)
            );
        }

        [Fact]
        public void MinMaxBy_ForNullSelector_ShouldFailForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            const Func<DummyClass, DummyClass>? keySelector = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector", () => emptyCollection.MinMaxBy(keySelector!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMaxBy_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            Func<DummyStruct, DummyStruct> keySelector = IdentityFunction<DummyStruct>.Instance;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMaxBy(keySelector)
            );
        }

        [Fact]
        public void MinMaxBy_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);
            Func<DummyStruct?, DummyStruct?> keySelector = IdentityFunction<DummyStruct?>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);
            Func<DummyClass, DummyClass> keySelector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMaxBy_ForPredefinedCollection_ShouldReturnProperMinMaxForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> predefinedCollection = DummyStruct.DefaultList;
            Func<DummyStruct, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (new DummyStruct(-minValue), new DummyStruct(-maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_ForPredefinedCollection_ShouldReturnProperMinMaxForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> predefinedCollection = DummyClass.DefaultList;
            Func<DummyClass, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_ForCollectionWithSomeItems_ShouldReturnProperMinMaxForValueTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyStructList(count);
            Func<DummyStruct, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int maxValue = collectionWithSomeItems.Max(keySelector);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (new DummyStruct(-minValue), new DummyStruct(-maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_ForCollectionWithSomeItems_ShouldReturnProperMinMaxForReferenceTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyClass> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyClassList(count);
            Func<DummyClass, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithSomeItems.Min(keySelector);
            int maxValue = collectionWithSomeItems.Max(keySelector);
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_ForCollectionWithTheSameItems_ShouldReturnThatItemForValueTypes(
            int count)
        {
            // Arrange.
            var expectedItem = DummyStruct.Item;
            IReadOnlyList<DummyStruct> collectionWithTheSameItems = Enumerable
                .Repeat(expectedItem, count)
                .ToList();
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (expectedItem, expectedItem);
            Func<DummyStruct, int> keySelector = item => InverseFunction.ForInt32(item.Value);

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_ForCollectionWithTheSameItems_ShouldReturnThatItemForReferenceTypes(
           int count)
        {
            // Arrange.
            var expectedItem = DummyClass.Item;
            IReadOnlyList<DummyClass> collectionWithTheSameItems = Enumerable
                .Repeat(expectedItem, count)
                .ToList();
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (expectedItem, expectedItem);
            Func<DummyClass, int> keySelector = item => InverseFunction.ForInt32(item.Value);

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMaxBy_ForCollectionWithRandomSize_ShouldReturnMinMaxForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            Func<DummyStruct, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int maxValue = collectionWithRandomSize.Max(keySelector);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (new DummyStruct(-minValue), new DummyStruct(-maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_ForCollectionWithRandomSize_ShouldReturnMinMaxForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            Func<DummyClass, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int maxValue = collectionWithRandomSize.Max(keySelector);
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMaxBy_ShouldLookWholeCollectionToFindItemForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collection = DummyStruct.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<DummyStruct, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collection.Min(keySelector);
            int maxValue = collection.Max(keySelector);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (new DummyStruct(-minValue), new DummyStruct(-maxValue));

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_ShouldLookWholeCollectionToFindItemForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> collection = DummyClass.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            Func<DummyClass?, int> keySelector = item => InverseFunction.ForInt32(item!.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collection.Min(keySelector);
            int maxValue = collection.Max(keySelector);
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMinMax<TComparerType, TItems>(
            MockComparer<TComparerType> comparer, IReadOnlyList<TItems> collection)
        {
            if (collection.Count > 0)
            {
                comparer.VerifyCompareCalls(times: (collection.Count - 1) * 2);
            }
            else
            {
                comparer.VerifyNoCalls();
            }
        }

        #endregion
    }
}
