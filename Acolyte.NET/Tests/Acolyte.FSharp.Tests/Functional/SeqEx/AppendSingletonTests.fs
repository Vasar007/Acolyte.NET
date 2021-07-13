module Acolyte.Functional.Tests.SeqEx.appendSingletontonTests


open System
open Acolyte.Functional
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit
open Acolyte.Functional.Tests.Helpers

/// region: Null Values

[<Fact>]
let ``"appendSingleton" throw an exception if argument "source" is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ SeqEx.appendSingleton Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Fact>]
let ``"appendSingleton" appends item to the empty collection`` () =
    // Arrange.
    let emptySeq = Seq.empty
    let itemToAppend = 1
    let expectedSeq = emptySeq
                      |> Seq.append (Seq.singleton itemToAppend)
                      |> Seq.toList

    // Act.
    let actualSeq = emptySeq
                    |> SeqEx.appendSingleton itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Predefined Values

[<Fact>]
let ``"appendSingleton" appends item for prefefined collection`` () =
    // Arrange.
    let predefinedSeq = [ 1..3 ] |> Seq.cast
    let itemToAppend = 4
    let expectedSeq = predefinedSeq
                      |> Seq.append (Seq.singleton itemToAppend)
                      |> Seq.toList

    // Act.
    let actualSeq = predefinedSeq
                    |> SeqEx.appendSingleton itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"appendSingleton" appends item for collection with some items``
    (count: int32) =
    // Arrange.
    let seqWithSomeItems = FsTestDataCreator.createRandomInt32Seq count
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = seqWithSomeItems
                      |> Seq.append (Seq.singleton itemToAppend)
                      |> Seq.toList

    // Act.
    let actualSeq = seqWithSomeItems
                    |> SeqEx.appendSingleton itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Random Values

[<Fact>]
let ``"appendSingleton" skips or does not skip items for random collection`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32Seq count
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = randomdSeq
                      |> Seq.append (Seq.singleton itemToAppend)
                      |> Seq.toList

    // Act.
    let actualSeq = randomdSeq
                    |> SeqEx.appendSingleton itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let ``"appendSingleton" should only append (i.g. create new seq) item without iteration`` () =
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
