﻿using System;
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
        #region Null Values

        [Fact]
        public void MinMaxBy_WithComparer_ForNullValue_ShouldFailForValueTypes()
        {
            // Arrange.
            const IEnumerable<DummyStruct>? nullValue = null;
            Func<DummyStruct, DummyStruct> discardKeySelector = DiscardFunction<DummyStruct>.Instance;
            var keyComparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullValue_ShouldFailForReferenceTypes()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;
            Func<DummyClass, DummyClass?> discardKeySelector = DiscardFunction<DummyClass>.Instance;
            var keyComparer = MockComparer<DummyClass?>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMaxBy(discardKeySelector, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullSelector_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            var keyComparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinMaxBy(keySelector: null!, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullSelector_ShouldFailForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            var keyComparer = MockComparer<DummyClass>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "keySelector",
                () => emptyCollection.MinMaxBy(keySelector: null!, keyComparer)
            );
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparerForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());
            Func<DummyStruct, DummyStruct> keySelector = IdentityFunction<DummyStruct>.Instance;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForNullComparer_ShouldUseDefaultComparerForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());
            Func<DummyClass, DummyClass> keySelector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            Func<DummyStruct, DummyStruct> keySelector = IdentityFunction<DummyStruct>.Instance;
            var keyComparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMaxBy(keySelector, keyComparer)
            );
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);
            Func<DummyStruct?, DummyStruct?> keySelector = IdentityFunction<DummyStruct?>.Instance;
            var keyComparer = MockComparer<DummyStruct?>.Default;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);
            Func<DummyClass, DummyClass> keySelector = IdentityFunction<DummyClass>.Instance;
            var keyComparer = MockComparer<DummyClass>.Default;

            // Act.
            var actualValue = emptyCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            keyComparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMinMaxForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct?> predefinedCollection = new DummyStruct?[]
            {
                 null, new DummyStruct(1), null, new DummyStruct(2), null, new DummyStruct(3)
            };
            // Workaround for nullable value type.
            Func<DummyStruct?, int> keySelector =
                item => InverseFunction.ForInt32(item?.Value ?? 2);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue =
                (new DummyStruct(-minValue), new DummyStruct(-maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, predefinedCollection);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForPredefinedCollection_ShouldReturnProperMinMaxForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass?> predefinedCollection =
                 new[] { null, new DummyClass(1), null, new DummyClass(2), null, new DummyClass(3) };
            // Workaround for nullable reference type.
            Func<DummyClass?, int> keySelector =
                item => InverseFunction.ForInt32(item?.Value ?? 2);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = predefinedCollection.Min(keySelector);
            int maxValue = predefinedCollection.Max(keySelector);
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = predefinedCollection.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, predefinedCollection);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMaxForValueTypes(
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
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_WithComparer_ForCollectionWithSomeItems_ShouldReturnProperMinMaxForReferenceTypes(
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
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithSomeItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItemForValueTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            (int minValue, int maxValue) expectedValue = (count, count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithTheSameItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void MinMaxBy_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItemForReferenceTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<int> collectionWithTheSameItems = Enumerable
                .Repeat(count, count)
                .ToList();
            (int minValue, int maxValue) expectedValue = (count, count);
            Func<int, int> keySelector = InverseFunction.ForInt32;
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithTheSameItems.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxForValueTypes()
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
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithRandomSize);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            Func<DummyClass, int> keySelector = item => InverseFunction.ForInt32(item.Value);
            // Min/max with selector returns transformed value. Need to transform it back.
            int minValue = collectionWithRandomSize.Min(keySelector);
            int maxValue = collectionWithRandomSize.Max(keySelector);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (new DummyClass(-minValue), new DummyClass(-maxValue));
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = collectionWithRandomSize.MinMaxBy(keySelector, keyComparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMaxBy_WithComparer_ShouldLookWholeCollectionToFindItemForValueTypes()
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
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector, keyComparer);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collection);
        }

        [Fact]
        public void MinMaxBy_WithComparer_ShouldLookWholeCollectionToFindItemForReferenceTypes()
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
            var keyComparer = MockComparer<int>.Default;

            // Act.
            var actualValue = explosive.MinMaxBy(keySelector, keyComparer);

            // Assert.
            CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(keyComparer, collection);
        }

        #endregion
    }
}
