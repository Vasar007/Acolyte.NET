using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Acolyte.Tests.Cases.Parameterized
{
    public sealed class CartesianProductOfPositiveAndNegativeWithZeroTestCases :
        BaseParameterizedTestCase<(int, int)>
    {
        private readonly PositiveWithZeroTestCases _positiveWithZeroTestConstants;
        private readonly NegativeWithZeroTestCases _negativeWithZeroTestConstants;


        public CartesianProductOfPositiveAndNegativeWithZeroTestCases()
            : base(flattenValueTuple: true)
        {
            _positiveWithZeroTestConstants = new PositiveWithZeroTestCases();
            _negativeWithZeroTestConstants = new NegativeWithZeroTestCases();
        }

        #region BaseParameterizedTestCase<(int, int)> Overridden Methods

        public override IEnumerable<(int, int)> GetValues()
        {
            IEnumerable<int> positiveWithZero = _positiveWithZeroTestConstants.GetValues();
            IEnumerable<int> negativeWithZero = _negativeWithZeroTestConstants.GetValues();
            var unitedSequence = positiveWithZero
                .Union(negativeWithZero)
                .ToArray();

            return unitedSequence.Cartesian(unitedSequence, (x, y) => (x, y));
        }

        #endregion
    }
}
