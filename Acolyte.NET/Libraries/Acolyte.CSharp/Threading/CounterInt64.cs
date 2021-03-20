using System.Threading;

namespace Acolyte.Threading
{
    public sealed class CounterInt64
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
