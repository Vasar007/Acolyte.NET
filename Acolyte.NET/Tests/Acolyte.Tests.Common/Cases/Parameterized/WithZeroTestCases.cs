using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Tests.Cases.Parameterized
{
    internal sealed class WithZeroTestCases : BaseParameterizedTestCase<int>
    {
        private readonly BaseParameterizedTestCase<int> _originalTestCases;


        public WithZeroTestCases(
            BaseParameterizedTestCase<int> originalTestCases)
        {
            _originalTestCases = originalTestCases.ThrowIfNull(nameof(originalTestCases));
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        public override IEnumerable<int> GetValues()
        {
            return _originalTestCases
                .GetValues()
                .Prepend(TestConstants._0);
        }

        #endregion
    }
}
