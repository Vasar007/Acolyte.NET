using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Cases.One
{
    public sealed class NegativeTestConstants : BaseOneParameterTestCase<int>
    {
        private readonly PositiveTestConstants _positiveTestConstants;


        public NegativeTestConstants()
        {
            _positiveTestConstants = new PositiveTestConstants();
        }

        #region BaseOneParameterTestCase<int> Overridden Methods

        protected internal override IEnumerable<int> GetValues()
        {
            return _positiveTestConstants
                 .GetValues()
                 .Select(item => -item);
        }

        #endregion
    }
}
