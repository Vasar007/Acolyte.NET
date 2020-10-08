using System.Threading;

namespace Acolyte.Tests.Common
{
    public sealed class CounterInt32
    {
        private int _counter;

        public int Value => Interlocked.CompareExchange(ref _counter, 0, 0);


        public CounterInt32()
        {
            _counter = default;
        }

        public CounterInt32(int startValue)
        {
            _counter = startValue;
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
    }
}
