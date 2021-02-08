using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Acolyte.Common;
using Acolyte.Tests;
using Acolyte.Tests.Collections;
using Acolyte.Tests.Creators;
using Acolyte.Tests.Functions;
using Acolyte.Tests.Objects;

namespace Acolyte.Collections.Tests.EnumerableExtensions
{
    public sealed class MinMaxWithSelectorTests
    {
        public MinMaxWithSelectorTests()
        {
        }

        #region MinMax For Int32

        [Fact]
        public void Call_MinMax_WithSelector_Int32_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Int32_ForNullSelector()
        {
            // Arrange.
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableInt32_ForNullValue()
        {
            // Arrange.
            const IEnumerable<int?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableInt32_ForNullSelector()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Int32_ForEmptyCollection_ShouldThrow()
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
        public void Call_MinMax_WithSelector_NullableInt32_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<int?> emptyCollection = Enumerable.Empty<int?>();
            (int? minValue, int? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_Int32_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_NullableInt32_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_Int32_ForCollectionWithRandomSize()
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
        public void Call_MinMax_WithSelector_NullableInt32_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_Int32_WithSelector_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new int[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int minValue, int maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleInt32),
                explosiveCollection.Max(MultiplyFunction.RedoubleInt32)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleInt32);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt32_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new int?[] { 1, 2, 3, 4 };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (int? minValue, int? maxValue) expectedValue =
             (
                 explosiveCollection.Min(MultiplyFunction.RedoubleNullableInt32),
                 explosiveCollection.Max(MultiplyFunction.RedoubleNullableInt32)
             );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableInt32);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Int64

        [Fact]
        public void Call_MinMax_WithSelector_Int64_ForNullValue()
        {
            // Arrange.
            const IEnumerable<long>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Int64_ForNullSelector()
        {
            // Arrange.
            IEnumerable<long> emptyCollection = Enumerable.Empty<long>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableInt64_ForNullValue()
        {
            // Arrange.
            const IEnumerable<long?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableInt64_ForNullSelector()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Int64_ForEmptyCollection_ShouldThrow()
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
        public void Call_MinMax_WithSelector_NullableInt64_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<long?> emptyCollection = Enumerable.Empty<long?>();
            (long? minValue, long? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_Int64_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_NullableInt64_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_Int64_ForCollectionWithRandomSize()
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
        public void Call_MinMax_WithSelector_NullableInt64_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_Int64_WithSelector_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new long[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long minValue, long maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleInt64),
                explosiveCollection.Max(MultiplyFunction.RedoubleInt64)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleInt64);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableInt64_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new long?[] { 1L, 2L, 3L, 4L };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (long? minValue, long? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableInt64),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableInt64)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableInt64);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Single

        [Fact]
        public void Call_MinMax_WithSelector_Single_ForNullValue()
        {
            // Arrange.
            const IEnumerable<float>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Single_ForNullSelector()
        {
            // Arrange.
            IEnumerable<float> emptyCollection = Enumerable.Empty<float>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableSingle_ForNullValue()
        {
            // Arrange.
            const IEnumerable<float?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableSingle_ForNullSelector()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Single_ForEmptyCollection_ShouldThrow()
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
        public void Call_MinMax_WithSelector_NullableSingle_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<float?> emptyCollection = Enumerable.Empty<float?>();
            (float? minValue, float? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_Single_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_NullableSingle_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_Single_ForCollectionWithRandomSize()
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
        public void Call_MinMax_WithSelector_NullableSingle_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_WithSelector_Single_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new float[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float minValue, float maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleSingle),
                explosiveCollection.Max(MultiplyFunction.RedoubleSingle)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleSingle);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableSingle_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new float?[] { 1.0F, 2.0F, 3.0F, 4.0F };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (float? minValue, float? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableSingle),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableSingle)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableSingle);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Double

        [Fact]
        public void Call_MinMax_WithSelector_Double_ForNullValue()
        {
            // Arrange.
            const IEnumerable<double>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Double_ForNullSelector()
        {
            // Arrange.
            IEnumerable<double> emptyCollection = Enumerable.Empty<double>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableDouble_ForNullValue()
        {
            // Arrange.
            const IEnumerable<double?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableDouble_ForNullSelector()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Double_ForEmptyCollection_ShouldThrow()
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
        public void Call_MinMax_WithSelector_NullableDouble_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<double?> emptyCollection = Enumerable.Empty<double?>();
            (double? minValue, double? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_Double_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_NullableDouble_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_Double_ForCollectionWithRandomSize()
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
        public void Call_MinMax_WithSelector_NullableDouble_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_WithSelector_Double_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new double[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double minValue, double maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleDouble),
                explosiveCollection.Max(MultiplyFunction.RedoubleDouble)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleDouble);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDouble_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new double?[] { 1.0D, 2.0D, 3.0D, 4.0D };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (double? minValue, double? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableDouble),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableDouble)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableDouble);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Decimal

        [Fact]
        public void Call_MinMax_WithSelector_Decimal_ForNullValue()
        {
            // Arrange.
            const IEnumerable<decimal>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Decimal_ForNullSelector()
        {
            // Arrange.
            IEnumerable<decimal> emptyCollection = Enumerable.Empty<decimal>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableDecimal_ForNullValue()
        {
            // Arrange.
            const IEnumerable<decimal?>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_NullableDecimal_ForNullSelector()
        {
            // Arrange.
            IEnumerable<decimal?> emptyCollection = Enumerable.Empty<decimal?>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_Decimal_ForEmptyCollection_ShouldThrow()
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
        public void Call_MinMax_WithSelector_NullableDecimal_ForEmptyCollection_ShouldReturnNull()
        {
            // Arrange.
            IEnumerable<decimal?> emptyCollection = Enumerable.Empty<decimal?>();
            (decimal? minValue, decimal? maxValue) expectedValue = (null, null);

            // Act.
            var actualValue = emptyCollection.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_Decimal_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_NullableDecimal_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_Decimal_ForCollectionWithRandomSize()
        {
            // Arrange.
            int count = TestDataCreator.GetRandomSmallCountNumber();
            IReadOnlyList<decimal> collectionWithRandomSize = TestDataCreator
                .CreateRandomDecimalList(count);

            // Act & Assert.
            if (collectionWithRandomSize.Count > 1)
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
        public void Call_MinMax_WithSelector_NullableDecimal_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_WithSelector_Decimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new decimal[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal minValue, decimal maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleDecimal),
                explosiveCollection.Max(MultiplyFunction.RedoubleDecimal)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleDecimal);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_NullableDecimal_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new decimal?[] { 1.0M, 2.0M, 3.0M, 4.0M };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (decimal? minValue, decimal? maxValue) expectedValue =
            (
                explosiveCollection.Min(MultiplyFunction.RedoubleNullableDecimal),
                explosiveCollection.Max(MultiplyFunction.RedoubleNullableDecimal)
            );

            // Act.
            var actualValue = explosiveCollection.MinMax(MultiplyFunction.RedoubleNullableDecimal);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region MinMax For Generic Types

        // Using user-defined struct and class to test generic overload.

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_ForNullValue()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source", () => nullValue.MinMax(selector: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_ForNullSelector()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector", () => emptyCollection.MinMax(selector: null)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForNullValue()
        {
            // Arrange.
            const IEnumerable<DummyClass>? nullValue = null;

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "source",
                () => nullValue.MinMax(IdentityFunction<DummyClass>.Instance, comparer: default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForNullSelector()
        {
            // Arrange.
            IEnumerable<DummyClass> emptyCollection = Enumerable.Empty<DummyClass>();

            // Act & Assert.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(
                "selector",
                () => emptyCollection.MinMax(selector: null, Comparer<DummyClass>.Default)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForNullComparer_ShouldUseDefaultComparer()
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

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldThrowForValueTypes()
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
        public void Call_MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
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
        public void Call_MinMax_WithSelector_GenericTypes_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
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
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldThrowForValueTypes()
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
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForNullableValueTypes()
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
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForEmptyCollection_ShouldReturnNullForReferenceTypes()
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

        [Theory]
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_GenericTypes_ForCollectionWithSomeItems(int count)
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
        [InlineData(TestHelper.OneCollectionSize)]
        [InlineData(TestHelper.TwoCollectionSize)]
        [InlineData(TestHelper.FiveCollectionSie)]
        [InlineData(TestHelper.TenCollectionSize)]
        [InlineData(TestHelper.HundredCollectionSize)]
        [InlineData(TestHelper.TenThousandCollectionSize)]
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithSomeItems(int count)
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

        [Fact]
        public void Call_MinMax_WithSelector_GenericTypes_ForCollectionWithRandomSize()
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
        public void Call_MinMax_WithSelector_GenericTypes_WithComparer_ForCollectionWithRandomSize()
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

        [Fact]
        public void MinMax_WithSelector_GenericTypes_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new string[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax(IdentityFunction<string?>.Instance);

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void MinMax_WithSelector_GenericTypes_WithComparer_ShouldLookWholeCollectionToFindValues()
        {
            // Arrange.
            var collection = new string[] { "1", "2", "3", "4" };
            var explosiveCollection = ExplosiveCollection.CreateNotExplosive(collection);
            (string? minValue, string? maxValue) expectedValue =
                (explosiveCollection.Min(), explosiveCollection.Max());

            // Act.
            var actualValue = explosiveCollection.MinMax(
                IdentityFunction<string?>.Instance, Comparer<string?>.Default
            );

            // Assert.
            Assert.Equal(expected: collection.Length, explosiveCollection.VisitedItemsNumber);
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion
    }
}
