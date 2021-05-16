using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases
{
    public sealed class NegativeTestConstants : BaseSingleTestCaseEnumerable
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public NegativeTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseSingleTestCaseEnumerable Overridden Methods

        protected override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                 .GetValues()
                 .Select(item => -item);
        }

        #endregion
    }
}
