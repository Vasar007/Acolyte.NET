using System;

namespace Acolyte.Common
{
    /// <inheritdoc cref="Numeric.DoubleExtensions" />
    [Obsolete("Use \"Acolyte.Numeric.DoubleExtensions.IsEqual\" instead. This class will be remove in next major version.", error: false)]
    public static class DoubleExtensions
    {
        /// <inheritdoc cref="Numeric.DoubleExtensions.IsEqual(double, double)" />
        [Obsolete("Use \"Acolyte.Numeric.DoubleExtensions.IsEqual\" instead. This class will be remove in next major version.", error: false)]
        public static bool IsEqual(this double value, double otherValue, double tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        /// <inheritdoc cref="Numeric.DoubleExtensions.IsEqual(double, double, double)" />
        [Obsolete("Use \"Acolyte.Numeric.DoubleExtensions.IsEqual\" instead. This class will be remove in next major version.", error: false)]
        public static bool IsEqual(this double value, double otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1E-9D);
        }
    }
}
