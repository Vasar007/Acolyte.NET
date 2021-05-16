using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases
{
    public abstract class BaseSingleTestCaseEnumerable : IEnumerable<object[]>
    {
        protected BaseSingleTestCaseEnumerable()
        {
        }

        protected abstract IEnumerable<int> GetValues();

        #region IEnumerable<object[]> Implementation

        public IEnumerator<object[]> GetEnumerator()
        {
            return GetValues()
                .Select(item => new object[] { item })
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
