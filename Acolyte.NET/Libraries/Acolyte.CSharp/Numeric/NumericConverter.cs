using System;

namespace Acolyte.Numeric
{
    public static class NumericConverter
    {
        public static int ConvertToInt32Safe(uint value)
        {
            return ConvertToInt32Safe(value, defaultValue: int.MaxValue);
        }

        public static int ConvertToInt32Safe(uint value, int defaultValue)
        {
            return ConvertInternalSafe(value, x => Convert.ToInt32(x), defaultValue);
        }

        public static int ConvertToInt32Safe(ulong value)
        {
            return ConvertToInt32Safe(value, defaultValue: int.MaxValue);
        }

        public static int ConvertToInt32Safe(ulong value, int defaultValue)
        {
            return ConvertInternalSafe(value, x => Convert.ToInt32(x), defaultValue);
        }

        public static long ConvertToInt64Safe(ulong value)
        {
            return ConvertToInt64Safe(value, defaultValue: long.MaxValue);
        }

        public static long ConvertToInt64Safe(ulong value, long defaultValue)
        {
            return ConvertInternalSafe(value, x => Convert.ToInt64(x), defaultValue);
        }

        private static TOut ConvertInternalSafe<TIn, TOut>(TIn value, Func<TIn, TOut> conversion, TOut defaultValue)
            where TIn : struct
        {
            try
            {
                return conversion(value);
            }
            catch (OverflowException)
            {
                return defaultValue;
            }
        }
    }
}
