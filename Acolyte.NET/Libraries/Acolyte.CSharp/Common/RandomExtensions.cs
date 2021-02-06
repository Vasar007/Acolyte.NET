using System;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    /// <summary>
    /// Extensions method for <see cref="Random" /> class.
    /// </summary>
    /// <remarks>
    /// <see href="https://stackoverflow.com/a/13095144/8581036" />
    /// <see href="https://stackoverflow.com/a/28860710/8581036" />
    /// </remarks>
    public static class RandomExtensionMethods
    {
        /// <summary>
        /// Returns a random long number from <paramref name="minValue" /> (inclusive) to
        /// <paramref name="maxValue" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="minValue">The inclusive minimum bound.</param>
        /// <param name="maxValue">
        /// The exclusive maximum bound. Must be greater than <paramref name="minValue" />.
        /// </param>
        /// <returns>A random long number from min (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <paramref name="minValue" />.
        /// </exception>
        public static long NextInt64(this Random random, long minValue, long maxValue)
        {
            random.ThrowIfNull(nameof(random));

            if (maxValue <= minValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxValue), maxValue,
                    $"{nameof(maxValue)} ({maxValue.ToString()}) must be greater than " +
                    $"{nameof(minValue)} ({minValue.ToString()})."
                );
            }

            // Working with ulong so that modulo works correctly with values > long.MaxValue.
            ulong uRange = (ulong) (maxValue - minValue);

            // Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
            // for more information.
            // In the worst case, the expected number of calls is 2 (though usually it's
            // much closer to 1) so this loop doesn't really hurt performance at all.
            ulong ulongRand;
            do
            {
               var buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong) BitConverter.ToInt64(buf, 0);
            }
            while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long) (ulongRand % uRange) + minValue;
        }

        /// <summary>
        /// Returns a random non-negative long that is less than <paramref name="maxValue" />.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="maxValue">The exclusive maximum bound. Must be greater than 0.</param>
        /// <returns>A random long number from 0 (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to 0.
        /// </exception>
        public static long NextInt64(this Random random, long maxValue)
        {
            return random.NextInt64(minValue: 0L, maxValue);
        }

        /// <summary>
        /// Returns a random non-negative long number (except long.MaxValue, similar to
        /// random.Next()).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <returns>A random non-negative long number.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        public static long NextInt64(this Random random)
        {
            return random.NextInt64(minValue: 0L, long.MaxValue);
        }

        /// <summary>
        /// Returns a random floating-point number from <paramref name="minValue" /> (inclusive) to
        /// <paramref name="maxValue" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="minValue">The inclusive minimum bound.</param>
        /// <param name="maxValue">
        /// The exclusive maximum bound. Must be greater than <paramref name="minValue" />.
        /// </param>
        /// <returns>A random floating-point from min (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <paramref name="minValue" />.
        /// </exception>
        public static float NextSingle(this Random random, float minValue, float maxValue)
        {
            random.ThrowIfNull(nameof(random));

            if (maxValue <= minValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxValue), maxValue,
                    $"{nameof(maxValue)} ({maxValue.ToString()}) must be greater than " +
                    $"{nameof(minValue)} ({minValue.ToString()})."
                );
            }

            // Perform arithmetic in double type to avoid overflowing.
            double range = (double) maxValue - (double) minValue;
            double sample = random.NextDouble();
            double scaled = (sample * range) + float.MinValue;
            return (float) scaled;
        }

        /// <summary>
        /// Returns a random non-negative floating-point number that is less than
        /// <paramref name="maxValue" />.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="maxValue">The exclusive maximum bound. Must be greater than 0.0F.</param>
        /// <returns>
        /// A random floating-point number from 0.0F (inclusive) to max (exclusive).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to 0.0F.
        /// </exception>
        public static float NextSingle(this Random random, float maxValue)
        {
            return random.NextSingle(minValue: 0.0F, maxValue);
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0F,
        /// and less than 1.0F.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <returns>A random non-negative long number.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        public static float NextSingle(this Random random)
        {
            return Convert.ToSingle(random.NextDouble());
        }

        /// <summary>
        /// Returns a random floating-point number from <paramref name="minValue" /> (inclusive) to
        /// <paramref name="maxValue" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="minValue">The inclusive minimum bound.</param>
        /// <param name="maxValue">
        /// The exclusive maximum bound. Must be greater than <paramref name="minValue" />.
        /// </param>
        /// <returns>
        /// A random floating-point number from min (inclusive) to max (exclusive).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <paramref name="minValue" />.
        /// </exception>
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            random.ThrowIfNull(nameof(random));

            if (maxValue <= minValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxValue), maxValue,
                    $"{nameof(maxValue)} ({maxValue.ToString()}) must be greater than " +
                    $"{nameof(minValue)} ({minValue.ToString()})."
                );
            }

            double range = maxValue - minValue;
            double sample = random.NextDouble();
            double scaled = (sample * range) + double.MinValue;
            return scaled;
        }

        /// <summary>
        /// Returns a random non-negative floating-point number that is less than
        /// <paramref name="maxValue" />.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="maxValue">The exclusive maximum bound. Must be greater than 0.0.</param>
        /// <returns>A random floating-point from 0.0 (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to 0.0.
        /// </exception>
        public static double NextDouble(this Random random, double maxValue)
        {
            return random.NextDouble(minValue: 0.0, maxValue);
        }

        /// <summary>
        /// Returns an integer number with a random value across the entire range of
        /// possible values.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <returns>
        /// An integer value with a random number across the entire range of possible values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        public static int NextFullRangeInt32(this Random random)
        {
            random.ThrowIfNull(nameof(random));

            int firstBits = random.Next(0, 1 << 4) << 28;
            int lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        /// <summary>
        /// Returns a random floating-point number in the range [0.0000000000000000000000000000,
        /// 0.9999999999999999999999999999) with (theoretical) uniform and discrete distribution.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <returns>
        /// A random floating-point number with (theoretical) uniform and discrete distribution.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <paramref name="minValue" />.
        /// </exception>
        public static decimal NextDecimal(this Random random)
        {
            random.ThrowIfNull(nameof(random));

            decimal sample = decimal.One;
            // After ~200 million tries this never took more than one attempt but it is possible
            // to generate combinations of a, b, and c with the approach below resulting in
            // a sample >= 1.
            while (sample >= 1)
            {
                int a = random.NextFullRangeInt32();
                int b = random.NextFullRangeInt32();
                // The high bits of 0.9999999999999999999999999999m are 542101086.
                int c = random.Next(542101087);
                sample = new decimal(lo: a, mid: b, hi: c, isNegative: false, scale: 28);
            }
            return sample;
        }

        /// <summary>
        /// Returns a random floating-point number from <see cref="decimal.Zero" /> (inclusive) to
        /// <paramref name="maxValue" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="maxValue">
        /// The exclusive maximum bound. Must be greater than <see cref="decimal.Zero" /> (0.0M).
        /// </param>
        /// <returns>
        /// A random floating-point number from min (inclusive) to max (exclusive).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <see cref="decimal.Zero" /> (0.0M).
        /// </exception>
        public static decimal NextDecimal(this Random random, decimal maxValue)
        {
            return NextDecimal(random, minValue: decimal.Zero, maxValue);
        }

        /// <summary>
        /// Returns a random floating-point number from <paramref name="minValue" /> (inclusive) to
        /// <paramref name="maxValue" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="minValue">The inclusive minimum bound.</param>
        /// <param name="maxValue">
        /// The exclusive maximum bound. Must be greater than <paramref name="minValue" />.
        /// </param>
        /// <returns>
        /// A random floating-point number from min (inclusive) to max (exclusive).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue" /> is less or equal to <paramref name="minValue" />.
        /// </exception>
        public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
        {
            if (maxValue <= minValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxValue), maxValue,
                    $"{nameof(maxValue)} ({maxValue.ToString()}) must be greater than " +
                    $"{nameof(minValue)} ({minValue.ToString()})."
                );
            }

            decimal nextDecimalSample = random.NextDecimal();
            decimal scaled = maxValue * nextDecimalSample + minValue * (1 - nextDecimalSample);
            return scaled;
        }
    }
}
