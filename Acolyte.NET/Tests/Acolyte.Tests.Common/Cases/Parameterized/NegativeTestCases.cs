using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class NegativeTestCases : BaseParameterizedTestCase<int>
    {
        private readonly PositiveTestCases _positiveTestConstants;


        public NegativeTestCases()
        {
            _positiveTestConstants = new PositiveTestCases();
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        public override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                 .GetValues()
                 .Select(item => -item);
        }

        #endregion
    }
}
