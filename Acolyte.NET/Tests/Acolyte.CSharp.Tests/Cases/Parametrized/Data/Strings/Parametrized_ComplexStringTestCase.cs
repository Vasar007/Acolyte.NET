using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.Strings
{
    internal sealed class Parametrized_ComplexStringTestCase : BaseParameterizedTestCase<string>
    {
        public Parametrized_ComplexStringTestCase()
        {
        }

        #region BaseParameterizedTestCase<string> Overridden Methods

        public override IEnumerable<string> GetValues()
        {
            yield return "qwertyuiop[]asdfghjkl;'zxcvbnm,./";
            yield return "йцукенгшщзхъфывапролджэячсмитьбю.";
            yield return "qwertzuiopú)asdfghjklů§yxcvbnm,.-";
        }

        #endregion
    }
}
