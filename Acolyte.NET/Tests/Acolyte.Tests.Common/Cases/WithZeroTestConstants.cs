using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Linq;

namespace Acolyte.Tests.Cases
{
    internal sealed class WithZeroTestConstants : BaseSingleTestCaseEnumerable
    {
        private readonly BaseSingleTestCaseEnumerable _originalTestCases;


        public WithZeroTestConstants(
            BaseSingleTestCaseEnumerable originalTestCases)
        {
            _originalTestCases = originalTestCases.ThrowIfNull(nameof(originalTestCases));
        }

        #region BaseSingleTestCaseEnumerable Overridden Methods

        protected override IEnumerable<int> GetValues()
        {
            return _originalTestCases
                 .GetValues()
                .PrependElement(TestConstants._0);
        }

        #endregion
    }
}
