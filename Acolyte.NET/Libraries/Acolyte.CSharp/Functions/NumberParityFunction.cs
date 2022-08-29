using System;
using Acolyte.Basic.Randomness;

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
                : null;
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
                : null;
        }

        #endregion

        #region Helpers

        public static T? ReturnNullValueIfRandomInt32IsOdd<T>(T value)
            where T : class
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : null;
        }

        public static T? ReturnNullableIfRandomInt32IsOdd<T>(T value)
            where T : struct
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : null;
        }

        /// <inheritdoc cref="ReturnNullableIfRandomInt32IsOdd{T}(T)" />
        [Obsolete("Use \"Acolyte.Functions.NumberParityFunction.ReturnNullableIfRandomInt32IsOdd\" instead. This method will be removed in next major version.", error: false)]
        public static T? ReturnNullIfRandomInt32IsOdd<T>(T value)
            where T : struct
        {
            // Convert values to null if randomly created Int32 is odd.
            int mark = StaticRandom.Next();
            return IsEven(mark)
                ? value
                : null;
        }

        #endregion
    }
}
