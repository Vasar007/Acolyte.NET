using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public sealed class RangeEnumerable<T> : IEnumerable<T?>
    {
        // TODO: accept configuration instead of raw parameters.

        private readonly IEnumerable<T> _range;

        public bool IsInfinite { get; }


        public RangeEnumerable(
            IEnumerable<T> range,
            bool isInfinite)
        {
            _range = range.ThrowIfNull(nameof(range));
            IsInfinite = isInfinite;
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            return RangeEnumerator.Create(_range, IsInfinite);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public static class RangeEnumerable
    {
        public static RangeEnumerable<T> From<T>(
            IEnumerable<T> range,
            bool isInfinite)
        {
            return new RangeEnumerable<T>(
                range: range,
                isInfinite: isInfinite
            );
        }
    }
}
