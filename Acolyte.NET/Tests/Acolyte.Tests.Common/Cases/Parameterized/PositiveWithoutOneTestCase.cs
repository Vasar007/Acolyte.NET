using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class PositiveWithoutOneTestCase : BaseParameterizedTestCase<int>
    {
        private readonly PositiveTestCases _positiveTestConstants;


        public PositiveWithoutOneTestCase()
        {
            _positiveTestConstants = new PositiveTestCases();
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        public override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                .GetValues()
                .Skip(1);
        }

        #endregion
    }
}
