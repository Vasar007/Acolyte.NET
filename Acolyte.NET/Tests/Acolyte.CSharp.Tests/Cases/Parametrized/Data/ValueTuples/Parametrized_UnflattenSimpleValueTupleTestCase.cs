using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.ValueTuples
{
    internal sealed class Parametrized_UnflattenSimpleValueTupleTestCase :
        BaseParameterizedTestCase<(string, bool)>
    {
        public Parametrized_UnflattenSimpleValueTupleTestCase()
            : base(flattenValueTuple: false)
        {
        }

        #region BaseParameterizedTestCase<(string, bool)> Overridden Methods

        public override IEnumerable<(string, bool)> GetValues()
        {
            yield return (string.Empty, true);
            yield return ("1", false);
            yield return ("12", true);
            yield return ("123", false);
        }

        #endregion
    }
}
