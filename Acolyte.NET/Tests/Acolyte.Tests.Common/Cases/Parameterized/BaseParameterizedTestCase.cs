using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Common;
using Acolyte.Reflection;

namespace Acolyte.Tests.Cases.Parameterized
{
    public abstract class BaseParameterizedTestCase<TData> : IEnumerable<object?[]>
    {
        private readonly bool _flattenValueTuple;


        protected BaseParameterizedTestCase(
            bool flattenValueTuple)
        {
            _flattenValueTuple = flattenValueTuple;
        }

        protected BaseParameterizedTestCase()
            : this(flattenValueTuple: false)
        {
        }

        protected internal abstract IEnumerable<TData> GetValues();

        #region IEnumerable<object?[]> Implementation

        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerable<TData> values = GetValues();

            // Special case: unwrap ValueTuple and create object array with arguments.
            bool shouldFlattenValueTuple =
                _flattenValueTuple &&
                typeof(TData).IsValueTupleType();
            if (shouldFlattenValueTuple)
            {
                return values
                    .Select(value => value!.GetPublicInstanceFields().ToArray())
                    .GetEnumerator();
            }

            return values
                .Select(value => new object?[] { value })
                .GetEnumerator();
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
