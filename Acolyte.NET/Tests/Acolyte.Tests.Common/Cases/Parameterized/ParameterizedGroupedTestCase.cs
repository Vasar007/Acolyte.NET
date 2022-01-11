using System.Collections.Generic;
using Acolyte.Functions;

namespace Acolyte.Tests.Cases.Parameterized
{
    public class ParameterizedGroupedTestCase<TData> :
        ParameterizedGroupedTestCaseWithSelector<TData, TData>
    {
        public ParameterizedGroupedTestCase(
            BaseParameterizedTestCase<TData> testCase1,
            params BaseParameterizedTestCase<TData>[] testCases)
            : base(IdentityFunction<TData>.Instance, testCase1, testCases)
        {
        }

        public ParameterizedGroupedTestCase(
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
            : base(IdentityFunction<TData>.Instance, testCases)
        {
        }
    }

    public static class ParameterizedGroupedTestCase
    {
        public static ParameterizedGroupedTestCase<TData> Create<TData>(
            BaseParameterizedTestCase<TData> testCase1,
            params BaseParameterizedTestCase<TData>[] testCases)
        {
            return new ParameterizedGroupedTestCase<TData>(testCase1, testCases);
        }

        public static ParameterizedGroupedTestCase<TData> Create<TData>(
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
        {
            return new ParameterizedGroupedTestCase<TData>(testCases);
        }
    }
}
