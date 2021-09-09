using System;
using System.Threading;

namespace Acolyte.Threading
{
    public sealed class CounterInt32 : IEquatable<CounterInt32>
    {
        private int _counter;

        public int Value => Thread.VolatileRead(ref _counter);


        public CounterInt32()
        {
            _counter = default;
        }

        public CounterInt32(int startValue)
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
            return Equals(obj as CounterInt32);
        }

        #endregion

        #region IEquatable<CounterInt64> Implementation

        /// <inheritdoc />
        public bool Equals(CounterInt32? other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Value.Equals(other.Value);
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="CounterInt32" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(CounterInt32? left, CounterInt32? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="CounterInt32" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator !=(CounterInt32? left, CounterInt32? right)
        {
            return !(left == right);
        }

        public CounterInt32 Increment()
        {
            Interlocked.Increment(ref _counter);
            return this;
        }

        public int ExchangeIncrement()
        {
            return Interlocked.Exchange(ref _counter, _counter + 1);
        }

        public CounterInt32 Decrement()
        {
            Interlocked.Decrement(ref _counter);
            return this;
        }

        public int ExchangeDecrement()
        {
            return Interlocked.Exchange(ref _counter, _counter - 1);
        }

        public CounterInt32 Add(int value)
        {
            Interlocked.Exchange(ref _counter, _counter + value);
            return this;
        }

        public CounterInt32 Subtract(int value)
        {
            Interlocked.Exchange(ref _counter, _counter - value);
            return this;
        }

        public CounterInt32 Reset()
        {
            return Reset(default);
        }

        public CounterInt32 Reset(int value)
        {
            Interlocked.Exchange(ref _counter, value);
            return this;
        }

        public static implicit operator int(CounterInt32 counter)
        {
            return counter.Value;
        }

        public static CounterInt32 operator ++(CounterInt32 counter)
        {
            return counter.Increment();
        }

        public static CounterInt32 operator --(CounterInt32 counter)
        {
            return counter.Decrement();
        }

        public static CounterInt32 operator +(CounterInt32 counter, int value)
        {
            return counter.Add(value);
        }

        public static CounterInt32 operator -(CounterInt32 counter, int value)
        {
            return counter.Subtract(value);
        }
    }
}
