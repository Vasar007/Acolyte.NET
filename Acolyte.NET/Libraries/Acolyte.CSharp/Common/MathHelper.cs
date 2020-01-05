using System;
using System.Linq;

namespace Acolyte.Common
{
    /// <summary>
    /// Provides a set of useful mathematic methods.
    /// </summary>
    public static class MathHelper
    {
        public static int Percent(int current, int total)
        {
            return total > 0
                ? (int) (current * 100.0 / total)
                : 0;
        }

        public static long Percent(long current, long total)
        {
            return total > 0
                ? (long) (current * 100.0 / total)
                : 0;
        }

        public static float Percent(float current, float total)
        {
            return total > 0
                ? (current * 100.0f / total)
                : 0;
        }

        public static double Percent(double current, double total)
        {
            return total > 0
                ? (current * 100.0 / total)
                : 0;
        }

        public static decimal Percent(decimal current, decimal total)
        {
            return total > 0
                ? (current * 100.0M / total)
                : 0;
        }

        public static bool IsNumeric(string str)
        {
            // Null check is provided by Enumerable.All method.
            return str.All(ch => char.IsNumber(ch));
        }

        public static double LinearInterpolation(double a, double b, double t)
        {
            return a + t * (b - a);
        }

        public static int NumberOfDigits(int value)
        {
            // TODO: calculate number of digits for negative numbers.
            if (value < 0)
            {
                throw new NotImplementedException(
                    "Cannot calculate number of digints for negative value '{value.ToString()}'."
                );
            }

            if (value == 0) return 0;

            double logOfMax = Math.Log10(value);
            int numberOfDigits = Convert.ToInt32(Math.Ceiling(logOfMax));
            return numberOfDigits;
        }

        public static decimal ToDecimalSafe(double number)
        {
            if (double.IsNaN(number))
                return 0.0M;

            if (number < (double) decimal.MinValue)
                return decimal.MinValue;

            if (number > (double) decimal.MaxValue)
                return decimal.MaxValue;

            return (decimal) number;
        }

        public static decimal NormalizePricing(decimal price)
        {
            if (price == 0.0m)
                return price;

            if (0.0m < price && price < 0.01m)
                return 0.01m;

            return price;
        }
    }
}
