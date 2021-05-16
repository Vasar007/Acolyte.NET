using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Linq;

namespace Acolyte.Tests.Cases.One
{
    internal sealed class WithZeroTestConstants : BaseOneParameterTestCase<int>
    {
        private readonly BaseOneParameterTestCase<int> _originalTestCases;


        public WithZeroTestConstants(
            BaseOneParameterTestCase<int> originalTestCases)
        {
            _originalTestCases = originalTestCases.ThrowIfNull(nameof(originalTestCases));
        }

        #region BaseOneParameterTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _originalTestCases
                 .GetValues()
                .PrependElement(TestConstants._0);
        }

        #endregion
    }
}
