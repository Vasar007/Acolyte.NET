using System.Collections.Generic;

namespace Acolyte.Tests.Cases.One
{
    public sealed class NegativeWithZeroTestConstants : BaseOneParameterTestCase<int>
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public NegativeWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new NegativeTestConstants());
        }

        #region BaseOneParameterTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
