using System;

namespace Acolyte.Common
{
    public static class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();

        [ThreadStatic]
        private static Random? _local;

        public static int Next()
        {
            if (_local is null)
            {
                lock (_global)
                {
                    if (_local is null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next();
        }
    }
}
