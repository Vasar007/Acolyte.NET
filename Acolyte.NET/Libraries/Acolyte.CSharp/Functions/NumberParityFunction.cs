using Acolyte.Data.Randomness;

namespace Acolyte.Functions
{
    public static class NumberParityFunction
    {
        #region Int32

        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }

        public static int? ReturnNullIfOdd(int value)
        {
            // Convert all odd values to null.
            return IsEven(value)
                ? value
                : (int?) null;
        }

        #endregion

        #region Int64

        public static bool IsEven(long value)
        {
            return (value & 1) == 0;
        }

        public static long? ReturnNullIfOdd(long value)
        {
            // Convert all odd values to null.
            return IsEven(value)
                ? value
                : (long?) null;
        }

        #endregion

        #region Single

        public static float? ReturnNullIfRandomInt32IsOdd(float value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : (float?) null;
        }

        #endregion

        #region Double

        public static double? ReturnNullIfRandomInt32IsOdd(double value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : (double?) null;
        }

        #endregion

        #region Decimal

        public static decimal? ReturnNullIfRandomInt32IsOdd(decimal value)
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : (decimal?) null;
        }

        #endregion
    }
}
