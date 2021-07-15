module Acolyte.Functional.Tests.Cases.AppendSingleton


open Acolyte.Functional.Collections
open Acolyte.Tests.Cases.Parameterized

type internal ConversionFunction<'T> = (seq<'T> -> seq<'T>)

type internal ExpectedValueFactory<'T> = ('T -> seq<'T> -> seq<'T>)

type internal ActualValueFactory<'T> = ('T -> seq<'T> -> seq<'T>)

type public TestCaseParameters<'T> = {
    Conversion: ConversionFunction<'T>
    ExpectedFactory: ExpectedValueFactory<'T>
    ActualFactory: ActualValueFactory<'T>
}

type public TestCaseParametersWithCount<'T> = {
    Common: TestCaseParameters<'T>
    Count: int32
}

type internal IAppendSingletonTestCase<'T> =
    abstract member Convert : seqToConvert: seq<'T> -> seq<'T>
    abstract member GetExpectedValue : itemToAppend: 'T -> initialSeq: seq<'T> -> seq<'T>
    abstract member GetActualValue : itemToAppend: 'T -> initialSeq: seq<'T> -> seq<'T>

type internal SeqExAppendSingletonTestCase<'T>() =
    interface IAppendSingletonTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert |> this.SeqToSeqForce

        member this.GetExpectedValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToSeqForce
                |> Seq.append (Seq.singleton itemToAppend)

        member this.GetActualValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToSeqForce
                |> SeqEx.appendSingleton itemToAppend

    member private _.SeqToSeqForce (seqToConvert: seq<'T>) =
        seqToConvert |> SeqEx.asSeq

type internal ListExAppendSingletonTestCase<'T>() =
    interface IAppendSingletonTestCase<'T> with

        override this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToList
                |> SeqEx.asSeq

        override this.GetExpectedValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> List.append (List.singleton itemToAppend)
                |> SeqEx.asSeq

        override this.GetActualValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> ListEx.appendSingleton itemToAppend
                |> SeqEx.asSeq

    member private _.SeqToList (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? list<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toList

type internal ArrayExAppendSingletonTestCase<'T>() =
    interface IAppendSingletonTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToArray
                |> SeqEx.asSeq

        member this.GetExpectedValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> Array.append (Array.singleton itemToAppend)
                |> SeqEx.asSeq

        member this.GetActualValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> ArrayEx.appendSingleton itemToAppend
                |> SeqEx.asSeq

    member private _.SeqToArray (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? array<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toArray

let private activeTestCases<'T> = [
    SeqExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
    ListExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
    ArrayExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
]

type internal AppendSingletonTestCases<'T>() =
    inherit BaseParameterizedTestCase<TestCaseParameters<'T>>()

    override _.GetValues() =
        activeTestCases<'T>
            |> Seq.map (fun testCase -> {
                        Conversion = testCase.Convert
                        ExpectedFactory = testCase.GetExpectedValue
                        ActualFactory = testCase.GetActualValue })

[<AbstractClass>]
type internal BaseAppendSingletonWithSomethingTestCases<'T>(withTestCases: BaseParameterizedTestCase<int32>) =
    inherit BaseParameterizedTestCase<TestCaseParametersWithCount<'T>>()

    let _withTestCases = withTestCases

    member private _.GetTestCasesForAllPositive (testCase: IAppendSingletonTestCase<'T>) =
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
                        Count = case })

    override this.GetValues() =
        activeTestCases<'T>
            |> Seq.collect (fun testCase -> this.GetTestCasesForAllPositive testCase)

type internal AppendSingletonWithPositiveTestCases<'T>() =
    inherit BaseAppendSingletonWithSomethingTestCases<'T>(PositiveTestCases())
