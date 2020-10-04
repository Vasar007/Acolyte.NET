using System;

namespace Acolyte.Collections.Concurrent
{
    [Flags]
    public enum TwoWayDictionaryAddFlags
    {
        None = 0,
        Safe = 1,
        Blocking = 2,
        BlockingSafe = Blocking | Safe
    }
}
