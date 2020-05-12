using System;

namespace Acolyte.Tests.Functions
{
    public static class IdentityFunction<TElement>
    {
        public static Func<TElement, TElement> Instance { get; } = x => x;
    }
}
