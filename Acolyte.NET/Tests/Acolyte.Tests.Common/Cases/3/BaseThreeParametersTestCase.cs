using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.One
{
    public abstract class BaseThreeParametersTestCase<TFirst, TSecond, TThird> :
        IEnumerable<object?[]>
    {
        protected BaseThreeParametersTestCase()
        {
        }

        protected internal abstract IEnumerable<(TFirst first, TSecond second, TThird third)> GetValues();

        #region IEnumerable<object?[]> Implementation

        public IEnumerator<object?[]> GetEnumerator()
        {
            return GetValues()
                .Select(item => new object?[] { item.first, item.second, item.third })
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
