using System.Collections.Generic;

namespace Acolyte.Tests.Cases
{
    public sealed class NegativeWithZeroTestConstants : BaseSingleTestCaseEnumerator
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public NegativeWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new NegativeTestConstants());
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
