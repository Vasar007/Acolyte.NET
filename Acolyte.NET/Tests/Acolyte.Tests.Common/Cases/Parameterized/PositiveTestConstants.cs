using System.Collections.Generic;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class PositiveTestConstants : BaseParameterizedTestCase<int>
    {
        public PositiveTestConstants()
        {
        }

        #region BaseParameterizedTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
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
