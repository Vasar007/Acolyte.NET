using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Collections;

namespace Acolyte.Tests
{
    // TODO: move some methods to Acolyte.Charp assembly because it can be useful.
    internal static class TestDataCreator
    {
        private static readonly Random RandomInstance = new Random();

        internal static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }

        internal static string CreateRandomString(int length, Random? random = null)
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
                Enumerable.Repeat(chars, length)
                    .Select(str => str[random.Next(str.Length)])
                    .ToArray()
            );
        }

        internal static int CreateRandomNonNegativeInt32(Random? random = null)
        {
            random ??= RandomInstance;

            // Random.Next return non-negative values.
            return random.Next();
        }

        internal static int CreateRandomNonNegativeInt32(int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            // Random.Next lower bound is equal to zero.
            return random.Next(maxValue);
        }

        internal static int CreateRandomInt32(int minValue, int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.Next(minValue, maxValue);
        }

        internal static int CreateRandomInt32(Random? random = null)
        {
            return CreateRandomInt32(int.MinValue, int.MaxValue, random);
        }

        internal static double CreateRandomDouble(Random? random = null)
        {
            random ??= RandomInstance;

            return random.NextDouble();
        }

        internal static IReadOnlyList<int> CreateRandomInt32List(int count, Random? random = null)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count), count, "Count parameter must be positive."
                );
            }

            return Enumerable
                .Range(1, count)
                .Select(i => CreateRandomInt32(random))
                .ToReadOnlyList();
        }

        internal static IReadOnlyList<int> CreateRandomInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            // Random.Next return non-negative values.
            int count = random.Next();
            return CreateRandomInt32List(count, random);
        }

        internal static (TSource item, int index) ChoiceWithIndex<TSource>(
            IReadOnlyList<TSource> source, Random? random = null)
        {
            source.ThrowIfNullOrEmpty(nameof(source));

            random ??= RandomInstance;

            int randomItemIndex = random.Next(source.Count);
            return (source[randomItemIndex], randomItemIndex);
        }

        internal static TSource Choice<TSource>(IReadOnlyList<TSource> source,
            Random? random = null)
        {
            return ChoiceWithIndex(source, random).item;
        }
    }
}