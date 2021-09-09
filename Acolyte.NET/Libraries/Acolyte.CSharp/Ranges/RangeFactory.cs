using System.Collections.Generic;
using System.Numerics;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public static partial class RangeFactory

    {
        #region For Int32

        public static IEnumerable<int> StartsWith(int start, int count)
        {
            count.ThrowIfValueIsOutOfRange(nameof(count), 0, int.MaxValue);

            var castedStart = (long) start;
            long max = castedStart + count - 1;
            max.ThrowIfValueIsOutOfRange(nameof(count), int.MinValue, int.MaxValue);

            for (int i = 0; i < count; ++i) yield return start + i;
        }

        public static IEnumerable<int> For(int start, int end)
        {
            for (int item = start; item <= end; ++item) yield return item;
        }

        #endregion

        #region For Int64

        public static IEnumerable<long> StartsWith(long start, int count)
        {
            count.ThrowIfValueIsOutOfRange(nameof(count), 0, int.MaxValue);

            var castedStart = new BigInteger(start);
            BigInteger max = castedStart + count - 1;
            max.ThrowIfValueIsOutOfRange(nameof(count), long.MinValue, long.MaxValue);

            for (long i = 0; i < count; ++i) yield return start + i;
        }

        public static IEnumerable<long> For(long start, long end)
        {
            for (long item = start; item <= end; ++item) yield return item;
        }

        #endregion

        #region For Single

        public static IEnumerable<float> StartsWith(float start, int count)
        {
            count.ThrowIfValueIsOutOfRange(nameof(count), 0, int.MaxValue);

            var castedStart = new BigInteger(start);
            BigInteger max = castedStart + count - 1;
            max.ThrowIfValueIsOutOfRange(
                nameof(count), (BigInteger) float.MinValue, (BigInteger) float.MaxValue
            );

            for (float i = 0; i < count; ++i) yield return start + i;
        }

        public static IEnumerable<float> For(float start, float end)
        {
            for (float item = start; item <= end; ++item) yield return item;
        }

        #endregion

        #region For Double

        public static IEnumerable<double> StartsWith(double start, int count)
        {
            count.ThrowIfValueIsOutOfRange(nameof(count), 0, int.MaxValue);

            var castedStart = new BigInteger(start);
            BigInteger max = castedStart + count - 1;
            max.ThrowIfValueIsOutOfRange(
                nameof(count), (BigInteger) double.MinValue, (BigInteger) double.MaxValue
            );

            for (double i = 0; i < count; ++i) yield return start + i;
        }

        public static IEnumerable<double> For(double start, double end)
        {
            for (double item = start; item <= end; ++item) yield return item;
        }

        #endregion

        #region For Decimal

        public static IEnumerable<decimal> StartsWith(decimal start, int count)
        {
            count.ThrowIfValueIsOutOfRange(nameof(count), 0, int.MaxValue);

            var castedStart = new BigInteger(start);
            BigInteger max = castedStart + count - 1;
            max.ThrowIfValueIsOutOfRange(
                nameof(count), (BigInteger) decimal.MinValue, (BigInteger) decimal.MaxValue
            );

            for (decimal i = 0; i < count; ++i) yield return start + i;
        }

        public static IEnumerable<decimal> For(decimal start, decimal end)
        {
            for (decimal item = start; item <= end; ++item) yield return item;
        }

        #endregion
    }
}
