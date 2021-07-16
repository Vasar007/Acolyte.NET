module Acolyte.Functional.Tests.Cases.Throw.ThrowModule

open Acolyte.Functional
open Acolyte.Functional.Tests.Cases.Base
open Acolyte.Tests.Cases.Parameterized


/// region: Test Cases And Parameters Definitions

type internal ExpectedValueFactory<'T when 'T : null> = ('T -> 'T)

type internal ActualValueFactory<'T when 'T : null> = ('T -> string -> 'T)

type public TestCaseParameters<'T when 'T : null> = {
    ExpectedFactory: ExpectedValueFactory<'T>
    ActualFactory: ActualValueFactory<'T>
}

type public TestCaseParametersWithCount<'T when 'T : null> = {
    Common: TestCaseParameters<'T>
    Count: int32
}

type internal IThrowModuleTestCase<'T> =
    abstract member GetExpectedValue : valueToCheck: 'T -> 'T
    abstract member GetActualValue : valueToCheck: 'T -> paramName: string -> 'T

/// endregion

/// region: Test Cases Implementations

type internal IfNullValueTestCase<'T>() =
    interface IThrowModuleTestCase<'T> with

        member _.GetExpectedValue (valueToCheck: 'T) =
            valueToCheck

        member _.GetActualValue (valueToCheck: 'T) (paramName: string) =
            Throw.ifNullValue valueToCheck paramName

type internal IfNullTestCase<'T when 'T : null>() =
    interface IThrowModuleTestCase<'T> with

        member _.GetExpectedValue (valueToCheck: 'T) =
            valueToCheck

        member _.GetActualValue (valueToCheck: 'T) (paramName: string) =
            Throw.ifNull valueToCheck paramName

type internal CheckIfNullValueTestCase<'T when 'T : null>() =
    interface IThrowModuleTestCase<'T> with

        member _.GetExpectedValue (valueToCheck: 'T) =
            Unchecked.defaultof<'T>

        member _.GetActualValue (valueToCheck: 'T) (paramName: string) =
            Throw.checkIfNull valueToCheck paramName
            Unchecked.defaultof<'T>

type internal CheckIfNullTestCase<'T when 'T : null>() =
    interface IThrowModuleTestCase<'T> with

        member _.GetExpectedValue (valueToCheck: 'T) =
            Unchecked.defaultof<'T>

        member _.GetActualValue (valueToCheck: 'T) (paramName: string) =
            Throw.checkIfNull valueToCheck paramName
            Unchecked.defaultof<'T>

/// endregion

/// region: Test Cases With Parameters

let private activeTestCases<'T when 'T : null> = [
    IfNullValueTestCase<'T>() :> IThrowModuleTestCase<'T>
    IfNullTestCase<'T>() :> IThrowModuleTestCase<'T>
    CheckIfNullValueTestCase<'T>() :> IThrowModuleTestCase<'T>
    CheckIfNullTestCase<'T>() :> IThrowModuleTestCase<'T>
]

let private getParametersFactory (testCase: IThrowModuleTestCase<'T>) =
    let parameters = {
        ExpectedFactory = testCase.GetExpectedValue
        ActualFactory = testCase.GetActualValue
    }
    parameters

let private getParametersWithCountFactory (testCase: IThrowModuleTestCase<'T>) (count: int32) =
    let parametersWithCount = {
        Common = getParametersFactory testCase
        Count = count
    }
    parametersWithCount

type internal ThrowModuleTestCases<'T when 'T : null>() =
    inherit BaseTestCases<IThrowModuleTestCase<'T>, TestCaseParameters<'T>>(
        activeTestCases<'T>, getParametersFactory
    )

type internal ThrowModuleWithPositiveTestCases<'T when 'T : null>() =
    inherit BaseWithSomethingTestCases<IThrowModuleTestCase<'T>, TestCaseParametersWithCount<'T>>(
        activeTestCases<'T>, getParametersWithCountFactory, PositiveTestCases()
    )

/// endregion
