using System.Collections.Generic;

namespace Acolyte.Tests.Cases
{
    public sealed class NegativeWithZeroTestConstants : BaseSingleTestCaseEnumerable
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public NegativeWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new NegativeTestConstants());
        }

        #region BaseSingleTestCaseEnumerable Overridden Methods

        protected override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
