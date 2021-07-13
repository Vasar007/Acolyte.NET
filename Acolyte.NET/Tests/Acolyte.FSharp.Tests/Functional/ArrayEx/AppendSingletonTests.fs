module Acolyte.Functional.Tests.ArrayEx.appendSingletontonTests


open System
open Acolyte.Functional
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit

/// region: Null Values

[<Fact>]
let ``"appendSingleton" throw an exception if argument "source" is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ ArrayEx.appendSingleton Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Fact>]
let ``"appendSingleton" appends item to the empty collection`` () =
    // Arrange.
    let emptySeq = Array.empty
    let itemToAppend = 1
    let expectedSeq = emptySeq |> Array.append (Array.singleton itemToAppend)

    // Act.
    let actualSeq = emptySeq |> ArrayEx.appendSingleton itemToAppend

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Predefined Values

[<Fact>]
let ``"appendSingleton" appends item for prefefined collection`` () =
    // Arrange.
    let predefinedSeq = [| 1..3 |]
    let itemToAppend = 4
    let expectedSeq = predefinedSeq |> Array.append (Array.singleton itemToAppend)

    // Act.
    let actualSeq = predefinedSeq |> ArrayEx.appendSingleton itemToAppend

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"appendSingleton" appends item for collection with some items``
    (count: int32) =
    // Arrange.
    let seqWithSomeItems = FsTestDataCreator.createRandomInt32Array count
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = seqWithSomeItems |> Array.append (Array.singleton itemToAppend)

    // Act.
    let actualSeq = seqWithSomeItems |> ArrayEx.appendSingleton itemToAppend

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Random Values

[<Fact>]
let ``"appendSingleton" skips or does not skip items for random collection`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32Array count
    let itemToAppend = TestDataCreator.CreateRandomInt32()
    let expectedSeq = randomdSeq |> Array.append (Array.singleton itemToAppend)

    // Act.
    let actualSeq = randomdSeq |> ArrayEx.appendSingleton itemToAppend

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

// We cannot use explosive collection due to collection restriction for methods from ArrayEx.

/// endregion
