using System;
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

        public abstract IEnumerable<TData> GetValues();

        #region IEnumerable<object?[]> Implementation

        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerable<TData> values = GetValues();

            // Special case: unwrap ValueTuple and create object array with arguments.
            if (_flattenValueTuple)
            {
                Type dataType = typeof(TData);
                bool shouldFlattenValueTuple = dataType.IsValueTupleType();
                if (!shouldFlattenValueTuple)
                {
                    string message =
                        $"Failed to flatten values because type " +
                        $"[{dataType.FullName}] is not value tuple.";
                    throw new InvalidOperationException(message);
                }

                return values
                    .Select(value => value!.EnumeratePublicInstanceFields().ToArray())
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
