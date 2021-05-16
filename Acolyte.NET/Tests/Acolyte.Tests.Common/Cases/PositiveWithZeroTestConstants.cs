using System.Collections.Generic;

namespace Acolyte.Tests.Cases
{
    public sealed class PositiveWithZeroTestConstants : BaseSingleTestCaseEnumerable
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public PositiveWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new PositiveTestConstants());
        }

        #region BaseSingleTestCaseEnumerable Overridden Methods

        protected override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
