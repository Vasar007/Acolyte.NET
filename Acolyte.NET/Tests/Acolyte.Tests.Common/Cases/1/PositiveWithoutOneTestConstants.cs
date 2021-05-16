using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.One
{
    public sealed class PositiveWithoutOneTestConstants : BaseOneParameterTestCase<int>
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public PositiveWithoutOneTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseOneParameterTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                .GetValues()
                .Skip(1);
        }

        #endregion
    }
}
