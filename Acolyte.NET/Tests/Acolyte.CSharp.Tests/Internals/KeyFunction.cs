using System;

namespace Acolyte.Tests
{
    internal class KeyFunction<TElement>
    {
        // Keep parameter to allow use this function directly in LINQ methods.
        public static Func<TElement, Guid> Simple => _ => Guid.NewGuid();
    }
}
