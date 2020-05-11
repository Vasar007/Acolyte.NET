using System;

namespace Acolyte.Tests
{
    internal class IdentityFunction<TElement>
    {
        public static Func<TElement, TElement> Instance => x => x;
    }
}
