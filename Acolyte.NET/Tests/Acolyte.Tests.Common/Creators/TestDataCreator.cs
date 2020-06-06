﻿using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Collections;

namespace Acolyte.Tests.Creators
{
    // TODO: move some methods to Acolyte.CSharp assembly because it can be useful.
    public static class TestDataCreator
    {
        private static readonly Random RandomInstance = new Random();

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

        #endregion

        #region Int32 Helpers

        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }

        public static int? ReturnNullIfOdd(int value)
        {
            // Convert all odd values to null.
            return IsEven(value)
                ? value
                : (int?) null;
        }

        #endregion

        #region Int64 Helpers

        public static bool IsEven(long value)
        {
            return (value & 1) == 0;
        }

        public static long? ReturnNullIfOdd(long value)
        {
            // Convert all odd values to null.
            return IsEven(value)
                ? value
                : (long?) null;
        }

        #endregion

        #region Single Helpers

        public static float? ReturnNullIfRandomInt32IsOdd(float value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = CreateRandomInt32();
            return IsEven(mark)
                ? value
                : (float?) null;
        }

        #endregion

        #region Double Helpers

        public static double? ReturnNullIfRandomInt32IsOdd(double value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = CreateRandomInt32();
            return IsEven(mark)
                ? value
                : (double?) null;
        }

        #endregion

        #region Decimal Helpers

        public static decimal? ReturnNullIfRandomInt32IsOdd(decimal value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = CreateRandomInt32();
            return IsEven(mark)
                ? value
                : (decimal?) null;
        }

        #endregion

        #region Create Generic List

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<int, Random, TItem> valueFactory, Random? random = null)
        {
            random ??= RandomInstance;

            // "Enumerable.Range" method throws exception when "count" < 0.
            return Enumerable
                .Range(0, count)
                .Select(i => valueFactory(i, random))
                .ToReadOnlyList();
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<Random, TItem> valueFactory, Random? random = null)
        {
            random ??= RandomInstance;

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(rand),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count,
            Func<int, TItem> valueFactory, Random? random = null)
        {
            random ??= RandomInstance;

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(i),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(int count, Func<TItem> valueFactory,
            Random? random = null)
        {
            random ??= RandomInstance;

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueFactory(),
                random: random
            );
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<int, Random, TItem> valueFactory,
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<Random, TItem> valueFactory,
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<int, TItem> valueFactory,
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        public static IReadOnlyList<TItem> CreateList<TItem>(Func<TItem> valueFactory,
            Random? random = null)
        {
            random ??= RandomInstance;

            int count = GetRandomCountNumber();
            return CreateList(count, valueFactory, random);
        }

        #endregion

        #region Create String

        public static string CreateRandomString(int length, Random? random = null)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length), length, "Length must pe positive."
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

        #endregion

        #region Create Int32

        public static int CreateRandomNonNegativeInt32(Random? random = null)
        {
            random ??= RandomInstance;

            // "Random.Next" method returns non-negative values.
            return random.Next();
        }

        public static int CreateRandomNonNegativeInt32(int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // "Random.Next" method lower bound is equal to zero.
            return random.Next(GetUpperBound(maxValue));
        }

        public static int CreateRandomInt32(int minValue, int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.Next(minValue, GetUpperBound(maxValue));
        }

        public static int CreateRandomInt32(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomInt32(int.MinValue, int.MaxValue, random);
        }

        public static int GetRandomCountNumber(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize, random);
        }

        #endregion

        #region Create Int64

        public static long CreateRandomNonNegativeInt64(Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextInt64" method returns non-negative values.
            return random.NextInt64();
        }

        public static long CreateRandomNonNegativeInt64(long maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // "RandomExtensions.NextInt64" method lower bound is equal to zero.
            return random.NextInt64(GetUpperBound(maxValue));
        }

        public static long CreateRandomInt64(long minValue, long maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextInt64(minValue, GetUpperBound(maxValue));
        }

        public static long CreateRandomInt64(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomInt64(long.MinValue, long.MaxValue, random);
        }

        #endregion

        #region Create Double

        public static double CreateRandomDouble(Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextDouble();
        }

        #endregion

        #region Choise Item

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

        #region Create Int32 List

        public static IReadOnlyList<int> CreateRandomInt32List(int count, Random? random = null)
        {
            random ??= RandomInstance;

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
            random ??= RandomInstance;
            valueTransformer ??= i => ReturnNullIfOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomInt32(rand)),
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(int count,
            Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt32List(
                count: count,
                valueTransformer: i => ReturnNullIfOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(
            Func<int, int?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => ReturnNullIfOdd(i);

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
                valueTransformer: i => ReturnNullIfOdd(i),
                random: random
            );
        }

        #endregion

        #region Create Int64 List

        public static IReadOnlyList<long> CreateRandomInt64List(int count, Random? random = null)
        {
            random ??= RandomInstance;

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
            random ??= RandomInstance;
            valueTransformer ??= i => ReturnNullIfOdd(i);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomInt32(rand)),
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(int count,
            Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt64List(
                count: count,
                valueTransformer: i => ReturnNullIfOdd(i),
                random: random
            );
        }

        public static IReadOnlyList<long?> CreateRandomNullableInt64List(
            Func<long, long?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= i => ReturnNullIfOdd(i);

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
                valueTransformer: i => ReturnNullIfOdd(i),
                random: random
            );
        }

        #endregion

        #region Create Double List

        public static IReadOnlyList<double> CreateRandomDoubleList(int count, Random? random = null)
        {
            random ??= RandomInstance;

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
            random ??= RandomInstance;
            valueTransformer ??= d => ReturnNullIfRandomInt32IsOdd(d);

            return CreateList(
                count: count,
                valueFactory: (i, rand) => valueTransformer(CreateRandomDouble(rand)),
                random: random
            );
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(int count,
            Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableDoubleList(
                count: count,
                valueTransformer: d => ReturnNullIfRandomInt32IsOdd(d),
                random: random
            );
        }

        public static IReadOnlyList<double?> CreateRandomNullableDoubleList(
            Func<double, double?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= d => ReturnNullIfRandomInt32IsOdd(d);

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
                valueTransformer: d => ReturnNullIfRandomInt32IsOdd(d),
                random: random
            );
        }

        #endregion
    }
}
