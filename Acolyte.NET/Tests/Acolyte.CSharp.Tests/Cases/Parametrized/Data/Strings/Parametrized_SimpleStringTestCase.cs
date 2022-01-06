using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.Strings
{
    internal sealed class Parametrized_SimpleStringTestCase : BaseParameterizedTestCase<string>
    {
        public Parametrized_SimpleStringTestCase()
        {
        }

        #region BaseParameterizedTestCase<string> Overridden Methods

        public override IEnumerable<string> GetValues()
        {
            yield return string.Empty;
            yield return "1";
            yield return "12";
            yield return "123";
            yield return "1234";
            yield return "12345";
        }

        #endregion
    }
}
