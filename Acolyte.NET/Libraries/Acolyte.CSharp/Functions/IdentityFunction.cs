using System;

namespace Acolyte.Functions
{
    public static class IdentityFunction<TElement>
    {
        public static Func<TElement, TElement> Instance { get; } = x => x;
    }
}
