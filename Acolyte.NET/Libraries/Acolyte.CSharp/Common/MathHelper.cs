using System;

namespace Acolyte.Common
{
    /// <inheritdoc cref="Numeric.MathHelper" />
    [Obsolete("Use \"Acolyte.Numeric.MathHelper\" instead. This class will be remove in next major version.", error: false)]
    public static class MathHelper
    {
        /// <inheritdoc cref="Numeric.MathHelper.Percent(int, int)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.Percent\" instead. This method will be remove in next major version.", error: false)]
        public static int Percent(int current, int total)
        {
            return Numeric.MathHelper.Percent(current, total);
        }

        /// <inheritdoc cref="Numeric.MathHelper.Percent(long, long)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.Percent\" instead. This method will be remove in next major version.", error: false)]
        public static long Percent(long current, long total)
        {
            return Numeric.MathHelper.Percent(current, total);
        }

        /// <inheritdoc cref="Numeric.MathHelper.Percent(float, float)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.Percent\" instead. This method will be remove in next major version.", error: false)]
        public static float Percent(float current, float total)
        {
            return Numeric.MathHelper.Percent(current, total);
        }

        /// <inheritdoc cref="Numeric.MathHelper.Percent(double, double)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.Percent\" instead. This method will be remove in next major version.", error: false)]
        public static double Percent(double current, double total)
        {
            return Numeric.MathHelper.Percent(current, total);
        }

        /// <inheritdoc cref="Numeric.MathHelper.Percent(decimal, decimal)" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.Percent\" instead. This method will be remove in next major version.", error: false)]
        public static decimal Percent(decimal current, decimal total)
        {
            return Numeric.MathHelper.Percent(current, total);
        }

        /// <inheritdoc cref="Numeric.MathHelper.IsNumeric" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.IsNumeric\" instead. This method will be remove in next major version.", error: false)]
        public static bool IsNumeric(string str)
        {
            return Numeric.MathHelper.IsNumeric(str);
        }

        /// <inheritdoc cref="Numeric.MathHelper.LinearInterpolation" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.LinearInterpolation\" instead. This method will be remove in next major version.", error: false)]
        public static double LinearInterpolation(double a, double b, double t)
        {
            return Numeric.MathHelper.LinearInterpolation(a, b, t);
        }

        /// <inheritdoc cref="Numeric.MathHelper.NumberOfDigits" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.NumberOfDigits\" instead. This method will be remove in next major version.", error: false)]
        public static int NumberOfDigits(int value)
        {
            return Numeric.MathHelper.NumberOfDigits(value);
        }

        /// <inheritdoc cref="Numeric.MathHelper.ToDecimalSafe" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.ToDecimalSafe\" instead. This method will be remove in next major version.", error: false)]
        public static decimal ToDecimalSafe(double number)
        {
            return Numeric.MathHelper.ToDecimalSafe(number);
        }

        /// <inheritdoc cref="Numeric.MathHelper.NormalizePricing" />
        [Obsolete("Use \"Acolyte.Numeric.DecimalExtensions.NormalizePricing\" instead. This method will be remove in next major version.", error: false)]
        public static decimal NormalizePricing(decimal price)
        {
            return Numeric.MathHelper.NormalizePricing(price);
        }
    }
}
