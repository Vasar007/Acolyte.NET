using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Collections;

namespace Acolyte.Tests
{
    internal static class TestDataCreator
    {
        private static readonly Random RandomInstance = new Random();


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

            return random.Next();
        }

        internal static int CreateRandomNonNegativeInt32(int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.Next(maxValue);
        }

        internal static int CreateRandomInt32(int minValue, int maxValue, Random? random = null)
        {
            random ??= RandomInstance;

            return random.Next(minValue, maxValue);
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
                .Select(i => CreateRandomNonNegativeInt32(random))
                .ToReadOnlyList();
        }

        internal static IReadOnlyList<int> CreateRandomInt32List(Random? random = null)
        {
            random ??= RandomInstance;

            int count = random.Next();
            return CreateRandomInt32List(count, random);
        }
    }
}