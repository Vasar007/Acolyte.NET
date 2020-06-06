using System;

namespace Acolyte.Common
{
    public static class DoubleExtensions
    {
        public static bool IsEqual(this double value, double otherValue, double tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        public static bool IsEqual(this double value, double otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1E-9);
        }
    }
}
