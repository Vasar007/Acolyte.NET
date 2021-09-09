using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Ranges
{
    public sealed class LoopedEnumerable<T> : IEnumerable<T?>
    {
        private readonly IEnumerable<T> _range;

        public LoopedEnumerable(
            IEnumerable<T> range)
        {
            _range = range.ThrowIfNull(nameof(range));
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            return LoopedEnumerator.Create(_range);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public static class LoopedEnumerable
    {
        public static LoopedEnumerable<T> From<T>(
            IEnumerable<T> range)
        {
            return new LoopedEnumerable<T>(
                range: range
            );
        }
    }
}
