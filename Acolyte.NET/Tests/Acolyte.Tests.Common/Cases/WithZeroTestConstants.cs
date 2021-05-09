using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Linq;

namespace Acolyte.Tests.Cases
{
    internal sealed class WithZeroTestConstants : BaseSingleTestCaseEnumerator
    {
        private readonly BaseSingleTestCaseEnumerator _originalTestCases;


        public WithZeroTestConstants(
            BaseSingleTestCaseEnumerator originalTestCases)
        {
            _originalTestCases = originalTestCases.ThrowIfNull(nameof(originalTestCases));
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            return _originalTestCases
                 .GetValues()
                .PrependElement(TestConstants._0);
        }

        #endregion
    }
}
