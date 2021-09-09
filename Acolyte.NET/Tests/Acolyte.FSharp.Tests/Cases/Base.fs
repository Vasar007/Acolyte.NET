module Acolyte.Functional.Tests.Cases.Base

open Acolyte.Tests.Cases.Parameterized


[<AbstractClass>]
type internal BaseTestCases<'TestCase, 'Parameter>(actualTestCases: seq<'TestCase>,
    parameterFactory: 'TestCase -> 'Parameter) =
    inherit BaseParameterizedTestCase<'Parameter>()

    let _actualCases = actualTestCases
    let _parameterFactory = parameterFactory

    member private _.GetTestCases (testCase: 'TestCase) =
        _parameterFactory testCase

    override this.GetValues() =
        _actualCases
            |> Seq.map (fun testCase -> this.GetTestCases testCase)

[<AbstractClass>]
type internal BaseWithSomethingTestCases<'TestCase, 'Parameter>(actualTestCases: seq<'TestCase>,
    parameterFactory: 'TestCase -> int32 -> 'Parameter,
    withTestCases: BaseParameterizedTestCase<int32>) =
    inherit BaseParameterizedTestCase<'Parameter>()

    let _actualCases = actualTestCases
    let _parameterFactory = parameterFactory
    let _withTestCases = withTestCases

    member private _.GetTestCasesForAllCases (testCase: 'TestCase) =
        _withTestCases
            // Class "BaseParameterizedTestCase<int32>" returns sequence of arrays with single integer.
            |> Seq.map (fun case -> (Array.exactlyOne case) :?> int32)
            |> Seq.map (fun case -> _parameterFactory testCase case)

    override this.GetValues() =
        _actualCases
            |> Seq.collect (fun testCase -> this.GetTestCasesForAllCases testCase)
