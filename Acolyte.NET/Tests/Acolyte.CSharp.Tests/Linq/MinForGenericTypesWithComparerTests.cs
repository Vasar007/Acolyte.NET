using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Acolyte.Tests.Objects;
using Xunit;

namespace Acolyte.Tests.Linq
{
    public sealed class MinForGenericTypesWithComparerTests
    {
        public MinForGenericTypesWithComparerTests()
        {
        }

        // Using user-defined struct and class to test generic overload.

        #region Null Values

        [Fact]
        public void Min_WithComparer_ForNullValue_ShouldFailForValueTypes()
        {
            // Arrange.
            const IEnumerable<DummyStruct>? nullValue = null;
            var comparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.Min(comparer));
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForNullValue_ShouldFailForReferenceTypes()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;
            var comparer = MockComparer<DummyClass>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.Min(comparer));
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForNullComparer_ShouldUseDefaultComparerForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            DummyStruct expectedValue = collectionWithRandomSize.Min();

            // Act.
            DummyStruct actualValue = collectionWithRandomSize.Min(comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Min_WithComparer_ForNullComparer_ShouldUseDefaultComparerForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            DummyClass? expectedValue = collectionWithRandomSize.Min();

            // Act.
            DummyClass? actualValue = collectionWithRandomSize.Min(comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            var comparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.Min(comparer));
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            DummyStruct? expectedValue = null;
            var comparer = MockComparer<DummyStruct?>.Default;

            // Act.
            DummyStruct? actualValue = emptyCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void Min_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            const DummyClass? expectedValue = null;
            var comparer = MockComparer<DummyClass>.Default;

            // Act.
            DummyClass? actualValue = emptyCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            comparer.VerifyNoCalls();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMinForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> predefinedCollection = DummyStruct.DefaultList;
            DummyStruct expectedValue = predefinedCollection[0];
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            DummyStruct actualValue = predefinedCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, predefinedCollection);
        }

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMinForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> predefinedCollection = DummyClass.DefaultList;
            DummyClass expectedValue = predefinedCollection[0];
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            DummyClass? actualValue = predefinedCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, predefinedCollection);
        }

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMinMinForNullableValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct?> predefinedCollection = new DummyStruct?[]
            {
                 null, new DummyStruct(1), null, new DummyStruct(2), null, new DummyStruct(3)
            };
            DummyStruct? expectedValue = predefinedCollection[1];
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            DummyStruct? actualValue = predefinedCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            // "Min" method skips null value and do not call "Compare" method.
            // Than we skip fist not null value.
            comparer.VerifyCompareCalls(times: 2);
        }

        [Fact]
        public void Min_WithComparer_ForPredefinedCollection_ShouldReturnMinMinForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass?> predefinedCollection =
                new[] { null, new DummyClass(1), null, new DummyClass(2), null, new DummyClass(3) };
            DummyClass? expectedValue = predefinedCollection[1];
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            DummyClass? actualValue = predefinedCollection.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            // "Min" method skips null value and do not call "Compare" method.
            // Than we skip fist not null value.
            comparer.VerifyCompareCalls(times: 2);
        }

        #endregion

        #region Some Values

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void Min_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinForValueTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyStructList(count);
            DummyStruct expectedValue = collectionWithSomeItems.Min();
            var comparer = MockComparer.SetupDefaultFor(collectionWithSomeItems);

            // Act.
            DummyStruct actualValue = collectionWithSomeItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void Min_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinForReferenceTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyClass> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyClassList(count);
            DummyClass? expectedValue = collectionWithSomeItems.Min();
            var comparer = MockComparer.SetupDefaultFor(collectionWithSomeItems);

            // Act.
            DummyClass? actualValue = collectionWithSomeItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithSomeItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void Min_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItemForValueTypes(
            int count)
        {
            // Arrange.
            var expectedValue = DummyStruct.Item;
            IReadOnlyList<DummyStruct> collectionWithTheSameItems = Enumerable
                .Repeat(expectedValue, count)
                .ToList();
            var comparer = MockComparer.SetupDefaultFor(collectionWithTheSameItems);

            // Act.
            DummyStruct actualValue = collectionWithTheSameItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithTheSameItems);
        }

        [Theory]
        [ClassData(typeof(PositiveTestConstants))]
        public void Min_WithComparer_ForCollectionWithTheSameItems_ShouldReturnThatItemForReferenceTypes(
            int count)
        {
            // Arrange.
            var expectedValue = DummyClass.Item;
            IReadOnlyList<DummyClass> collectionWithTheSameItems = Enumerable
                .Repeat(expectedValue, count)
                .ToList();
            var comparer = MockComparer.SetupDefaultFor(collectionWithTheSameItems);

            // Act.
            DummyClass? actualValue = collectionWithTheSameItems.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithTheSameItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void Min_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            var comparer = MockComparer<DummyStruct>.Default;

            if (collectionWithRandomSize.Count > 0)
            {
                DummyStruct expectedValue = collectionWithRandomSize.Min();

                // Act.
                DummyStruct actualValue = collectionWithRandomSize.Min(comparer);

                // Assert.
                Assert.Equal(expectedValue, actualValue);
                VerifyCompareCallsForMin(comparer, collectionWithRandomSize);
            }
            else
            {
                // Act & Assert.
                Assert.Throws(Error.NoElements().GetType(), () => collectionWithRandomSize.Min());
            }
        }

        [Fact]
        public void Min_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            DummyClass? expectedValue = collectionWithRandomSize.Min();
            var comparer = MockComparer<DummyClass>.Default;

            // Act.
            DummyClass? actualValue = collectionWithRandomSize.Min(comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void Min_WithComparer_ShouldLookWholeCollectionToFindItemForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collection = DummyStruct.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            DummyStruct expectedValue = explosive.Min();
            var comparer = MockComparer.SetupDefaultFor(collection);

            // Act.
            DummyStruct actualValue = explosive.Min(comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collection);
        }

        [Fact]
        public void Min_WithComparer_ShouldLookWholeCollectionToFindItemForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> collection = DummyClass.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            DummyClass? expectedValue = explosive.Min();
            var comparer = MockComparer.SetupDefaultFor<DummyClass?>(collection);

            // Act.
            DummyClass? actualValue = explosive.Min(comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyTwiceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMin(comparer, collection);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMin<T>(MockComparer<T> comparer,
            IReadOnlyList<T> collection)
        {
            if (collection.Count > 0)
            {
                comparer.VerifyCompareCalls(times: collection.Count - 1);
            }
            else
            {
                comparer.VerifyNoCalls();
            }
        }

        #endregion
    }
}
