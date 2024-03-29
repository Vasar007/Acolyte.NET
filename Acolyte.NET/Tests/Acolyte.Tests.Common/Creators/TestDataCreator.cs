﻿using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Data.Randomness;
using Acolyte.Functions;
using Acolyte.Numeric;
using Acolyte.Tests.Objects;

namespace Acolyte.Tests.Creators
{
    // TODO: move some methods to Acolyte.CSharp assembly because it can be useful.
    public static class TestDataCreator
    {
        private static ThreadSafeRandom RandomInstance { get; } = new(Seed: 42);

        #region Boundaries

        private static int GetUpperBound(int potentionUpperBound)
        {
            return potentionUpperBound == int.MaxValue
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }

        private static long GetUpperBound(long potentionUpperBound)
        {
            return potentionUpperBound == long.MaxValue
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }

        private static float GetUpperBound(float potentionUpperBound)
        {
            // Do not use "operator ==" to check floating-points on equality!
            return potentionUpperBound.IsEqual(float.MaxValue)
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }

        private static double GetUpperBound(double potentionUpperBound)
        {
            // Do not use "operator ==" to check floating-points on equality!
            return potentionUpperBound.IsEqual(double.MaxValue)
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }

        private static decimal GetUpperBound(decimal potentionUpperBound)
        {
            // Do not use "operator ==" to check floating-points on equality!
            return potentionUpperBound.IsEqual(decimal.MaxValue)
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }

        #endregion

        #region Choice Item

        public static (TSource item, int index) ChoiceWithIndex<TSource>(
            IReadOnlyList<TSource> source, Random? random = null)
        {
            source.ThrowIfNullOrEmpty(nameof(source));

            random ??= RandomInstance;

            int randomItemIndex = random.Next(source.Count);
            return (source[randomItemIndex], randomItemIndex);
        }

        public static TSource Choice<TSource>(IReadOnlyList<TSource> source,
            Random? random = null)
        {
            random ??= RandomInstance;

            return ChoiceWithIndex(source, random).item;
        }

        #endregion

