using System.Collections.Generic;

namespace Acolyte.Tests.Cases
{
    public sealed class PositiveWithZeroTestConstants : BaseSingleTestCaseEnumerator
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public PositiveWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new PositiveTestConstants());
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
