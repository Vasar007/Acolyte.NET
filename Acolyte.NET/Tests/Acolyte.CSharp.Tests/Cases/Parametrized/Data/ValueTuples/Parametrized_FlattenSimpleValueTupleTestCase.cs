using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.ValueTuples
{
    internal sealed class Parametrized_FlattenSimpleValueTupleTestCase :
        BaseParameterizedTestCase<(string, bool)>
    {
        public Parametrized_FlattenSimpleValueTupleTestCase()
            : base(flattenValueTuple: true)
        {
        }

        #region BaseParameterizedTestCase<(string, bool)> Overridden Methods

        public override IEnumerable<(string, bool)> GetValues()
        {
            yield return ("2", false);
            yield return ("1", true);
            yield return ("3", true);
        }

        #endregion
    }
}
