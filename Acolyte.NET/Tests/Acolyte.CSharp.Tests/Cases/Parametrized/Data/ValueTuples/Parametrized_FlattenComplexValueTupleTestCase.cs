using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.ValueTuples
{
    internal sealed class Parametrized_FlattenComplexValueTupleTestCase :
        BaseParameterizedTestCase<(string, bool)>
    {
        public Parametrized_FlattenComplexValueTupleTestCase()
            : base(flattenValueTuple: true)
        {
        }

        #region BaseParameterizedTestCase<(string, bool)> Overridden Methods

        public override IEnumerable<(string, bool)> GetValues()
        {
            yield return ("1098765432", true);
            yield return ("0987654321", false);
            yield return ("2109876543", false);
        }

        #endregion
    }
}
