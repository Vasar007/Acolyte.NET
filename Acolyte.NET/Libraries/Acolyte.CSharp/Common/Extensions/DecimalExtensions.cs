using System;

namespace Acolyte.Common
{
    /// <inheritdoc cref="Numeric.DecimalExtensions" />
    [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions\" instead. This class will be remove in next major version.", error: false)]
    public static class DecimalExtensions
    {
        /// <inheritdoc cref="Numeric.DecimalExtensions.IsEqual(decimal, decimal)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.IsEqual\" instead. This method will be remove in next major version.", error: false)]
        public static bool IsEqual(this decimal value, decimal otherValue, decimal tolerance)
        {
            return Math.Abs(value - otherValue) < tolerance;
        }

        /// <inheritdoc cref="Numeric.DecimalExtensions.IsEqual(decimal, decimal, decimal)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.IsEqual\" instead. This method will be remove in next major version.", error: false)]
        public static bool IsEqual(this decimal value, decimal otherValue)
        {
            return IsEqual(value, otherValue, tolerance: 1e-9M);
        }
    }
}