        #region Create Generic List

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<int, Random, TItem> valueFactory, Random? random = null)
        {
            random ??= RandomInstance;

            // "Enumerable.Range" method and "List" constructor throw exception when "count" < 0.
            var values = Enumerable
                .Range(0, count)
                .Select(i => valueFactory(i, random));

            var result = new List<TItem>(capacity: count);
            result.AddRange(values);
            return result.AsReadOnly();
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<Random, TItem> valueFactory, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(rand),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<int, TItem> valueFactory, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(i),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count, Func<TItem> valueFactory,
            Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count, Random? random = null)
            where TItem : new()
        {
            return CreateList(
                count: count,
                valueFactory: () => new TItem(),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<int, Random, TItem> valueFactory,
            Random? random = null)
        {
            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<Random, TItem> valueFactory,
            Random? random = null)
        {
            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<int, TItem> valueFactory,
            Random? random = null)
        {
            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<TItem> valueFactory,
            Random? random = null)
        {
            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Random? random = null)
            where TItem : new()
        {
            int count = GetRandomCountNumber();
            return CreateList<TItem>(count, random);
        }

        #endregion

        #region Create String

        public static string CreateRandomString(int length, Random? random = null)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length), length, "Length must be positive."
                );
            }

            random ??= RandomInstance;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(
                Enumerable
                    .Repeat(chars, length)
                    .Select(str => str[random.Next(str.Length)])
                    .ToArray()
            );
        }

        public static string CreateRandomString(Random? random = null)
        {
            random ??= RandomInstance;

            int length = GetRandomPositiveSmallCountNumber(random);

            return CreateRandomString(length, random);
        }

        public static string? CreateRandomNullableString(Random? random = null)
        {
            random ??= RandomInstance;

            int length = GetRandomPositiveSmallCountNumber(random);

            var result = CreateRandomString(length, random);
            return NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(result);
        }

        #endregion

        #region Create Int32

        public static int CreateRandomNonNegativeInt32(Random? random = null)
        {
            random ??= RandomInstance;

            // "Random.Next" method returns non-negative values.
            return random.Next();
        }

        public static int CreateRandomNonNegativeInt32(int includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // "Random.Next" method lower bound is equal to zero.
            return random.Next(GetUpperBound(includedMaxValue));
        }

        public static int CreateRandomPositiveInt32(int includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // "Random.Next" method lower bound is equal to zero.
            return random.Next(1, GetUpperBound(includedMaxValue));
        }

        public static int CreateRandomInt32(int includedMinValue, int includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            return random.Next(includedMinValue, GetUpperBound(includedMaxValue));
        }

        public static int CreateRandomInt32(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomInt32(int.MinValue, int.MaxValue, random);
        }

        public static int GetRandomCountNumber(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNonNegativeInt32(Cases.TestConstants.MaxCollectionSize, random);
        }

        public static int GetRandomPositiveCountNumber(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomPositiveInt32(Cases.TestConstants.MaxCollectionSize, random);
        }

        public static int GetRandomSmallCountNumber(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNonNegativeInt32(Cases.TestConstants._100, random);
        }

        public static int GetRandomPositiveSmallCountNumber(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomPositiveInt32(Cases.TestConstants._100, random);
        }

        public static int? CreateRandomNullableInt32(int includedMinValue, int includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            var result = random.Next(includedMinValue, GetUpperBound(includedMaxValue));
            return NumberParityFunction.ReturnNullIfOdd(result);
        }

        public static int? CreateRandomNullableInt32(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt32(int.MinValue, int.MaxValue, random);
        }

        #endregion

        #region Create Int64

        public static long CreateRandomNonNegativeInt64(Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextInt64" method returns non-negative values.
            return random.NextInt64();
        }

        public static long CreateRandomNonNegativeInt64(long includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextInt64" method lower bound is equal to zero.
            return random.NextInt64(GetUpperBound(includedMaxValue));
        }

        public static long CreateRandomInt64(long includedMinValue, long includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextInt64(includedMinValue, GetUpperBound(includedMaxValue));
        }

        public static long CreateRandomInt64(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomInt64(long.MinValue, long.MaxValue, random);
        }

        public static long? CreateRandomNullableInt64(long includedMinValue, long includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            var result = random.NextInt64(includedMinValue, GetUpperBound(includedMaxValue));
            return NumberParityFunction.ReturnNullIfOdd(result);
        }

        public static long? CreateRandomNullableInt64(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt64(long.MinValue, long.MaxValue, random);
        }

        #endregion

        #region Create Single

        public static float CreateRandomNonNegativeSingle(Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextSingle" method returns values within a range [0.0F, 1.0F).
            return random.NextSingle();
        }

        public static float CreateRandomNonNegativeSingle(float includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextSingle" method lower bound is equal to zero.
            return random.NextSingle(GetUpperBound(includedMaxValue));
        }

        public static float CreateRandomSingle(float includedMinValue, float includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextSingle(includedMinValue, GetUpperBound(includedMaxValue));
        }

        public static float CreateRandomSingle(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomSingle(float.MinValue, float.MaxValue, random);
        }

        public static float? CreateRandomNullableSingle(float includedMinValue,
            float includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            var result = random.NextSingle(includedMinValue, GetUpperBound(includedMaxValue));
            return NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(result);
        }

        public static float? CreateRandomNullableSingle(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableSingle(float.MinValue, float.MaxValue, random);
        }

        #endregion

        #region Create Double

        public static double CreateRandomNonNegativeDouble(Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextDouble" method returns values within a range [0.0, 1.0).
            return random.NextDouble();
        }

        public static double CreateRandomNonNegativeDouble(double includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextDouble" method lower bound is equal to zero.
            return random.NextDouble(GetUpperBound(includedMaxValue));
        }

        public static double CreateRandomDouble(double includedMinValue, double includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextDouble(includedMinValue, GetUpperBound(includedMaxValue));
        }

        public static double CreateRandomDouble(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomDouble(double.MinValue, double.MaxValue, random);
        }

        public static double? CreateRandomNullableDouble(double includedMinValue,
            double includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            var result = random.NextDouble(includedMinValue, GetUpperBound(includedMaxValue));
            return NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(result);
        }

        public static double? CreateRandomNullableDouble(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableDouble(double.MinValue, double.MaxValue, random);
        }

        #endregion

        #region Create Decimal

        public static decimal CreateRandomNonNegativeDecimal(Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextDecimal" method returns values within a range [0.0M, 1.0M).
            return random.NextDecimal();
        }

        public static decimal CreateRandomNonNegativeDecimal(decimal includedMaxValue,
            Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextDecimal" method lower bound is equal to decimal.Zero.
            return random.NextDecimal(GetUpperBound(includedMaxValue));
        }

        public static decimal CreateRandomDecimal(decimal includedMinValue,
            decimal includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextDecimal(includedMinValue, GetUpperBound(includedMaxValue));
        }

        public static decimal CreateRandomDecimal(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomDecimal(decimal.MinValue, decimal.MaxValue, random);
        }

        public static decimal? CreateRandomNullableDecimal(decimal includedMinValue,
            decimal includedMaxValue, Random? random = null)
        {
            random ??= RandomInstance;

            var result = random.NextDecimal(includedMinValue, GetUpperBound(includedMaxValue));
            return NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(result);
        }

        public static decimal? CreateRandomNullableDecimal(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableDecimal(decimal.MinValue, decimal.MaxValue, random);
        }

        #endregion

        #region Create Dummy Struct

        public static DummyStruct CreateRandomDummyStruct(Random? random = null)
        {
            random ??= RandomInstance;

            int value = CreateRandomInt32(random);

            return new DummyStruct(value);
        }

        public static DummyStruct? CreateRandomNullableDummyStruct(Random? random = null)
        {
            random ??= RandomInstance;

            int value = CreateRandomInt32(random);

            var result = new DummyStruct(value);
            return NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(result);
        }

        #endregion

        #region Create Dummy Class

        public static DummyClass CreateRandomDummyClass(Random? random = null)
        {
            random ??= RandomInstance;

            int value = CreateRandomInt32(random);

            return new DummyClass(value);
        }

        public static DummyClass? CreateRandomNullableDummyClass(Random? random = null)
        {
            random ??= RandomInstance;

            int value = CreateRandomInt32(random);

            var result = new DummyClass(value);
            return NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(result);
        }

        #endregion

        #region Create String List

        public static IReadOnlyList<string> CreateRandomStringList(int count, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomString(rand),
                random: random
            );
        }

        public static IReadOnlyList<string> CreateRandomStringList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomString(rand),
                random: random
            );
        }

        public static IReadOnlyList<string?> CreateRandomNullableStringList(int count,
            Func<string, string?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomString(rand)),
                random: random
            );
        }

        public static IReadOnlyList<string?> CreateRandomNullableStringList(int count,
            Random? random = null)
        {
            return CreateRandomNullableStringList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<string?> CreateRandomNullableStringList(
            Func<string, string?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableStringList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<string?> CreateRandomNullableStringList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableStringList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i),
                random: random
            );
        }

        #endregion

        #region Create Int32 List

        public static IReadOnlyList<int> CreateRandomInt32List(int count, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomInt32(rand),
                random: random
            );
        }

        public static IReadOnlyList<int> CreateRandomInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomInt32List(
                count: count,
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(int count,
            Func<int, int?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= i => NumberParityFunction.ReturnNullIfOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomInt32(rand)),
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(int count,
            Random? random = null)
        {
            return CreateRandomNullableInt32List(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullIfOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(
            Func<int, int?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => NumberParityFunction.ReturnNullIfOdd(i);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableInt32List(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableInt32List(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullIfOdd(i),
                random: random
            );
        }

        #endregion

        #region Create Int64 List

        public static IReadOnlyList<long> CreateRandomInt64List(int count, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomInt64(rand),
                random: random
            );
        }

        public static IReadOnlyList<long> CreateRandomInt64List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomInt64List(
                count: count,
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(int count,
            Func<long, long?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= l => NumberParityFunction.ReturnNullIfOdd(l);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomInt32(rand)),
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(int count,
            Random? random = null)
        {
            return CreateRandomNullableInt64List(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullIfOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(
            Func<long, long?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= l => NumberParityFunction.ReturnNullIfOdd(l);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableInt64List(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableInt64List(
                count: count,
                valueTransformer: l => NumberParityFunction.ReturnNullIfOdd(l),
                random: random
            );
        }

        #endregion

        #region Create Single List

        public static IReadOnlyList<float> CreateRandomSingleList(int count, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomSingle(rand),
                random: random
            );
        }

        public static IReadOnlyList<float> CreateRandomSingleList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomSingleList(count, random);
        }

        public static IReadOnlyList<float?> CreateRandomNullableSingleList(int count,
           Func<float, float?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= f => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(f);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomSingle(rand)),
                random: random
            );
        }

        public static IReadOnlyList<float?> CreateRandomNullableSingleList(int count,
            Random? random = null)
        {
            return CreateRandomNullableSingleList(
                count: count,
                valueTransformer: f => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(f),
                random: random
            );
        }

        public static IReadOnlyList<float?> CreateRandomNullableSingleList(
            Func<float, float?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= f => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(f);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableSingleList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<float?> CreateRandomNullableSingleList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableSingleList(
                count: count,
                valueTransformer: f => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(f),
                random: random
            );
        }

        #endregion

        #region Create Double List

        public static IReadOnlyList<double> CreateRandomDoubleList(int count, Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDouble(rand),
                random: random
            );
        }

        public static IReadOnlyList<double> CreateRandomDoubleList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomDoubleList(count, random);
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(int count,
           Func<double, double?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomDouble(rand)),
                random: random
            );
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(int count,
            Random? random = null)
        {
            return CreateRandomNullableDoubleList(
                count: count,
                valueTransformer: d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d),
                random: random
            );
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(
            Func<double, double?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDoubleList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDoubleList(
                count: count,
                valueTransformer: d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d),
                random: random
            );
        }

        #endregion

        #region Create Decimal List

        public static IReadOnlyList<decimal> CreateRandomDecimalList(int count,
            Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDecimal(rand),
                random: random
            );
        }

        public static IReadOnlyList<decimal> CreateRandomDecimalList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomDecimalList(count, random);
        }

        public static IReadOnlyList<decimal?> CreateRandomNullableDecimalList(int count,
           Func<decimal, decimal?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomDecimal(rand)),
                random: random
            );
        }

        public static IReadOnlyList<decimal?> CreateRandomNullableDecimalList(int count,
            Random? random = null)
        {
            return CreateRandomNullableDecimalList(
                count: count,
                valueTransformer: d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d),
                random: random
            );
        }

        public static IReadOnlyList<decimal?> CreateRandomNullableDecimalList(
            Func<decimal, decimal?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDecimalList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<decimal?> CreateRandomNullableDecimalList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDecimalList(
                count: count,
                valueTransformer: d => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(d),
                random: random
            );
        }

        #endregion

        #region Create Dummy Struct List

        public static IReadOnlyList<DummyStruct> CreateRandomDummyStructList(int count,
            Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDummyStruct(rand),
                random: random
            );
        }

        public static IReadOnlyList<DummyStruct> CreateRandomDummyStructList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDummyStruct(rand),
                random: random
            );
        }

        public static IReadOnlyList<DummyStruct?> CreateRandomNullableDummyStructList(int count,
            Func<DummyStruct, DummyStruct?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= i => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomDummyStruct(rand)),
                random: random
            );
        }

        public static IReadOnlyList<DummyStruct?> CreateRandomNullableDummyStructList(int count,
            Random? random = null)
        {
            return CreateRandomNullableDummyStructList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<DummyStruct?> CreateRandomNullableDummyStructList(
            Func<DummyStruct, DummyStruct?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(i);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDummyStructList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<DummyStruct?> CreateRandomNullableDummyStructList(
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDummyStructList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullableIfRandomInt32IsOdd(i),
                random: random
            );
        }

        #endregion

        #region Create Dummy Class List

        public static IReadOnlyList<DummyClass> CreateRandomDummyClassList(int count,
          Random? random = null)
        {
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDummyClass(rand),
                random: random
            );
        }

        public static IReadOnlyList<DummyClass> CreateRandomDummyClassList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateList(
                count: count,
                valueFactory: (i, rand) => CreateRandomDummyClass(rand),
                random: random
            );
        }

        public static IReadOnlyList<DummyClass?> CreateRandomNullableDummyClassList(int count,
            Func<DummyClass, DummyClass?>? valueTransformer, Random? random = null)
        {
            valueTransformer ??= i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomDummyClass(rand)),
                random: random
            );
        }

        public static IReadOnlyList<DummyClass?> CreateRandomNullableDummyClassList(int count,
            Random? random = null)
        {
            return CreateRandomNullableDummyClassList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<DummyClass?> CreateRandomNullableDummyClassList(
            Func<DummyClass, DummyClass?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i);

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDummyClassList(
                count: count,
                valueTransformer: valueTransformer,
                random: random
            );
        }

        public static IReadOnlyList<DummyClass?> CreateRandomNullableDummyClassList(
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber(random);
            return CreateRandomNullableDummyClassList(
                count: count,
                valueTransformer: i => NumberParityFunction.ReturnNullValueIfRandomInt32IsOdd(i),
                random: random
            );
        }

        #endregion
    }
}
