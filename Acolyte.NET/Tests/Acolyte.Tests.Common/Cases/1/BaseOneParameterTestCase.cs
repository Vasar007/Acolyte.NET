using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.One
{
    public abstract class BaseOneParameterTestCase<TFirst> : IEnumerable<object?[]>
    {
        protected BaseOneParameterTestCase()
        {
        }

        protected internal abstract IEnumerable<TFirst> GetValues();

        #region IEnumerable<object?[]> Implementation

        public IEnumerator<object?[]> GetEnumerator()
        {
            return GetValues()
                .Select(item => new object?[] { item })
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
