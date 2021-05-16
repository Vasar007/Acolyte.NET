using System.Collections.Generic;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class PositiveWithZeroTestConstants : BaseParameterizedTestCase<int>
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public PositiveWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new PositiveTestConstants());
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
