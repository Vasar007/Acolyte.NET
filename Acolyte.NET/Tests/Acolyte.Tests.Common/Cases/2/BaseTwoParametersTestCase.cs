using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.Two
{
    public abstract class BaseTwoParametersTestCase<TFirst, TSecond> : IEnumerable<object?[]>
    {
        protected BaseTwoParametersTestCase()
        {
        }

        protected internal abstract IEnumerable<(TFirst first, TSecond second)> GetValues();

        #region IEnumerable<object?[]> Implementation

        public IEnumerator<object?[]> GetEnumerator()
        {
            return GetValues()
                .Select(item => new object?[] { item.first, item.second })
                .GetEnumerator();
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
