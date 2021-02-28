using System;

namespace Acolyte.Tests.Functions
{
    public static class MultiplyFunction
    {
        public static Func<int, int> RedoubleInt32 { get; } =
            x => { unchecked { return x * 2; } };
        public static Func<int?, int?> RedoubleNullableInt32 { get; } =
            x => { unchecked { return x * 2; } };

        public static Func<long, long> RedoubleInt64 { get; } =
            x => { unchecked { return x * 2L; } };
        public static Func<long?, long?> RedoubleNullableInt64 { get; } =
            x => { unchecked { return x * 2L; } };

        public static Func<float, float> RedoubleSingle { get; } =
            x => { unchecked { return x * 2.0F; } };
        public static Func<float?, float?> RedoubleNullableSingle { get; } =
            x => { unchecked { return x * 2.0F; } };

        public static Func<double, double> RedoubleDouble { get; } =
            x => { unchecked { return x * 2.0; } };
        public static Func<double?, double?> RedoubleNullableDouble { get; } =
            x => { unchecked { return x * 2.0; } };

        public static Func<decimal, decimal> RedoubleDecimal { get; } =
            x =>
            {
                // As decimal is not primitive type and has overloads for operators,
                // unchecked does not work.
                // So, I catch exception and return original value if something goes wrong.
                try
                {
                    return x * 2.0M;
                }
                catch (OverflowException)
                {
                    return x;
                }
            };
        public static Func<decimal?, decimal?> RedoubleNullableDecimal { get; } =
        x =>
        {
            if (!x.HasValue) return null;

            return RedoubleDecimal(x.Value);
        };
    }
}
