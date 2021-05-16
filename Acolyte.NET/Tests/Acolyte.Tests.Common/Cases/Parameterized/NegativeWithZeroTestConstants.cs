using System.Collections.Generic;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class NegativeWithZeroTestConstants : BaseParameterizedTestCase<int>
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public NegativeWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new NegativeTestConstants());
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
