using System.Collections.Generic;
using Acolyte.Tests.Cases.Parameterized;

namespace Acolyte.Tests.Cases.Parametrized.Data.ValueTuples
{
    internal sealed class Parametrized_UnflattenComplexValueTupleTestCase :
        BaseParameterizedTestCase<(string, bool)>
    {
        public Parametrized_UnflattenComplexValueTupleTestCase()
            : base(flattenValueTuple: false)
        {
        }

        #region BaseParameterizedTestCase<(string, bool)> Overridden Methods

        public override IEnumerable<(string, bool)> GetValues()
        {
            yield return ("0000000000", true);
            yield return ("1234567890", false);
            yield return ("2345678901", false);
            yield return ("3456789012", false);
        }

        #endregion
    }
}
