module Acolyte.Functional.Tests.Cases.Collections.SkipSafe

open Acolyte.Functional.Collections
open Acolyte.Functional.Tests.Cases.Base
open Acolyte.Tests.Cases.Parameterized


/// region: Test Cases And Parameters Definitions

type internal ConversionFunction<'T> = (seq<'T> -> seq<'T>)

type internal ExpectedValueFactory<'T> = (int32 -> seq<'T> -> seq<'T>)

type internal ActualValueFactory<'T> = (int32 -> seq<'T> -> seq<'T>)

type public TestCaseParameters<'T> = {
    Conversion: ConversionFunction<'T>
    ExpectedFactory: ExpectedValueFactory<'T>
    ActualFactory: ActualValueFactory<'T>
}

type public TestCaseParametersWithSkipCount<'T> = {
    Common: TestCaseParameters<'T>
    SkipCount: int32
}

type internal ISkipSafeTestCase<'T> =
    abstract member Convert : seqToConvert: seq<'T> -> seq<'T>
    abstract member GetExpectedValue : skipCount: int32 -> initialSeq: seq<'T> -> seq<'T>
    abstract member GetActualValue : skipCount: int32 -> initialSeq: seq<'T> -> seq<'T>

/// endregion

/// region: Test Cases Implementations

type internal SeqExSkipSafeTestCase<'T>() =
    interface ISkipSafeTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert |> this.SeqToSeqForce

        member this.GetExpectedValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToSeqForce
                |> Seq.skip skipCount

        member this.GetActualValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToSeqForce
                |> SeqEx.skipSafe skipCount

    member private _.SeqToSeqForce (seqToConvert: seq<'T>) =
        seqToConvert |> seq

type internal ListExSkipSafeTestCase<'T>() =
    interface ISkipSafeTestCase<'T> with

        override this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToList
                |> seq

        override this.GetExpectedValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> List.skip skipCount
                |> seq

        override this.GetActualValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> ListEx.skipSafe skipCount
                |> seq

    member private _.SeqToList (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? list<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toList

type internal ArrayExSkipSafeTestCase<'T>() =
    interface ISkipSafeTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToArray
                |> seq

        member this.GetExpectedValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> Array.skip skipCount
                |> seq

        member this.GetActualValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> ArrayEx.skipSafe skipCount
                |> seq

    member private _.SeqToArray (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? array<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toArray

/// endregion

/// region: Test Cases With Parameters

let private activeTestCases<'T> = [
    SeqExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
    ListExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
    ArrayExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
]

let private getParametersFactory (testCase: ISkipSafeTestCase<'T>) =
    let parameters = {
        Conversion = testCase.Convert
        ExpectedFactory = testCase.GetExpectedValue
        ActualFactory = testCase.GetActualValue
    }
    parameters

let private getParametersWithSkipCountFactory (testCase: ISkipSafeTestCase<'T>) (skipCount: int32) =
    let parametersWithCount = {
        Common = getParametersFactory testCase
        SkipCount = skipCount
    }
    parametersWithCount

type internal SkipSafeTestCases<'T>() =
    inherit BaseTestCases<ISkipSafeTestCase<'T>, TestCaseParameters<'T>>(
        activeTestCases<'T>, getParametersFactory
    )

type internal SkipSafeWithPositiveTestCases<'T>() =
    inherit BaseWithSomethingTestCases<ISkipSafeTestCase<'T>, TestCaseParametersWithSkipCount<'T>>(
        activeTestCases<'T>, getParametersWithSkipCountFactory, PositiveTestCases()
    )
type internal SkipSafeWithNegativeAndZeroTestCases<'T>() =
    inherit BaseWithSomethingTestCases<ISkipSafeTestCase<'T>, TestCaseParametersWithSkipCount<'T>>(
        activeTestCases<'T>, getParametersWithSkipCountFactory, NegativeWithZeroTestCases()
    )

/// endregion
