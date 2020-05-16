﻿using System.Threading;

namespace Acolyte.Tests.Common
{
    public sealed class CounterInt32
    {
        private int _counter;

        // Read Int32 is atomic operation on both x86 and x64.
        public int Value => _counter;


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

    public sealed class CounterInt64
    {
        private long _counter;

        public long Value => Interlocked.Read(ref _counter);


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
