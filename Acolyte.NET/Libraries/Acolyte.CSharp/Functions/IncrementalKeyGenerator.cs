using Acolyte.Common;

namespace Acolyte.Functions
{
    public sealed class IncrementalKeyGenerator<TElement>
    {
        private readonly CounterInt64 _counter;


        public IncrementalKeyGenerator()
        {
            _counter = new CounterInt64();
        }

        public IncrementalKeyGenerator(
            long startValue)
        {
            _counter = new CounterInt64(startValue);
        }

        // Keep parameter to allow use this function directly in LINQ methods.
        public long GetKey(TElement _)
        {
            return _counter.ExchangeIncrement();
        }

        public long Reset()
        {
            return _counter.Reset();
        }
    }
}
