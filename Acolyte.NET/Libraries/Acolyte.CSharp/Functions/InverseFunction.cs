using System;

namespace Acolyte.Functions
{
    public static class InverseFunction
    {
        public static Func<int, int> Int32 { get; } = x => -x;
        public static Func<long, long> Int64 { get; } = x => -x;
        public static Func<float, float> Single { get; } = x => -x;
        public static Func<double, double> Double { get; } = x => -x;
        public static Func<decimal, decimal> Decimal { get; } = x => -x;
    }
}
