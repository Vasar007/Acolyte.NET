using System.Collections.Generic;

namespace Acolyte.Tests.Cases.One
{
    public sealed class PositiveWithZeroTestConstants : BaseOneParameterTestCase<int>
    {
        private readonly WithZeroTestConstants _withZeroTestConstants;


        public PositiveWithZeroTestConstants()
        {
            _withZeroTestConstants = new WithZeroTestConstants(new PositiveTestConstants());
        }

        #region BaseOneParameterTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _withZeroTestConstants.GetValues();
        }

        #endregion
    }
}
