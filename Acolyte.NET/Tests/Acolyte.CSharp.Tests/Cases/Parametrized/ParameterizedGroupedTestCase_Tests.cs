using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Tests.Cases.Parameterized;
using Acolyte.Tests.Cases.Parametrized.Data.Strings;
using Acolyte.Tests.Cases.Parametrized.Data.ValueTuples;
using FluentAssertions;
using Xunit;

namespace Acolyte.Tests.Cases.Parametrized
{
    public sealed class ParameterizedGroupedTestCase_Tests
    {
        public ParameterizedGroupedTestCase_Tests()
        {
        }

        #region Null Values

        [Fact]
        public void ParameterizedGroupedTestCase_ForNullTestCase1_ShouldFail()
        {
            // Arrange.
            var testCase2 = new Parametrized_ComplexStringTestCase();

            // Act & Assert.
            Action checkMethod1 = () => _ = new ParameterizedGroupedTestCase<string>(testCase1: null!, testCase2);
            checkMethod1.Should().Throw<ArgumentNullException>();

            Action checkMethod2 = () => _ = ParameterizedGroupedTestCase.Create(testCase1: null!, testCase2);
            checkMethod2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForNullTestCases_ShouldFail()
        {
            // Arrange.
            var testCase1 = new Parametrized_SimpleStringTestCase();

            // Act & Assert.
            Action checkMethod1 = () => _ = new ParameterizedGroupedTestCase<string>(testCase1, testCases: null!);
            checkMethod1.Should().Throw<ArgumentNullException>();

            Action checkMethod2 = () => _ = ParameterizedGroupedTestCase.Create(testCase1, testCases: null!);
            checkMethod2.Should().Throw<ArgumentNullException>();

            Action checkMethod3 = () => _ = new ParameterizedGroupedTestCase<string>(testCases: null!);
            checkMethod3.Should().Throw<ArgumentNullException>();

            Action checkMethod4 = () => _ = ParameterizedGroupedTestCase.Create<string>(testCases: null!);
            checkMethod4.Should().Throw<ArgumentNullException>();
        }

        #endregion

        #region Empty Values

        [Fact]
        public void ParameterizedGroupedTestCase_ForEmptyTestCases_ShouldBeOk()
        {
            // Arrange.
            var testCases = Array.Empty<BaseParameterizedTestCase<string>>();

            // Act.
            var groupedTestCase1 = new ParameterizedGroupedTestCase<string>(testCases);
            var groupedTestCase2 = ParameterizedGroupedTestCase.Create(testCases);

            // Assert.
            groupedTestCase1.Should().NotBeNull();
            groupedTestCase2.Should().NotBeNull();
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForEmptyTestCasesWithTestCase1And2_ShouldBeOk()
        {
            // Arrange.
            var testCase1 = new Parametrized_SimpleStringTestCase();
            var testCases = Array.Empty<BaseParameterizedTestCase<string>>();

            // Act.
            var groupedTestCase1 = new ParameterizedGroupedTestCase<string>(testCase1, testCases);
            var groupedTestCase2 = ParameterizedGroupedTestCase.Create(testCase1, testCases);

            // Assert.
            groupedTestCase1.Should().NotBeNull();
            groupedTestCase2.Should().NotBeNull();
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForSingleTestCaseAsCollection_ShouldBeOk()
        {
            // Arrange.
            var testCase1 = new Parametrized_SimpleStringTestCase();
            var testCases = new[] { testCase1 };

            // Act.
            var groupedTestCase1 = new ParameterizedGroupedTestCase<string>(testCases);
            var groupedTestCase2 = ParameterizedGroupedTestCase.Create(testCases);

            // Assert.
            groupedTestCase1.Should().NotBeNull();
            groupedTestCase2.Should().NotBeNull();
        }

        #endregion

        #region Predefined Values

        [Fact]
        public void ParameterizedGroupedTestCase_ForStringTestCases_ShouldGroupTheirValues()
        {
            // Arrange.
            var simpleTestCase = new Parametrized_SimpleStringTestCase();
            var complexTestCase = new Parametrized_ComplexStringTestCase();

            // Act & Assert.
            ParameterizedGroupedTestCase_ActAndAssert(simpleTestCase, complexTestCase);
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForUnflattenValueTupleTestCases_ShouldGroupTheirValues()
        {
            // Arrange.
            var simpleTestCase = new Parametrized_UnflattenSimpleValueTupleTestCase();
            var complexTestCase = new Parametrized_UnflattenComplexValueTupleTestCase();

            // Act & Assert.
            ParameterizedGroupedTestCase_ActAndAssert(simpleTestCase, complexTestCase);
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForFlattenValueTupleTestCases_ShouldGroupTheirValues()
        {
            // Arrange.
            var simpleTestCase = new Parametrized_FlattenSimpleValueTupleTestCase();
            var complexTestCase = new Parametrized_FlattenComplexValueTupleTestCase();

            // Act & Assert.
            ParameterizedGroupedTestCase_ActAndAssert(simpleTestCase, complexTestCase);
        }

        [Fact]
        public void ParameterizedGroupedTestCase_ForMixedValueTupleTestCases_ShouldFail()
        {
            // Arrange.
            var simpleTestCase = new Parametrized_FlattenSimpleValueTupleTestCase();
            var complexTestCase = new Parametrized_UnflattenSimpleValueTupleTestCase();

            // Act & Assert.
            Action checkMethod1 = () => _ = new ParameterizedGroupedTestCase<(string, bool)>(simpleTestCase, complexTestCase);
            checkMethod1.Should().Throw<ArgumentException>();

            Action checkMethod2 = () => _ = ParameterizedGroupedTestCase.Create(simpleTestCase, complexTestCase);
            checkMethod2.Should().Throw<ArgumentException>();
        }

        #endregion

        #region Some Values

        // TODO: add tests with large collection of values in test cases.

        #endregion

        #region Random Values

        // TODO: add tests with random generated values in test cases.

        #endregion

        #region Private Methods

        private static IEnumerable<TData> GetExpectedValues<TData>(
            BaseParameterizedTestCase<TData> testCase1,
            params BaseParameterizedTestCase<TData>[] testCases)
        {
            return testCase1.GetValues()
                .Concat(testCases.SelectMany(testCase => testCase.GetValues()));
        }

        private static IEnumerable<object?[]> GetExpectedObjects<TData>(
            BaseParameterizedTestCase<TData> testCase1,
            params BaseParameterizedTestCase<TData>[] testCases)
        {
            return testCase1.ToArray()
                .Concat(testCases.SelectMany(testCase => testCase.ToArray()));
        }

        private static void ParameterizedGroupedTestCase_ActAndAssert<TData>(
            BaseParameterizedTestCase<TData> testCase1,
            params BaseParameterizedTestCase<TData>[] testCases)
        {
            // Arrange.
            var groupedTestCase = ParameterizedGroupedTestCase.Create(testCase1, testCases);

            var expectedValues = GetExpectedValues(testCase1, testCases);
            var expectedObjects = GetExpectedObjects(testCase1, testCases);

            // Act.
            var actualValues = groupedTestCase.GetValues();
            var actualObjects = groupedTestCase.AsEnumerable();

            // Assert.
            actualValues.Should().BeEquivalentTo(expectedValues);
            actualObjects.Should().BeEquivalentTo(expectedObjects);
        }

        #endregion
    }
}
