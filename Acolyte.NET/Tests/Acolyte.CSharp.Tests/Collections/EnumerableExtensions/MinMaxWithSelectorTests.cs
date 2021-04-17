#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Objects;
using Xunit;

namespace Acolyte.Tests.Collections.EnumerableExtensions
{
    public sealed class MinMaxWithSelectorTests
    {
        public MinMaxWithSelectorTests()
        {
        }

        #region MinMax For Int32

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_Int32_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<int>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_Int32_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<int?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<int?>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_Int32_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(MultiplyFunction.RedoubleInt32)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_Int32_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<int> predefinedCollection = new[] { 1, 2, 3 };
            (int minValue, int maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleInt32),
                predefinedCollection.Max(MultiplyFunction.RedoubleInt32)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<int?> predefinedCollection = new int?[] { null, 1, null, 2, null, 3 };
            (int? minValue, int? maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleNullableInt32),
                predefinedCollection.Max(MultiplyFunction.RedoubleNullableInt32)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_Int32_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int> collectionWithSomeItems = TestDataCreator.CreateRandomInt32List(count);
            (int minValue, int maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleInt32),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleInt32)
            );

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleInt32);

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
        public void MinMax_WithSelector_NullableInt32_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<int?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableInt32List(count);
            (int? minValue, int? maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleNullableInt32),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleNullableInt32)
            );

            // Act.
            var actualValue =
                collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_Int32_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<int> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (int minValue, int maxValue) expectedValue =
                (
                    collectionWithRandomSize.Min(MultiplyFunction.RedoubleInt32),
                    collectionWithRandomSize.Max(MultiplyFunction.RedoubleInt32)
                );

                var actualValue = collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleInt32);

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(),
                    () => collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleInt32)
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<int?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt32List(count)
                .ToNullable();
            (int? minValue, int? maxValue) expectedValue =
            (
                collectionWithRandomSize.Min(MultiplyFunction.RedoubleNullableInt32),
                collectionWithRandomSize.Max(MultiplyFunction.RedoubleNullableInt32)
            );

            // Act.
            var actualValue =
                collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_Int32_WithSelector_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<int> collection = new[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int minValue, int maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleInt32),
                explosiveCollection.Max(MultiplyFunction.RedoubleInt32)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleInt32);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<int?> collection = new int?[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int? minValue, int? maxValue) expectedValue =
             (
                 explosiveCollection.Min(MultiplyFunction.RedoubleNullableInt32),
                 explosiveCollection.Max(MultiplyFunction.RedoubleNullableInt32)
             );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Int64

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_Int64_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<long>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<long>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_Int64_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<long> emptyCollection = Enumerable.Empty<long>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<long?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<long?>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_Int64_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<long> emptyCollection = Enumerable.Empty<long>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(MultiplyFunction.RedoubleInt64)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();
            (long? minValue, long? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_Int64_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<long> predefinedCollection = new[] { 1L, 2L, 3L };
            (long minValue, long maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleInt64),
                predefinedCollection.Max(MultiplyFunction.RedoubleInt64)
            ); ;

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<long?> predefinedCollection =
                new long?[] { null, 1L, null, 2L, null, 3L };
            (long? minValue, long? maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleNullableInt64),
                predefinedCollection.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_Int64_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<long> collectionWithSomeItems =
                TestDataCreator.CreateRandomInt64List(count);
            (long minValue, long maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleInt64),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleInt64)
            );

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleInt64);

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
        public void MinMax_WithSelector_NullableInt64_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<long?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableInt64List(count);
            (long? minValue, long? maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleNullableInt64),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue =
                collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_Int64_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<long> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt64List(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (long minValue, long maxValue) expectedValue =
                (
                    collectionWithRandomSize.Min(MultiplyFunction.RedoubleInt64),
                    collectionWithRandomSize.Max(MultiplyFunction.RedoubleInt64)
                );

                var actualValue = collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleInt64);

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(),
                    () => collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleInt64)
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<long?> collectionWithRandomSize = TestDataCreator
                .CreateRandomInt64List(count)
                .ToNullable();
            (long? minValue, long? maxValue) expectedValue =
            (
                collectionWithRandomSize.Min(MultiplyFunction.RedoubleNullableInt64),
                collectionWithRandomSize.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue =
                collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_Int64_WithSelector_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<long> collection = new[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long minValue, long maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleInt64),
                explosiveCollection.Max(MultiplyFunction.RedoubleInt64)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleInt64);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<long?> collection = new long?[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long? minValue, long? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableInt64),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Single

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_Single_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<float>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<float>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_Single_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<float> emptyCollection = Enumerable.Empty<float>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<float?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<float?>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_Single_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<float> emptyCollection = Enumerable.Empty<float>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(MultiplyFunction.RedoubleSingle)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();
            (float? minValue, float? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_Single_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<float> predefinedCollection = new[] { 1.1F, 2.2F, 3.5F };
            (float minValue, float maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleSingle),
                predefinedCollection.Max(MultiplyFunction.RedoubleSingle)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<float?> predefinedCollection =
                new float?[] { null, 1.1F, null, 2.2F, null, 3.5F };
            (float? minValue, float? maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleNullableSingle),
                predefinedCollection.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_Single_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<float> collectionWithSomeItems =
                TestDataCreator.CreateRandomSingleList(count);
            (float minValue, float maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleSingle),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleSingle)
            );

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleSingle);

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
        public void MinMax_WithSelector_NullableSingle_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<float?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableSingleList(count);
            (float? minValue, float? maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleNullableSingle),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue =
                collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_Single_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<float> collectionWithRandomSize = TestDataCreator
                .CreateRandomSingleList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (float minValue, float maxValue) expectedValue =
                (
                    collectionWithRandomSize.Min(MultiplyFunction.RedoubleSingle),
                    collectionWithRandomSize.Max(MultiplyFunction.RedoubleSingle)
                );

                var actualValue = collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleSingle);

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(),
                    () => collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleSingle)
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<float?> collectionWithRandomSize = TestDataCreator
                .CreateRandomSingleList(count)
                .ToNullable();
            (float? minValue, float? maxValue) expectedValue =
            (
                collectionWithRandomSize.Min(MultiplyFunction.RedoubleNullableSingle),
                collectionWithRandomSize.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue =
                collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_WithSelector_Single_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<float> collection = new[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float minValue, float maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleSingle),
                explosiveCollection.Max(MultiplyFunction.RedoubleSingle)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleSingle);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<float?> collection = new float?[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float? minValue, float? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableSingle),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Double

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_Double_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<double>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<double>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_Double_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<double> emptyCollection = Enumerable.Empty<double>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<double?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<double?>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_Double_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<double> emptyCollection = Enumerable.Empty<double>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(MultiplyFunction.RedoubleDouble)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();
            (double? minValue, double? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_Double_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<double> predefinedCollection = new[] { 1.1D, 2.2D, 3.5D };
            (double minValue, double maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleDouble),
                predefinedCollection.Max(MultiplyFunction.RedoubleDouble)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<double?> predefinedCollection =
                new double?[] { null, 1.1D, null, 2.2D, null, 3.5D };
            (double? minValue, double? maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleNullableDouble),
                predefinedCollection.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_Double_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<double> collectionWithSomeItems =
                TestDataCreator.CreateRandomDoubleList(count);
            (double minValue, double maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleDouble),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleDouble)
            );

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleDouble);

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
        public void MinMax_WithSelector_NullableDouble_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<double?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableDoubleList(count);
            (double? minValue, double? maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleNullableDouble),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue =
                collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_Double_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<double> collectionWithRandomSize = TestDataCreator
                .CreateRandomDoubleList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (double minValue, double maxValue) expectedValue =
                (
                    collectionWithRandomSize.Min(MultiplyFunction.RedoubleDouble),
                    collectionWithRandomSize.Max(MultiplyFunction.RedoubleDouble)
                );

                var actualValue = collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleDouble);

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(),
                    () => collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleDouble)
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<double?> collectionWithRandomSize = TestDataCreator
                .CreateRandomDoubleList(count)
                .ToNullable();
            (double? minValue, double? maxValue) expectedValue =
            (
                collectionWithRandomSize.Min(MultiplyFunction.RedoubleNullableDouble),
                collectionWithRandomSize.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue =
                collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_WithSelector_Double_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<double> collection = new[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double minValue, double maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleDouble),
                explosiveCollection.Max(MultiplyFunction.RedoubleDouble)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleDouble);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<double?> collection = new double?[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double? minValue, double? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableDouble),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Decimal

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_Decimal_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<decimal>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<decimal>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_Decimal_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<decimal> emptyCollection = Enumerable.Empty<decimal>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<decimal?>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<decimal?>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<decimal?> emptyCollection = Enumerable.Empty<decimal?>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_Decimal_ForEmptyCollection_ShouldFail()
        {
            // Arrange.
            IEnumerable<decimal> emptyCollection = Enumerable.Empty<decimal>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(MultiplyFunction.RedoubleDecimal)
            );
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<decimal?> emptyCollection = Enumerable.Empty<decimal?>();
            (decimal? minValue, decimal? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_Decimal_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<decimal> predefinedCollection = new[] { 1.1M, 2.2M, 3.5M };
            (decimal minValue, decimal maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleDecimal),
                predefinedCollection.Max(MultiplyFunction.RedoubleDecimal)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<decimal?> predefinedCollection =
                new decimal?[] { null, 1.1M, null, 2.2M, null, 3.5M };
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                predefinedCollection.Min(MultiplyFunction.RedoubleNullableDecimal),
                predefinedCollection.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue = predefinedCollection.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_Decimal_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<decimal> collectionWithSomeItems =
                TestDataCreator.CreateRandomDecimalList(count);
            (decimal minValue, decimal maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleDecimal),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleDecimal)
            );

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleDecimal);

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
        public void MinMax_WithSelector_NullableDecimal_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<decimal?> collectionWithSomeItems =
                TestDataCreator.CreateRandomNullableDecimalList(count);
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                collectionWithSomeItems.Min(MultiplyFunction.RedoubleNullableDecimal),
                collectionWithSomeItems.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue =
                collectionWithSomeItems.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_Decimal_ForCollectionWithRandomSize_ShouldReturnMinMaxOrFailIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<decimal> collectionWithRandomSize = TestDataCreator
                .CreateRandomDecimalList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 0)
            {
                (decimal minValue, decimal maxValue) expectedValue =
                (
                    collectionWithRandomSize.Min(MultiplyFunction.RedoubleDecimal),
                    collectionWithRandomSize.Max(MultiplyFunction.RedoubleDecimal)
                );

                var actualValue = collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleDecimal);

                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.Throws(
                    Error.NoElements().GetType(),
                    () => collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleDecimal)
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<decimal?> collectionWithRandomSize = TestDataCreator
                .CreateRandomDecimalList(count)
                .ToNullable();
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                collectionWithRandomSize.Min(MultiplyFunction.RedoubleNullableDecimal),
                collectionWithRandomSize.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue =
                collectionWithRandomSize.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_WithSelector_Decimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<decimal> collection = new[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal minValue, decimal maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleDecimal),
                explosiveCollection.Max(MultiplyFunction.RedoubleDecimal)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleDecimal);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<decimal?> collection = new decimal?[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableDecimal),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Generic Types

        // Using user-defined struct and class to test generic overload.

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(DiscardFunction<DummyClass>.Func)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullValue_ShouldFail()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue!.MinMax(DiscardFunction<DummyClass>.Func, comparer: default)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullSelector_ShouldFail()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => emptyCollection.MinMax(selector: null!, Comparer<DummyClass>.Default)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(
                IdentityFunction<string?>.Instance, comparer: null
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Empty Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(IdentityFunction<DummyStruct>.Instance)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(IdentityFunction<DummyStruct?>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(IdentityFunction<DummyClass>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(),
                () => emptyCollection.MinMax(
                    IdentityFunction<DummyStruct>.Instance, Comparer<DummyStruct>.Default
                )
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(
                IdentityFunction<DummyStruct?>.Instance, Comparer<DummyStruct?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(
                IdentityFunction<DummyClass>.Instance, Comparer<DummyClass>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<string> predefinedCollection = new[] { "aaa", "bbb", "ccc" };
            (string minValue, string maxValue) expectedValue =
               (predefinedCollection[0], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax(IdentityFunction<string>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForPredefinedCollection_ShouldReturnMinMax()
        {
            // Arrange.
            IReadOnlyList<string?> predefinedCollection =
                new string?[] { null, "aaa", null, "bbb", null, "ccc" };
            (string? minValue, string? maxValue) expectedValue =
                  (predefinedCollection[1], predefinedCollection[^1]);

            // Act.
            var actualValue = predefinedCollection.MinMax(
                IdentityFunction<string?>.Instance, Comparer<string?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Some Values

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<string> collectionWithSomeItems =
                TestDataCreator.CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(IdentityFunction<string>.Instance);

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
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinMax(
            int count)
        {
            // Arrange.
            IEnumerable<string> collectionWithSomeItems =
                TestDataCreator.CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(
                IdentityFunction<string>.Instance, Comparer<string>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(IdentityFunction<string>.Instance);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItems()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IEnumerable<string> collectionWithRandomSize = TestDataCreator
                .CreateRandomStringList(count);
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(
                IdentityFunction<string?>.Instance, Comparer<string?>.Default
            );

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<string> collection = new[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax(IdentityFunction<string?>.Instance);

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<string> collection = new[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            // Do not know why compiler decides that "explosiveCollection" should have "string?"
            // I think that's because of the nullable return type.
            var actualValue = explosiveCollection.MinMax(
                IdentityFunction<string?>.Instance, Comparer<string?>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Count, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion
    }
}
