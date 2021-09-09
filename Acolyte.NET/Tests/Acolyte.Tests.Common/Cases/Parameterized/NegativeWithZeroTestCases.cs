using System.Collections.Generic;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class NegativeWithZeroTestCases : BaseParameterizedTestCase<int>
    {
        private readonly WithZeroTestCases _withZeroTestCases;


        public NegativeWithZeroTestCases()
        {
            _withZeroTestCases = new WithZeroTestCases(new NegativeTestCases());
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        public override IEnumerable<int> GetValues()
        {
            return _withZeroTestCases.GetValues();
        }

        #endregion
    }
}
