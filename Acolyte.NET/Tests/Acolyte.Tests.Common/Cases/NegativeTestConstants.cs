using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases
{
    public sealed class NegativeTestConstants : BaseSingleTestCaseEnumerator
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public NegativeTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                 .GetValues()
                 .Select(item => -item);
        }

        #endregion
    }
}
