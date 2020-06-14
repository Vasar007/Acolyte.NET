using System;

namespace Acolyte.Tests.Functions
{
    public static class MultiplyFunction
    {
        public static Func<int, int> RedoubleInt32 { get; } = x => x * 2;
        public static Func<int?, int?> RedoubleNullableInt32 { get; } = x => x * 2;

        public static Func<long, long> RedoubleInt64 { get; } = x => x * 2;
        public static Func<long?, long?> RedoubleNullableInt64 { get; } = x => x * 2;

        public static Func<float, float> RedoubleSingle { get; } = x => x * 2;
        public static Func<float?, float?> RedoubleNullableSingle { get; } = x => x * 2;

        public static Func<double, double> RedoubleDouble { get; } = x => x * 2;
        public static Func<double?, double?> RedoubleNullableDouble { get; } = x => x * 2;

        public static Func<decimal, decimal> RedoubleDecimal { get; } = x => x * 2;
        public static Func<decimal?, decimal?> RedoubleNullableDecimal { get; } = x => x * 2;
    }
}
