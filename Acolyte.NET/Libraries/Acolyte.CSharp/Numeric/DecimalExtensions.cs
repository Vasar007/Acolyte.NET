using System;

namespace Acolyte.Numeric
{
    public static class DecimalExtensions
    {
        public static bool IsEqual(this decimal value, decimal otherValue, decimal tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        public static bool IsEqual(this decimal value, decimal otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1e-9M);
        }
    }
}
