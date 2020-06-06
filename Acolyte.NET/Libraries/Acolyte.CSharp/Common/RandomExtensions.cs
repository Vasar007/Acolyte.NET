using System;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    /// <summary>
    /// Extensions method for <see cref="Random" /> class.
    /// </summary>
    /// <remarks>
    /// <see href="https://stackoverflow.com/a/13095144/8581036" />
    /// </remarks>
    public static class RandomExtensionMethods
    {
        /// <summary>
        /// Returns a random long from <paramref name="min" /> (inclusive) to
        /// <paramref name="max" /> (exclusive).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="min">The inclusive minimum bound.</param>
        /// <param name="max">The exclusive maximum bound. Must be greater than min</param>
        /// <returns>A random long from min (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="max" /> is less or equal to <paramref name="min" />.
        /// </exception>
        public static long NextInt64(this Random random, long min, long max)
        {
            random.ThrowIfNull(nameof(random));

            if (max <= min)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(max), max,
                    $"{nameof(max)} ({max.ToString()}) must be greater than " +
                    $"{nameof(min)} ({min.ToString()})."
                );
            }

            // Working with ulong so that modulo works correctly with values > long.MaxValue.
            ulong uRange = (ulong) (max - min);

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

            return (long) (ulongRand % uRange) + min;
        }

        /// <summary>
        /// Returns a random non-negative long that is less than <paramref name="max" />.
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <param name="max">The exclusive maximum bound. Must be greater than 0.</param>
        /// <returns>A random long from 0 (inclusive) to max (exclusive).</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="max" /> is less or equal to 0.
        /// </exception>
        public static long NextInt64(this Random random, long max)
        {
            return random.NextInt64(0, max);
        }

        /// <summary>
        /// Returns a random non-negative long (except long.MaxValue, similar to random.Next()).
        /// </summary>
        /// <param name="random">The given random instance.</param>
        /// <returns>A random non-negative long.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="random" /> is <c>null</c>.
        /// </exception>
        public static long NextInt64(this Random random)
        {
            return random.NextInt64(0, long.MaxValue);
        }
    }
}
