using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class NegativeTestConstants : BaseParameterizedTestCase<int>
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public NegativeTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                 .GetValues()
                 .Select(item => -item);
        }

        #endregion
    }
}
