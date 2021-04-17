using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public sealed class LoopedRange<T> : IEnumerable<T?>
    {
        private readonly IEnumerable<T> _range;

        private readonly bool _isInfinite;


        public LoopedRange(
            IEnumerable<T> range,
            bool isInfinite)
        {
            _range = range.ThrowIfNull(nameof(range));
            _isInfinite = isInfinite;
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            return LoopedEnumerator.Create(_range, _isInfinite);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public static class LoopedRange
    {
        public static LoopedRange<T> From<T>(
            IEnumerable<T> range,
            bool isInfinite)
        {
            return new LoopedRange<T>(
                range: range,
                isInfinite: isInfinite
            );
        }

        public static LoopedRange<T> From<T>(
            IEnumerable<T> range)
        {
            return From(range, isInfinite: false);
        }

        #region Creation Methods For Int32

        public static LoopedRange<int> From(
            int start,
            int end,
            bool isInfinite)
        {
            return From(Range.For(start, end), isInfinite);
        }

        public static LoopedRange<int> From(
            int start,
            int end)
        {
            return From(start, end, isInfinite: false);
        }

        public static LoopedRange<int> StartsWith(
            int start,
            int count,
            bool isInfinite)
        {
            return From(Range.StartsWith(start, count), isInfinite);
        }

        public static LoopedRange<int> StartsWith(
            int start,
            int count)
        {
            return StartsWith(start, count, isInfinite: false);
        }

        #endregion

        #region Creation Methods For Int64

        public static LoopedRange<long> From(
            long start,
            long end,
            bool isInfinite)
        {
            return From(Range.For(start, end), isInfinite);
        }

        public static LoopedRange<long> From(
            long start,
            long end)
        {
            return From(start, end, isInfinite: false);
        }

        public static LoopedRange<long> StartsWith(
            long start,
            int count,
            bool isInfinite)
        {
            return From(Range.StartsWith(start, count), isInfinite);
        }

        public static LoopedRange<long> StartsWith(
            long start,
            int count)
        {
            return StartsWith(start, count, isInfinite: false);
        }

        #endregion

        #region Creation Methods For Single

        public static LoopedRange<float> From(
            float start,
            float end,
            bool isInfinite)
        {
            return From(Range.For(start, end), isInfinite);
        }

        public static LoopedRange<float> From(
            float start,
            float end)
        {
            return From(start, end, isInfinite: false);
        }

        public static LoopedRange<float> StartsWith(
            float start,
            int count,
            bool isInfinite)
        {
            return From(Range.StartsWith(start, count), isInfinite);
        }

        public static LoopedRange<float> StartsWith(
            float start,
            int count)
        {
            return StartsWith(start, count, isInfinite: false);
        }

        #endregion

        #region Creation Methods For Double

        public static LoopedRange<double> From(
            double start,
            double end,
            bool isInfinite)
        {
            return From(Range.For(start, end), isInfinite);
        }

        public static LoopedRange<double> From(
            double start,
            double end)
        {
            return From(start, end, isInfinite: false);
        }

        public static LoopedRange<double> StartsWith(
            double start,
            int count,
            bool isInfinite)
        {
            return From(Range.StartsWith(start, count), isInfinite);
        }

        public static LoopedRange<double> StartsWith(
            double start,
            int count)
        {
            return StartsWith(start, count, isInfinite: false);
        }

        #endregion

        #region Creation Methods For Decimal

        public static LoopedRange<decimal> From(
            decimal start,
            decimal end,
            bool isInfinite)
        {
            return From(Range.For(start, end), isInfinite);
        }

        public static LoopedRange<decimal> From(
            decimal start,
            decimal end)
        {
            return From(start, end, isInfinite: false);
        }

        public static LoopedRange<decimal> StartsWith(
            decimal start,
            int count,
            bool isInfinite)
        {
            return From(Range.StartsWith(start, count), isInfinite);
        }

        public static LoopedRange<decimal> StartsWith(
            decimal start,
            int count)
        {
            return StartsWith(start, count, isInfinite: false);
        }

        #endregion
    }
}
