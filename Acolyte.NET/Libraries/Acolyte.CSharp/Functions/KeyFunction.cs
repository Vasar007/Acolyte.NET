using System;

namespace Acolyte.Functions
{
    public static class KeyFunction<TElement>
    {
        // Keep parameter to allow use this function directly in LINQ methods.
        public static Func<TElement, Guid> Simple { get; } = _ => Guid.NewGuid();
    }
}
