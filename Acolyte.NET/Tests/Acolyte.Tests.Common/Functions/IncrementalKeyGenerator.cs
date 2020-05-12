using System.Threading;

namespace Acolyte.Tests.Functions
{
    public sealed class IncrementalKeyGenerator<TElement>
    {
        private long _counter;

        public IncrementalKeyGenerator()
        {
            _counter = 0;
        }

        // Keep parameter to allow use this function directly in LINQ methods.
        public long GetKey(TElement _)
        {
            return Interlocked.Increment(ref _counter);
        }

        public long Reset()
        {
            return Interlocked.Exchange(ref _counter, 0);
        }
    }
}
