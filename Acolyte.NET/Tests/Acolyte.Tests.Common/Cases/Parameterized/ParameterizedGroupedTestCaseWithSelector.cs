using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Tests.Cases.Parameterized
{
    public class ParameterizedGroupedTestCaseWithSelector<TSource, TResult> :
        BaseParameterizedTestCase<TResult>
    {
        protected readonly IReadOnlyCollection<BaseParameterizedTestCase<TSource>> _testCases;
        protected readonly Func<TSource, TResult> _selector;

        public ParameterizedGroupedTestCaseWithSelector(
            Func<TSource, TResult> selector,
            BaseParameterizedTestCase<TSource> testCase1,
            params BaseParameterizedTestCase<TSource>[] testCases)
            : base(
                testCase1.ThrowIfNull(nameof(testCase1)).FlattenValueTuple &&
                testCases.ThrowIfNull(nameof(testCases)).All(testCase => testCase.FlattenValueTuple)
            )
        {
            // Need to check again because condition above can be
            // executed partially because of && operator.
            testCases.ThrowIfNull(nameof(testCases));

            _selector = selector.ThrowIfNull(nameof(selector));
            _testCases = ConstructTestCases(testCase1, testCases);
            ValidateTestCases(FlattenValueTuple, _testCases);
        }

        public ParameterizedGroupedTestCaseWithSelector(
            Func<TSource, TResult> selector,
            IReadOnlyCollection<BaseParameterizedTestCase<TSource>> testCases)
            : base(
                testCases.ThrowIfNull(nameof(testCases)).All(testCase => testCase.FlattenValueTuple)
            )
        {
            _selector = selector.ThrowIfNull(nameof(selector));
            _testCases = testCases;
            ValidateTestCases(FlattenValueTuple, _testCases);
        }

        public override IEnumerable<TResult> GetValues()
        {
            return _testCases.SelectMany(testCase => testCase.GetValues().Select(_selector));
        }

        private static IReadOnlyList<BaseParameterizedTestCase<TSource>> ConstructTestCases(
            BaseParameterizedTestCase<TSource> testCase1,
            IReadOnlyCollection<BaseParameterizedTestCase<TSource>> testCases)
        {
            var list = new List<BaseParameterizedTestCase<TSource>>(testCases.Count + 1)
            {
                testCase1
            };

            list.AddRange(testCases);

            return list;
        }

        private static void ValidateTestCases(bool flattenValueTuple,
            IReadOnlyCollection<BaseParameterizedTestCase<TSource>> testCases)
        {
            if (testCases.Any(testCase => testCase.FlattenValueTuple != flattenValueTuple))
            {
                string message =
                    $"Failed to process test cases with mixed {nameof(FlattenValueTuple)} values.";
                throw new ArgumentException(message, nameof(testCases));
            }
        }
    }

    public static class ParameterizedGroupedTestCaseWithSelector
    {
        public static ParameterizedGroupedTestCaseWithSelector<TSource, TResult> Create<TSource, TResult>(
             Func<TSource, TResult> selector,
             BaseParameterizedTestCase<TSource> testCase1,
             params BaseParameterizedTestCase<TSource>[] testCases)
        {
            return new ParameterizedGroupedTestCaseWithSelector<TSource, TResult>(
                selector, testCase1, testCases
            );
        }

        public static ParameterizedGroupedTestCaseWithSelector<TSource, TResult> Create<TSource, TResult>(
            Func<TSource, TResult> selector,
            IReadOnlyCollection<BaseParameterizedTestCase<TSource>> testCases)
        {
            return new ParameterizedGroupedTestCaseWithSelector<TSource, TResult>(
                selector, testCases
            );
        }
    }
}
