using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Tests.Cases.Parameterized
{
    public class ParameterizedGroupedTestCase<TData> : BaseParameterizedTestCase<TData>
    {
        private readonly IReadOnlyCollection<BaseParameterizedTestCase<TData>> _testCases;

        public ParameterizedGroupedTestCase(
            BaseParameterizedTestCase<TData> testCase1,
            BaseParameterizedTestCase<TData> testCase2,
            params BaseParameterizedTestCase<TData>[] testCases)
            : base(
                testCase1.ThrowIfNull(nameof(testCase1)).FlattenValueTuple &&
                testCase2.ThrowIfNull(nameof(testCase2)).FlattenValueTuple &&
                testCases.ThrowIfNull(nameof(testCases)).All(testCase => testCase.FlattenValueTuple)
            )
        {
            // Need to check again second and third argument because condition
            // above can be executed partially because of && operator.
            testCase2.ThrowIfNull(nameof(testCase2));
            testCases.ThrowIfNull(nameof(testCases));

            _testCases = ConstructTestCases(testCase1, testCase2, testCases);
            ValidateTestCases(FlattenValueTuple, _testCases);
        }

        public ParameterizedGroupedTestCase(
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
            : base(
                testCases.ThrowIfNull(nameof(testCases)).All(testCase => testCase.FlattenValueTuple)
            )
        {
            if (testCases.Count < 2)
                throw new ArgumentException("Too few test cases to group.", nameof(testCases));

            _testCases = testCases;
            ValidateTestCases(FlattenValueTuple, _testCases);
        }

        public override IEnumerable<TData> GetValues()
        {
            return _testCases.SelectMany(testCase => testCase.GetValues());
        }

        private static IReadOnlyList<BaseParameterizedTestCase<TData>> ConstructTestCases(
            BaseParameterizedTestCase<TData> testCase1,
            BaseParameterizedTestCase<TData> testCase2,
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
        {
            var list = new List<BaseParameterizedTestCase<TData>>(testCases.Count + 2)
            {
                testCase1,
                testCase2
            };

            list.AddRange(testCases);

            return list;
        }

        private static void ValidateTestCases(bool flattenValueTuple,
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
        {
            if (testCases.Any(testCase => testCase.FlattenValueTuple != flattenValueTuple))
            {
                string message =
                    $"Failed to process test cases with mixed {nameof(FlattenValueTuple)} values.";
                throw new ArgumentException(message, nameof(testCases));
            }
        }
    }

    public static class ParameterizedGroupedTestCase
    {
        public static ParameterizedGroupedTestCase<TData> Create<TData>(
            BaseParameterizedTestCase<TData> testCase1,
            BaseParameterizedTestCase<TData> testCase2,
            params BaseParameterizedTestCase<TData>[] testCases)
        {
            return new ParameterizedGroupedTestCase<TData>(testCase1, testCase2, testCases);
        }

        public static ParameterizedGroupedTestCase<TData> Create<TData>(
            IReadOnlyCollection<BaseParameterizedTestCase<TData>> testCases)
        {
            return new ParameterizedGroupedTestCase<TData>(testCases);
        }
    }
}
