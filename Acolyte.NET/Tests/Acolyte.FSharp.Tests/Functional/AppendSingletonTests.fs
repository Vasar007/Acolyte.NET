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
    (conversion: ConversionFunction<int32>) (expectedFactory: ExpectedValueFactory<int32>) (actualFactory: ActualValueFactory<int32>) =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ actualFactory Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" appends item to the empty collection``
    (conversion: ConversionFunction<int32>) (expectedFactory: ExpectedValueFactory<int32>) (actualFactory: ActualValueFactory<int32>) =
    // Arrange.
    let emptySeq = Seq.empty |> conversion
    let itemToAppend = 1
    let expectedSeq = emptySeq
                      |> expectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = emptySeq
                    |> actualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Predefined Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" appends item for prefefined collection``
    (conversion: ConversionFunction<int32>) (expectedFactory: ExpectedValueFactory<int32>) (actualFactory: ActualValueFactory<int32>) =
    // Arrange.
    let predefinedSeq = [ 1..3 ] |> conversion
    let itemToAppend = 4
    let expectedSeq = predefinedSeq
                      |> expectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = predefinedSeq
                    |> actualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<AppendSingletonWithPositiveTestCases<int32>>)>]
let public ``"appendSingleton" appends item for collection with some items``
    (conversion: ConversionFunction<int32>) (expectedFactory: ExpectedValueFactory<int32>) (actualFactory: ActualValueFactory<int32>) (count: int32) =
    // Arrange.
    let seqWithSomeItems = FsTestDataCreator.createRandomInt32Seq count |> conversion
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = seqWithSomeItems
                      |> expectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = seqWithSomeItems
                    |> actualFactory itemToAppend
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Random Values

[<Theory>]
[<ClassData(typeof<AppendSingletonTestCases<int32>>)>]
let public ``"appendSingleton" skips or does not skip items for random collection``
    (conversion: ConversionFunction<int32>) (expectedFactory: ExpectedValueFactory<int32>) (actualFactory: ActualValueFactory<int32>) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32Seq count |> conversion
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = randomdSeq
                      |> expectedFactory itemToAppend
                      |> Seq.toList

    // Act.
    let actualSeq = randomdSeq
                    |> actualFactory itemToAppend
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
