using System;

namespace Acolyte.Common
{
    public static class FloatExtensions
    {
        public static bool IsEqual(this float value, float otherValue, float tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        public static bool IsEqual(this float value, float otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1E-6F);
        }
    }
}
