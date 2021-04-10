using System;
using System.Threading;

namespace Acolyte.Threading
{
    public sealed class CounterInt64 : IEquatable<CounterInt64>
    {
        private long _counter;

        public long Value => Thread.VolatileRead(ref _counter);


        public CounterInt64()
        {
            _counter = default;
        }

        public CounterInt64(long startValue)
        {
            _counter = startValue;
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return Equals(obj as CounterInt64);
        }

        #endregion

        #region IEquatable<CounterInt64> Implementation

        /// <inheritdoc />
        public bool Equals(CounterInt64? other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Value.Equals(other.Value);
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="CounterInt64" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals, <see langword="false" />
        /// otherwise.
        /// </returns>
        public static bool operator ==(CounterInt64? left, CounterInt64? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="CounterInt64" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals, <see langword="false" />
        /// otherwise.
        /// </returns>
        public static bool operator !=(CounterInt64? left, CounterInt64? right)
        {
            return !(left == right);
        }

        public CounterInt64 Increment()
        {
            Interlocked.Increment(ref _counter);
            return this;
        }

        public long ExchangeIncrement()
        {
            return Interlocked.Exchange(ref _counter, _counter + 1L);
        }

        public CounterInt64 Decrement()
        {
            Interlocked.Decrement(ref _counter);
            return this;
        }

        public long ExchangeDecrement()
        {
            return Interlocked.Exchange(ref _counter, _counter - 1L);
        }

        public CounterInt64 Advance(long value)
        {
            Interlocked.Exchange(ref _counter, _counter + value);
            return this;
        }

        public CounterInt64 Reset()
        {
            return Reset(default);
        }

        public CounterInt64 Reset(long value)
        {
            Interlocked.Exchange(ref _counter, value);
            return this;
        }

        public static implicit operator long(CounterInt64 counter)
        {
            return counter.Value;
        }

        public static CounterInt64 operator ++(CounterInt64 counter)
        {
            return counter.Increment();
        }

        public static CounterInt64 operator --(CounterInt64 counter)
        {
            return counter.Decrement();
        }
    }
}
