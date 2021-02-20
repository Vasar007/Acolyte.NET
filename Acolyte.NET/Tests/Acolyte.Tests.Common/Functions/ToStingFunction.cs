using System;

namespace Acolyte.Tests.Functions
{
    public static class ToStingFunction<TElement>
    {
        public static Func<TElement, string> Simple { get; }
            = x => x is null ? string.Empty : x.ToString();

        public static Func<TElement, string> WithQuotes { get; }
            = x => x is null ? string.Empty : $"'{x.ToString()}'";
    }
}
