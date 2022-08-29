using System;

namespace Acolyte.Basic.Randomness
{
    /// <summary>
    /// Thread-safe equivalent of System.Random, using just static methods.
    /// If all you want is a source of random numbers, this is an easy class to
    /// use. If you need to specify your own seeds (e.g. for reproducible sequences
    /// of numbers), use <see cref="Random" />.
    /// </summary>
    public static class StaticRandom
    {
        /// <summary>
        /// Default initialized random class instance.
        /// </summary>
        private static readonly Random _random = new Random();

        /// <summary>
        /// Object for lock statement.
        /// </summary>
        private static readonly object _lock = new object();


        #region Standard Random Methods

        /// <inheritdoc cref="Random.Next()" />
        public static int Next()
        {
            lock (_lock)
            {
                return _random.Next();
            }
        }

        /// <inheritdoc cref="Random.Next(int)" />
        public static int Next(int maxValue)
        {
            lock (_lock)
            {
                return _random.Next(maxValue);
            }
        }

        /// <inheritdoc cref="Random.Next(int, int)" />
        public static int Next(int minValue, int maxValue)
        {
            lock (_lock)
            {
                return _random.Next(minValue, maxValue);
            }
        }

        /// <inheritdoc cref="Random.NextDouble" />
        public static double NextDouble()
        {
            lock (_lock)
            {
                return _random.NextDouble();
            }
        }

        /// <inheritdoc cref="Random.NextBytes(byte[])" />
        public static void NextBytes(byte[] buffer)
        {
            lock (_lock)
            {
                _random.NextBytes(buffer);
            }
        }

        #endregion

        #region Extended Random Methods

        /// <inheritdoc cref="RandomExtensionMethods.NextInt64(Random, long, long)" />
        public static long NextInt64(long minValue, long maxValue)
        {
            lock (_lock)
            {
                return _random.NextInt64(minValue, maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextInt64(Random, long)" />
        public static long NextInt64(long maxValue)
        {
            lock (_lock)
            {
                return _random.NextInt64(maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextInt64(Random)" />
        public static long NextInt64()
        {
            lock (_lock)
            {
                return _random.NextInt64();
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextSingle(Random, float, float)" />
        public static float NextSingle(float minValue, float maxValue)
        {
            lock (_lock)
            {
                return _random.NextSingle(minValue, maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextSingle(Random, float)" />
        public static float NextSingle(float maxValue)
        {
            lock (_lock)
            {
                return _random.NextSingle(maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextSingle(Random)" />
        public static float NextSingle()
        {
            lock (_lock)
            {
                return _random.NextSingle();
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextDouble(Random, double, double)" />
        public static double NextDouble(double minValue, double maxValue)
        {
            lock (_lock)
            {
                return _random.NextDouble(minValue, maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextDouble(Random, double)" />
        public static double NextDouble(double maxValue)
        {
            lock (_lock)
            {
                return _random.NextDouble(maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextFullRangeInt32(Random)" />
        public static int NextFullRangeInt32()
        {
            lock (_lock)
            {
                return _random.NextFullRangeInt32();
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextDecimal(Random)" />
        public static decimal NextDecimal()
        {
            lock (_lock)
            {
                return _random.NextDecimal();
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextDecimal(Random, decimal)" />
        public static decimal NextDecimal(decimal maxValue)
        {
            lock (_lock)
            {
                return _random.NextDecimal(maxValue);
            }
        }

        /// <inheritdoc cref="RandomExtensionMethods.NextDecimal(Random, decimal, decimal)" />
        public static decimal NextDecimal(decimal minValue, decimal maxValue)
        {
            lock (_lock)
            {
                return _random.NextDecimal(minValue, maxValue);
            }
        }

        #endregion
    }
}
