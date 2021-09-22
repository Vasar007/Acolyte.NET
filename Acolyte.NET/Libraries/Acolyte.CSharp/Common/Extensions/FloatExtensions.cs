using System;

namespace Acolyte.Common
{
    /// <inheritdoc cref="Numeric.FloatExtensions" />
    [Obsolete("Use \"Acolyte.Numeric.FloatExtensions\" instead. This class will be remove in next major version.", error: false)]
    public static class FloatExtensions
    {
        /// <inheritdoc cref="Numeric.FloatExtensions.IsEqual(float, float)" />
        [Obsolete("Use \"Acolyte.Numeric.FloatExtensions.IsEqual\" instead. This method will be remove in next major version.", error: false)]
        public static bool IsEqual(this float value, float otherValue, float tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        /// <inheritdoc cref="Numeric.FloatExtensions.IsEqual(float, float, float)" />
        [Obsolete("Use \"Acolyte.Numeric.FloatExtensions.IsEqual\" instead. This method will be remove in next major version.", error: false)]
        public static bool IsEqual(this float value, float otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1E-6F);
        }
    }
}
