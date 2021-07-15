module Acolyte.Functional.Tests.Cases.AppendSingleton

open Acolyte.Functional.Collections
open Acolyte.Functional.Tests.Cases.Base
open Acolyte.Tests.Cases.Parameterized


/// region: Test Cases And Parameters Definitions

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

/// endregion

/// region: Test Cases Implementations

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
        seqToConvert |> seq

type internal ListExAppendSingletonTestCase<'T>() =
    interface IAppendSingletonTestCase<'T> with

        override this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToList
                |> seq

        override this.GetExpectedValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> List.append (List.singleton itemToAppend)
                |> seq

        override this.GetActualValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToList
                |> ListEx.appendSingleton itemToAppend
                |> seq

    member private _.SeqToList (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? list<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toList

type internal ArrayExAppendSingletonTestCase<'T>() =
    interface IAppendSingletonTestCase<'T> with

        member this.Convert (seqToConvert: seq<'T>) =
            seqToConvert
                |> this.SeqToArray
                |> seq

        member this.GetExpectedValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> Array.append (Array.singleton itemToAppend)
                |> seq

        member this.GetActualValue (itemToAppend: 'T) (initialSeq: seq<'T>) =
            initialSeq
                |> this.SeqToArray
                |> ArrayEx.appendSingleton itemToAppend
                |> seq

    member private _.SeqToArray (seqToConvert: seq<'T>) =
        match seqToConvert with
            | :? array<'T> as convertedList -> convertedList
            | _ -> seqToConvert |> Seq.toArray

/// endregion

/// region: Test Cases With Parameters

let private activeTestCases<'T> = [
    SeqExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
    ListExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
    ArrayExAppendSingletonTestCase<'T>() :> IAppendSingletonTestCase<'T>
]

let private getParametersFactory (testCase: IAppendSingletonTestCase<'T>) =
    let parameters = {
        Conversion = testCase.Convert
        ExpectedFactory = testCase.GetExpectedValue
        ActualFactory = testCase.GetActualValue
    }
    parameters

let private getParametersWithCountFactory (testCase: IAppendSingletonTestCase<'T>) (count: int32) =
    let parametersWithCount = {
        Common = getParametersFactory testCase
        Count = count
    }
    parametersWithCount

type internal AppendSingletonTestCases<'T>() =
    inherit BaseTestCases<IAppendSingletonTestCase<'T>, TestCaseParameters<'T>>(
        activeTestCases<'T>, getParametersFactory
    )

type internal AppendSingletonWithPositiveTestCases<'T>() =
    inherit BaseWithSomethingTestCases<IAppendSingletonTestCase<'T>, TestCaseParametersWithCount<'T>>(
        activeTestCases<'T>, getParametersWithCountFactory, PositiveTestCases()
    )

/// endregion
