using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class PositiveWithoutOneTestConstants : BaseParameterizedTestCase<int>
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public PositiveWithoutOneTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                .GetValues()
                .Skip(1);
        }

        #endregion
    }
}
