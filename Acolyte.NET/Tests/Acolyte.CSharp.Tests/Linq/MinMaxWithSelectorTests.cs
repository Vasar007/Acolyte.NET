using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Functions;
using Acolyte.Linq;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Mocked;
using Acolyte.Tests.Objects;
using Xunit;

namespace Acolyte.Tests.Linq
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (int minValue, int maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleInt32),
                explosive.Max(MultiplyFunction.RedoubleInt32)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleInt32);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<int?> collection = new int?[] { 1, 2, 3, 4 };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (int? minValue, int? maxValue) expectedValue =
             (
                 explosive.Min(MultiplyFunction.RedoubleNullableInt32),
                 explosive.Max(MultiplyFunction.RedoubleNullableInt32)
             );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (long minValue, long maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleInt64),
                explosive.Max(MultiplyFunction.RedoubleInt64)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleInt64);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<long?> collection = new long?[] { 1L, 2L, 3L, 4L };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (long? minValue, long? maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleNullableInt64),
                explosive.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (float minValue, float maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleSingle),
                explosive.Max(MultiplyFunction.RedoubleSingle)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleSingle);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<float?> collection = new float?[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (float? minValue, float? maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleNullableSingle),
                explosive.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (double minValue, double maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleDouble),
                explosive.Max(MultiplyFunction.RedoubleDouble)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleDouble);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<double?> collection = new double?[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (double? minValue, double? maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleNullableDouble),
                explosive.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
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
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (decimal minValue, decimal maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleDecimal),
                explosive.Max(MultiplyFunction.RedoubleDecimal)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleDecimal);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            IReadOnlyList<decimal?> collection = new decimal?[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                explosive.Min(MultiplyFunction.RedoubleNullableDecimal),
                explosive.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue = explosive.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #endregion

        #region MinMax For Generic Types

        // Using user-defined struct and class to test generic overload.

        #region Null Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullValue_ShouldFailForValueTypes()
        {
            // Arrange.
            const IEnumerable<DummyStruct>? nullValue = null;
            Func<DummyStruct, DummyStruct> discardKeySelector = DiscardFunction<DummyStruct>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(discardKeySelector)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullValue_ShouldFailForReferenceTypes()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;
            Func<DummyClass, DummyClass?> discardKeySelector = DiscardFunction<DummyClass>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(discardKeySelector)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullSelector_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForNullSelector_ShouldFailForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!)
            );
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullValue_ShouldFailForValueTypes()
        {
            // Arrange.
            const IEnumerable<DummyStruct>? nullValue = null;
            var comparer = MockComparer<DummyStruct>.Default;
            Func<DummyStruct, DummyStruct> discardKeySelector = DiscardFunction<DummyStruct>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(discardKeySelector, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullValue_ShouldFailForReferenceTypes()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;
            var comparer = MockComparer<DummyClass?>.Default;
            Func<DummyClass, DummyClass?> discardKeySelector = DiscardFunction<DummyClass>.Func;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue!.MinMax(discardKeySelector, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullSelector_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            var comparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullSelector_ShouldFailForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            var comparer = MockComparer<DummyClass>.Default;

            // Act & Assert.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null!, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullComparer_ShouldUseDefaultComparerForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(selector, comparer: null);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForNullComparer_ShouldUseDefaultComparerForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomPositiveSmallCountNumber();
            IReadOnlyList<string> collectionWithRandomSize =
                TestDataCreator.CreateRandomStringList(count);
            Func<string?, string?> selector = IdentityFunction<string?>.Instance;
            (string? minValue, string? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(selector, comparer: null);

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
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;

            // Act & Assert.
            Assert.Throws(Error.NoElements().GetType(), () => emptyCollection.MinMax(selector));
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);
            Func<DummyStruct?, DummyStruct?> selector = IdentityFunction<DummyStruct?>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMax(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = emptyCollection.MinMax(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldFailForValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct> emptyCollection = Enumerable.Empty<DummyStruct>();
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;
            var comparer = MockComparer<DummyStruct>.Default;

            // Act & Assert.
            Assert.Throws(
                Error.NoElements().GetType(), () => emptyCollection.MinMax(selector, comparer)
            );
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
        {
            // Arrange.
            IEnumerable<DummyStruct?> emptyCollection = Enumerable.Empty<DummyStruct?>();
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue = (null, null);
            Func<DummyStruct?, DummyStruct?> selector = IdentityFunction<DummyStruct?>.Instance;
            var comparer = MockComparer<DummyStruct?>.Default;

            // Act.
            var actualValue = emptyCollection.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            comparer.VerifyNoCalls();
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();
            (DummyClass? minValue, DummyClass? maxValue) expectedValue = (null, null);
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;
            var comparer = MockComparer<DummyClass>.Default;

            // Act.
            var actualValue = emptyCollection.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForPredefinedCollection_ShouldReturnMinMaxForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> predefinedCollection = DummyStruct.DefaultList;
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (predefinedCollection[0], predefinedCollection[^1]);
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;

            // Act.
            var actualValue = predefinedCollection.MinMax(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForPredefinedCollection_ShouldReturnMinMaxxForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> predefinedCollection = DummyClass.DefaultList;
            (DummyClass minValue, DummyClass maxValue) expectedValue =
                (predefinedCollection[0], predefinedCollection[^1]);
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = predefinedCollection.MinMax(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForPredefinedCollection_ShouldReturnMinMaxForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct?> predefinedCollection = new DummyStruct?[]
            {
                 null, new DummyStruct(1), null, new DummyStruct(2), null, new DummyStruct(3)
            };
            (DummyStruct? minValue, DummyStruct? maxValue) expectedValue =
                (predefinedCollection[1], predefinedCollection[^1]);
            Func<DummyStruct?, DummyStruct?> selector = IdentityFunction<DummyStruct?>.Instance;
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            var actualValue = predefinedCollection.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            // "MinMax" method skips null value and do not call "Compare" method.
            // Than we skip fist not null value.
            comparer.VerifyCompareCalls(times: 2 * 2);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForPredefinedCollection_ShouldReturnMinMaxForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass?> predefinedCollection =
                   new[] { null, new DummyClass(1), null, new DummyClass(2), null, new DummyClass(3) };
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (predefinedCollection[1], predefinedCollection[^1]);
            Func<DummyClass?, DummyClass?> selector = IdentityFunction<DummyClass?>.Instance;
            var comparer = MockComparer.SetupDefaultFor(predefinedCollection);

            // Act.
            var actualValue = predefinedCollection.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            // "MinMax" method skips null value and do not call "Compare" method.
            // Than we skip fist not null value.
            comparer.VerifyCompareCalls(times: 2 * 2);
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
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithSomeItems_ShouldReturnMinMaxForValueTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyStructList(count);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(selector);

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
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithSomeItems_ShouldReturnMinMaxForReferenceTypes(
           int count)
        {
            // Arrange.
            IReadOnlyList<DummyClass> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyClassList(count);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(selector);

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
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinMaxForValueTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyStructList(count);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;
            var comparer = MockComparer<DummyStruct>.Default;

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(comparer, collectionWithSomeItems);
        }

        [Theory]
        [InlineData(TestConstants._1)]
        [InlineData(TestConstants._2)]
        [InlineData(TestConstants._5)]
        [InlineData(TestConstants._10)]
        [InlineData(TestConstants._100)]
        [InlineData(TestConstants._10_000)]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithSomeItems_ShouldReturnMinMaxForReferenceTypes(
            int count)
        {
            // Arrange.
            IReadOnlyList<DummyClass> collectionWithSomeItems =
                TestDataCreator.CreateRandomDummyClassList(count);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (collectionWithSomeItems.Min(), collectionWithSomeItems.Max());
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;
            var comparer = MockComparer<DummyClass>.Default;

            // Act.
            var actualValue = collectionWithSomeItems.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(comparer, collectionWithSomeItems);
        }

        #endregion

        #region Random Values

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItemsForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;

            if (collectionWithRandomSize.Count > 0)
            {
                (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                // Act.
                var actualValue = collectionWithRandomSize.MinMax(selector);

                // Assert.
                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                // Act & Assert.
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax()
                );
            }
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItemsForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(selector);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItemsForValueTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<DummyStruct> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyStructList(count);
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;
            var comparer = MockComparer<DummyStruct>.Default;

            // Act.
            if (collectionWithRandomSize.Count > 0)
            {
                (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                    (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());

                // Act.
                var actualValue = collectionWithRandomSize.MinMax(selector, comparer);

                // Assert.
                Assert.Equal(expectedValue, actualValue);
                VerifyCompareCallsForMinMax(comparer, collectionWithRandomSize);
            }
            else
            {
                // Act & Assert.
                Assert.Throws(
                    Error.NoElements().GetType(), () => collectionWithRandomSize.MinMax(comparer)
                );
                comparer.VerifyNoCalls();
            }
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithRandomSize_ShouldReturnMinMaxOrNullIfNoItemsForReferenceTypes()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<DummyClass> collectionWithRandomSize =
                TestDataCreator.CreateRandomDummyClassList(count);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (collectionWithRandomSize.Min(), collectionWithRandomSize.Max());
            Func<DummyClass, DummyClass> selector = IdentityFunction<DummyClass>.Instance;
            var comparer = MockComparer<DummyClass>.Default;

            // Act.
            var actualValue = collectionWithRandomSize.MinMax(selector, comparer);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(comparer, collectionWithRandomSize);
        }

        #endregion

        #region Extended Logical Coverage

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ShouldLookWholeCollectionToFindValuesForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collection = DummyStruct.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue =
                (explosive.Min(), explosive.Max());
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;

            // Act.
            var actualValue = explosive.MinMax(selector);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ShouldLookWholeCollectionToFindValuesForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> collection = DummyClass.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (explosive.Min(), explosive.Max());
            Func<DummyClass?, DummyClass?> selector = IdentityFunction<DummyClass?>.Instance;

            // Act.
            var actualValue = explosive.MinMax(selector);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ShouldLookWholeCollectionToFindValuesForValueTypes()
        {
            // Arrange.
            IReadOnlyList<DummyStruct> collection = DummyStruct.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (DummyStruct minValue, DummyStruct maxValue) expectedValue = (explosive.Min(), explosive.Max());
            Func<DummyStruct, DummyStruct> selector = IdentityFunction<DummyStruct>.Instance;
            var comparer = MockComparer<DummyStruct>.Default;

            // Act.
            var actualValue = explosive.MinMax(selector, comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(comparer, collection);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ShouldLookWholeCollectionToFindValuesForReferenceTypes()
        {
            // Arrange.
            IReadOnlyList<DummyClass> collection = DummyClass.DefaultList;
            var explosive = ExplosiveEnumerable.CreateNotExplosive(collection);
            (DummyClass? minValue, DummyClass? maxValue) expectedValue =
                (explosive.Min(), explosive.Max());
            Func<DummyClass?, DummyClass?> selector = IdentityFunction<DummyClass?>.Instance;
            var comparer = MockComparer<DummyClass?>.Default;

            // Act.
            // Do not know why compiler decides that "explosive" should have "string?"
            // I think that's because of the nullable return type.
            var actualValue = explosive.MinMax(selector, comparer);

            // Assert.
            CustomAssert.True(explosive.VerifyThriceEnumerateWholeCollection(collection));
            Assert.Equal(expectedValue, actualValue);
            VerifyCompareCallsForMinMax(comparer, collection);
        }

        #endregion

        #region Private Methods

        private static void VerifyCompareCallsForMinMax<T>(MockComparer<T> comparer,
            IReadOnlyList<T> collection)
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

        #endregion
    }
}
