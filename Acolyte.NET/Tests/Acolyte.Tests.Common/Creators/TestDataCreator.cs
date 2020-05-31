using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Collections;

namespace Acolyte.Tests.Creators
{
    // TODO: move some methods to Acolyte.CSharp assembly because it can be useful.
    public static class TestDataCreator
    {
        private static readonly Random RandomInstance = new Random();

        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }

        public static int? ReturnNullIfOdd(int value)
        {
            // Convert all odd valies to null.
            return IsEven(value)
                ? value
                : (int?) null;
        }

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

        public static int CreateRandomNonNegativeInt32(Random? random = null)
        {
            random ??= RandomInstance;

            // Random.Next return non-negative values.
            return random.Next();
        }

        public static int CreateRandomNonNegativeInt32(int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // Random.Next lower bound is equal to zero.
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

        public static IReadOnlyList<int> CreateRandomInt32List(int count, Random? random = null)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count), count, "Count parameter must be positive."
                );
            }

            random ??= RandomInstance;

            return Enumerable
                .Range(1, count)
                .Select(i => CreateRandomInt32(random))
                .ToReadOnlyList();
        }

        public static IReadOnlyList<int> CreateRandomInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize, random);
            return CreateRandomInt32List(count, random);
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(int count,
            Func<int, int?>? valueTransformer, Random? random = null)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count), count, "Count parameter must be positive."
                );
            }

            random ??= RandomInstance;
            valueTransformer ??= ReturnNullIfOdd;

            return Enumerable
                .Range(1, count)
                .Select(i => CreateRandomNonNegativeInt32(random))
                .Select(value => valueTransformer(value))
                .ToReadOnlyList();
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(int count,
            Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt32List(count, ReturnNullIfOdd, random);
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(
            Func<int, int?>? valueTransformer, Random? random = null)
        {
            random ??= RandomInstance;
            valueTransformer ??= ReturnNullIfOdd;

            int count = CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize, random);
            return CreateRandomNullableInt32List(count, valueTransformer, random);
        }

        public static IReadOnlyList<int?> CreateRandomNullableInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            return CreateRandomNullableInt32List(ReturnNullIfOdd, random);
        }

        public static double CreateRandomDouble(Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextDouble();
        }

        public static IReadOnlyList<double> CreateRandomDoubleList(int count, Random? random = null)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count), count, "Count parameter must be positive."
                );
            }

            random ??= RandomInstance;

            return Enumerable
                .Range(1, count)
                .Select(i => CreateRandomDouble(random))
                .ToReadOnlyList();
        }

        public static IReadOnlyList<double> CreateRandomDoubleList(Random? random = null)
        {
            random ??= RandomInstance;

            int count = CreateRandomNonNegativeInt32(TestHelper.MaxCollectionSize, random);
            return CreateRandomDoubleList(count, random);
        }

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

        private static int GetUpperBound(int potentionUpperBound)
        {
            return potentionUpperBound == int.MaxValue
                ? potentionUpperBound
                : potentionUpperBound + 1;
        }
    }
}
