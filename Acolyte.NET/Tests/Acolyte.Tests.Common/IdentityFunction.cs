using System;

namespace Acolyte.Tests
{
    public class IdentityFunction<TElement>
    {
        public static Func<TElement, TElement> Instance => x => x;
    }
}
