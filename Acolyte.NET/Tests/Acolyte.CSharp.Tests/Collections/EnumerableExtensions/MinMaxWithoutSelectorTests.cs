#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Objects;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class MinMaxWithoutSelectorTests
    {
        public MinMaxWithoutSelectorTests()
        {
        }

        #region MinMax For Int32

        [Fact]
        public void MinMax_Int32_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_NullableInt32_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_Int32_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_NullableInt32_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int32_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            (int minValue, int maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableInt32_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<int?> predefinedCollection = new int?[] { null, 1, null, 2, null, 3 };
            (int? minValue, int? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

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
        public void MinMax_Int32_ForCollectionWithSomeItems_ShouldReturnMinMax(int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            (int minValue, int maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_NullableInt32_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableInt32List(count);
            (int? minValue, int? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int32_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (int minValue, int maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                var actualValue = collectionWithRandomSize.MinMax();

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_NullableInt32_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            (int? minValue, int? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int32_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new int[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int minValue, int maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableInt32_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new int?[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int? minValue, int? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Int64

        [Fact]
        public void MinMax_Int64_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<long>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_NullableInt64_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<long?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_Int64_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<long> emptyCollection = Enumerable.Empty<long>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_NullableInt64_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();
            (long? minValue, long? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int64_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            (long minValue, long maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableInt64_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList <long?> predefinedCollection =
                new long?[] { null, 1L, null, 2L, null, 3L };
            (long? minValue, long? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

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
        public void MinMax_Int64_ForCollectionWithSomeItems_ShouldReturnMinMax(int count)
        {
            // Arrange.
            IEnumerable<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            (long minValue, long maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_NullableInt64_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<long?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableInt64List(count);
            (long? minValue, long? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int64_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<long> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt64List(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (long minValue, long maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                var actualValue = collectionWithRandomSize.MinMax();

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_NullableInt64_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<long?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt64List(count)
                .ToNullable();
            (long? minValue, long? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Int64_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new long[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long minValue, long maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableInt64_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new long?[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long? minValue, long? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Single

        [Fact]
        public void MinMax_Single_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<float>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_NullableSingle_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<float?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_Single_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<float> emptyCollection = Enumerable.Empty<float>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_NullableSingle_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();
            (float? minValue, float? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Single_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<float> predefinedCollection = new[] { 1.1F, 2.2F, 3.5F };
            (float minValue, float maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableSingle_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<float?> predefinedCollection =
                new float?[] { null, 1.1F, null, 2.2F, null, 3.5F };
            (float? minValue, float? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

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
        public void MinMax_Single_ForCollectionWithSomeItems_ShouldReturnMinMax(int count)
        {
            // Arrange.
            IEnumerable<float> collectionWithSomeItems =
                TestDataCreator.CreateRandomSingleList(count);
            (float minValue, float maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_NullableSingle_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<float?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableSingleList(count);
            (float? minValue, float? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Single_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<float> collectionWithRandomSize = TestDataCreator
                .CreateRandomSingleList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (float minValue, float maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                var actualValue = collectionWithRandomSize.MinMax();

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_NullableSingle_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<float?> collectionWithRandomSize = TestDataCreator
                .CreateRandomSingleList(count)
                .ToNullable();
            (float? minValue, float? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Single_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new float[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float minValue, float maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableSingle_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new float?[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float? minValue, float? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Double

        [Fact]
        public void MinMax_Double_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<double>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_NullableDouble_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<double?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_Double_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<double> emptyCollection = Enumerable.Empty<double>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_NullableDouble_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();
            (double? minValue, double? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Double_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<double> predefinedCollection = new[] { 1.1D, 2.2D, 3.5D };
            (double minValue, double maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableDouble_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<double?> predefinedCollection =
                new double?[] { null, 1.1D, null, 2.2D, null, 3.5D };
            (double? minValue, double? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

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
        public void MinMax_Double_ForCollectionWithSomeItems_ShouldReturnMinMax(int count)
        {
            // Arrange.
            IEnumerable<double> collectionWithSomeItems =
                TestDataCreator.CreateRandomDoubleList(count);
            (double minValue, double maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_NullableDouble_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<double?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableDoubleList(count);
            (double? minValue, double? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Double_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<double> collectionWithRandomSize = TestDataCreator
                .CreateRandomDoubleList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (double minValue, double maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                var actualValue = collectionWithRandomSize.MinMax();

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_NullableDouble_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<double?> collectionWithRandomSize = TestDataCreator
                .CreateRandomDoubleList(count)
                .ToNullable();
            (double? minValue, double? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Double_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new double[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double minValue, double maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableDouble_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new double?[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double? minValue, double? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Decimal

        [Fact]
        public void MinMax_Decimal_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<decimal>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_NullableDecimal_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<decimal?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_Decimal_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<decimal> emptyCollection = Enumerable.Empty<decimal>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_NullableDecimal_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<decimal?> emptyCollection = Enumerable.Empty<decimal?>();
            (decimal? minValue, decimal? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Decimal_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<decimal> predefinedCollection = new[] { 1.1M, 2.2M, 3.5M };
            (decimal minValue, decimal maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableDecimal_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<decimal?> predefinedCollection =
                new decimal?[] { null, 1.1M, null, 2.2M, null, 3.5M };
            (decimal? minValue, decimal? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

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
        public void MinMax_Decimal_ForCollectionWithSomeItems_ShouldReturnMinMax(int count)
        {
            // Arrange.
            IEnumerable<decimal> collectionWithSomeItems =
                TestDataCreator.CreateRandomDecimalList(count);
            (decimal minValue, decimal maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_NullableDecimal_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<decimal?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableDecimalList(count);
            (decimal? minValue, decimal? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Decimal_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<decimal> collectionWithRandomSize = TestDataCreator
                .CreateRandomDecimalList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (decimal minValue, decimal maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                var actualValue = collectionWithRandomSize.MinMax();

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_NullableDecimal_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<decimal?> collectionWithRandomSize = TestDataCreator
                .CreateRandomDecimalList(count)
                .ToNullable();
            (decimal? minValue, decimal? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_Decimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new decimal[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal minValue, decimal maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_NullableDecimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new decimal?[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal? minValue, decimal? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Generic Types

        // Using user-defined struct and class to test generic overload.

        [Fact]
        public void MinMax_GenericTypes_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>("source", () => nullValue!.MinMax());
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(comparer: null)
            );
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax());
        }

        [Fact]
        public void MinMax_GenericTypes_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(Comparer<DummyStruct>.Default)
            );
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(Comparer<DummyStruct?>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(Comparer<DummyClass>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void MinMax_GenericTypes_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<string> predefinedCollection = new[] { "aaa", "bbb", "ccc" };
            (string minValue, string maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<string?> predefinedCollection =
                new string?[] { null, "aaa", null, "bbb", null, "ccc" };
            (string? minValue, string? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax(Comparer<string?>.Default);

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
        public void MinMax_GenericTypes_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<string> collectionWithSomeItems =
                TestDataCreator.CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax();

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
        public void MinMax_GenericTypes_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<string> collectionWithSomeItems =
                TestDataCreator.CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax();

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(Comparer<string>.Default);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new string[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax();

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_GenericTypes_WithComparer_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            string[] collection = new string[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            // Do not know why compiler decides that "explosiveCollection" should have "string?"
            // I think that's because of the nullable return type.
            var actualValue = explosiveCollection.MinMax(Comparer<string?>.Default);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
