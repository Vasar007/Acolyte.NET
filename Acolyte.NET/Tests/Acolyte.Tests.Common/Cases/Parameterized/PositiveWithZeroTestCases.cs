using System.Collections.Generic;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class PositiveWithZeroTestCases : BaseParameterizedTestCase<int>
    {
        private readonly WithZeroTestCases _withZeroTestCases;


        public PositiveWithZeroTestCases()
        {
            _withZeroTestCases = new WithZeroTestCases(new PositiveTestCases());
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        public override IEnumerable<int> GetValues()
        {
            return _withZeroTestCases.GetValues();
        }

        #endregion
    }
}
