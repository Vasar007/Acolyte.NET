using System;
using System.Threading;

namespace Acolyte.Data.Randomness
{
    /// <summary>
    /// Represents a pseudo-random number generator, which is a device that produces
    /// a sequence of numbers that meet certain statistical requirements for randomness.
    /// This class is thread-safe.
    /// </summary>
    public sealed class ThreadSafeRandom : Random
    {
        /// <summary>
        /// Spin-lock object to make random thread-safe.
        /// </summary>
        /// <remarks>
        /// Don't make it read-only because it can lead to shadow copies!
        /// <see cref="SpinLock" /> is a <see langword="struct" />!
        /// </remarks>
        private SpinLock _spinLock;


        /// <inheritdoc cref="Random()" />
        public ThreadSafeRandom()
            : base()
        {
            _spinLock = new SpinLock();
        }

        /// <inheritdoc cref="Random(int)" />
        public ThreadSafeRandom(
            int Seed)
            : base(Seed)
        {
            _spinLock = new SpinLock();
        }

        #region Random Overridden Methods

        /// <inheritdoc cref="Random.Next()" />
        public override int Next()
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            try
            {
                return base.Next();
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        /// <inheritdoc cref="Random.Next(int)" />
        public override int Next(int maxValue)
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            try
            {
                return base.Next(maxValue);
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        /// <inheritdoc cref="Random.Next(int, int)" />
        public override int Next(int minValue, int maxValue)
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            try
            {
                return base.Next(minValue, maxValue);
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        /// <inheritdoc cref="Random.NextDouble" />
        public override double NextDouble()
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            try
            {
                return base.NextDouble();
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        /// <inheritdoc cref="Random.NextBytes(byte[])" />
        public override void NextBytes(byte[] buffer)
        {
            bool lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            try
            {
                base.NextBytes(buffer);
            }
            finally
            {
                if (lockTaken)
                {
                    _spinLock.Exit();
                }
            }
        }

        #endregion
    }
}
