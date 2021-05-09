using System.Collections.Generic;

namespace Acolyte.Tests.Cases
{
    public sealed class PositiveTestConstants : BaseSingleTestCaseEnumerator
    {
        public PositiveTestConstants()
        {
        }

        #region BaseSingleTestCaseEnumerator Overridden Methods

        internal override IEnumerable<int> GetValues()
        {
            yield return TestConstants._1;
            yield return TestConstants._2;
            yield return TestConstants._5;
            yield return TestConstants._10;
            yield return TestConstants._100;
            yield return TestConstants._10_000;
        }

        #endregion
    }
}
