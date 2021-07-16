module Acolyte.Functional.Tests.Cases.Utils.CastAs

open Acolyte.Functional
open Acolyte.Functional.Tests.Cases.Base
open Acolyte.Tests.Cases.Parameterized


/// region: Test Cases And Parameters Definitions

type internal ActualValueFactory<'T, 'Return when 'Return : null> = ('T -> option<'Return>)

type public TestCaseParameters<'T, 'Return when 'Return : null> = {
    ActualFactory: ActualValueFactory<'T, 'Return>
}

type public TestCaseParametersWithCount<'T, 'Return when 'Return : null> = {
    Common: TestCaseParameters<'T, 'Return>
    Count: int32
}

type internal ICastAsTestCase<'T, 'Return> =
    abstract member GetActualValue : valueToCast: 'T -> option<'Return>

/// endregion

/// region: Test Cases Implementations

type internal CastAsTestCase<'T, 'Return when 'Return : null>() =
    interface ICastAsTestCase<'T, 'Return> with

        member _.GetActualValue (valueToCast: 'T) =
            let result = Utils.castAs<'Return> valueToCast
            Utils.nullableReferenceToOption result

type internal CastAsOptionTestCase<'T, 'Return>() =
    interface ICastAsTestCase<'T, 'Return> with

        member _.GetActualValue (valueToCast: 'T) =
            Utils.castAsOption<'Return> valueToCast

/// endregion

/// region: Test Cases With Parameters

let private activeTestCases<'T, 'Return when 'Return : null> = [
    CastAsTestCase<'T, 'Return>() :> ICastAsTestCase<'T, 'Return>
    CastAsOptionTestCase<'T, 'Return>() :> ICastAsTestCase<'T, 'Return>
]

let private getParametersFactory (testCase: ICastAsTestCase<'T, 'Return>) =
    let parameters = {
        ActualFactory = testCase.GetActualValue
    }
    parameters

let private getParametersWithCountFactory (testCase: ICastAsTestCase<'T, 'Return>) (count: int32) =
    let parametersWithCount = {
        Common = getParametersFactory testCase
        Count = count
    }
    parametersWithCount

type internal CastAsTestCases<'T, 'Return when 'Return : null>() =
    inherit BaseTestCases<ICastAsTestCase<'T, 'Return>, TestCaseParameters<'T, 'Return>>(
        activeTestCases<'T, 'Return>, getParametersFactory
    )

type internal CastAsWithPositiveTestCases<'T, 'Return when 'Return : null>() =
    inherit BaseWithSomethingTestCases<ICastAsTestCase<'T, 'Return>, TestCaseParametersWithCount<'T, 'Return>>(
        activeTestCases<'T, 'Return>, getParametersWithCountFactory, PositiveTestCases()
    )

/// endregion

