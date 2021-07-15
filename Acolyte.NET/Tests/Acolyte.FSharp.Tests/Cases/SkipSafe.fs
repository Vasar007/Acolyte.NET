module Acolyte.Functional.Tests.Cases.SkipSafe


open Acolyte.Functional.Collections
open Acolyte.Tests.Cases.Parameterized

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
        seqToConvert |> SeqEx.asSeq

type internal ListExSkipSafeTestCase<'T>() =
    interface ISkipSafeTestCase<'T> with

        override this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToList
                |> SeqEx.asSeq

        override this.GetExpectedValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> List.skip skipCount
                |> SeqEx.asSeq

        override this.GetActualValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> ListEx.skipSafe skipCount
                |> SeqEx.asSeq

    member private _.SeqToList (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? list<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toList

type internal ArrayExSkipSafeTestCase<'T>() =
    interface ISkipSafeTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToArray
                |> SeqEx.asSeq

        member this.GetExpectedValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> Array.skip skipCount
                |> SeqEx.asSeq

        member this.GetActualValue (skipCount: int32) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> ArrayEx.skipSafe skipCount
                |> SeqEx.asSeq

    member private _.SeqToArray (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? array<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toArray

let private activeTestCases<'T> = [
    SeqExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
    ListExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
    ArrayExSkipSafeTestCase<'T>() :> ISkipSafeTestCase<'T>
]

type internal SkipSafeTestCases<'T>() =
    inherit BaseParameterizedTestCase<TestCaseParameters<'T>>()

    override _.GetValues() =
        activeTestCases<'T>
            |> Seq.map (fun testCase -> {
                        Conversion = testCase.Convert
                        ExpectedFactory = testCase.GetExpectedValue
                        ActualFactory = testCase.GetActualValue })

[<AbstractClass>]
type internal BaseSkipSafeWithSomethingTestCases<'T>(withTestCases: BaseParameterizedTestCase<int32>) =
    inherit BaseParameterizedTestCase<TestCaseParametersWithSkipCount<'T>>()

    let _withTestCases = withTestCases

    member private _.GetTestCasesForAllPositive (testCase: ISkipSafeTestCase<'T>) =
        let parameters = {
            Conversion = testCase.Convert
            ExpectedFactory = testCase.GetExpectedValue
            ActualFactory = testCase.GetActualValue
        }
        _withTestCases
            // Class "PositiveTestCases" returns sequence of arrays with single integer.
            |> Seq.map (fun case -> (Array.exactlyOne case) :?> int32)
            |> Seq.map (fun case -> {
                        Common = parameters
                        SkipCount = case })

    override this.GetValues() =
        activeTestCases<'T>
            |> Seq.collect (fun testCase -> this.GetTestCasesForAllPositive testCase)

type internal SkipSafeWithPositiveTestCases<'T>() =
    inherit BaseSkipSafeWithSomethingTestCases<'T>(PositiveTestCases())

type internal SkipSafeWithNegativeAndZeroTestCases<'T>() =
    inherit BaseSkipSafeWithSomethingTestCases<'T>(NegativeWithZeroTestCases())
