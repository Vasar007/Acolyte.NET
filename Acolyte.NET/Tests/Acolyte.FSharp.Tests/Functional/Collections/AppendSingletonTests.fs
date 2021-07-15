module Acolyte.Functional.Tests.AppendSingletontonTests

open System
open Acolyte.Functional.Collections
open Acolyte.Functional.Tests.Cases.AppendSingleton
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


/// region: Null Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" throw an exception if argument "source" is null``
    (parameters: TestCaseParameters<int32>) =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ parameters.ActualFactory Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" appends item to the empty collection``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let emptySeq = Seq.empty |> parameters.Conversion
    let itemToAppend = 1
    let expectedSeq = emptySeq
                      |> parameters.ExpectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = emptySeq
                    |> parameters.ActualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Predefined Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" appends item for prefefined collection``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let predefinedSeq = [ 1..3 ]
                        |> seq
                        |> parameters.Conversion
    let itemToAppend = 4
    let expectedSeq = predefinedSeq
                      |> parameters.ExpectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = predefinedSeq
                    |> parameters.ActualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<AppendSingletonWithPositiveTestCases<int32>>)>]
let public ``"appendSingleton" appends item for collection with some items``
    (parameters: TestCaseParametersWithCount<int32>) =
    // Arrange.
    let seqWithSomeItems = FsTestDataCreator.createRandomInt32Seq parameters.Count
                           |> parameters.Common.Conversion
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = seqWithSomeItems
                      |> parameters.Common.ExpectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = seqWithSomeItems
                    |> parameters.Common.ActualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Random Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" skips or does not skip items for random collection``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32Seq count
                     |> parameters.Conversion
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = randomdSeq
                      |> parameters.ExpectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = randomdSeq
                    |> parameters.ActualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"appendSingleton" should only append (i.g. create new seq) item without iteration`` () =
    // Arrange.
    let collection = [ 1..4 ]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let itemToAppend = 5
    let expectedSeq = collection
                      |> Seq.append (Seq.singleton itemToAppend)
                      |> Seq.toList

    // Act.
    let actualSeq = explosive
                    |> SeqEx.appendSingleton itemToAppend

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualSeq
        |> Seq.toList
        |> should equal expectedSeq
    CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection))

/// endregion
