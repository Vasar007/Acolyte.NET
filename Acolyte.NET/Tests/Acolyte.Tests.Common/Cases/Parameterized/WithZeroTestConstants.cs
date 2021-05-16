using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Linq;

namespace Acolyte.Tests.Cases.Parameterized
{
    internal sealed class WithZeroTestConstants : BaseParameterizedTestCase<int>
    {
        private readonly BaseParameterizedTestCase<int> _originalTestCases;


        public WithZeroTestConstants(
            BaseParameterizedTestCase<int> originalTestCases)
        {
            _originalTestCases = originalTestCases.ThrowIfNull(nameof(originalTestCases));
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _originalTestCases
                 .GetValues()
                .PrependElement(TestConstants._0);
        }

        #endregion
    }
}
