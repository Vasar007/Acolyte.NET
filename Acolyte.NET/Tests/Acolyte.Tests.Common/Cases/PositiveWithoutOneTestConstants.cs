using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases
{
    public sealed class PositiveWithoutOneTestConstants : BaseSingleTestCaseEnumerable
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public PositiveWithoutOneTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseSingleTestCaseEnumerable Overridden Methods

        protected override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                .GetValues()
                .Skip(1);
        }

        #endregion
    }
}
