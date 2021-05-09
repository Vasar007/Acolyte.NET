using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases
{
    public sealed class PositiveWithoutOneTestConstants : BaseSingleTestCaseEnumerator
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public PositiveWithoutOneTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                .GetValues()
                .Skip(1);
        }

        #endregion
    }
}
