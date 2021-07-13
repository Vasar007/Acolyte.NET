module Acolyte.Functional.Tests.ListEx.SkipSafeTests


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
let ``"skipSafe" throw an exception if argument "source" is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ ListEx.skipSafe Unchecked.defaultof<int> Unchecked.defaultof<list<int>> @>

/// endregion

/// region: Empty Values

[<Fact>]
let ``"skipSafe" does not do anything if collection is empty`` () =
    // Arrange.
    let emptySeq = List.empty
    let skipCount = 1

    // Act.
    let actualSeq = emptySeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should be Empty

/// endregion

/// region: Predefined Values

[<Fact>]
let ``"skipSafe" skips some items for prefefined collection`` () =
    // Arrange.
    let predefinedSeq = [ 1..3 ]
    let skipCount = 1
    let expectedSeq = predefinedSeq |> List.skip skipCount

    // Act.
    let actualSeq = predefinedSeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<NegativeWithZeroTestCases>)>]
let ``"skipSafe" does not skip any items if "count" parameter is not positive``
    (skipCount: int32) =
    // Arrange.
    let expectedSeq = [ 1..3 ]

    // Act.
    let actualSeq = expectedSeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is less than collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount + 1
    let originalSeq = [ 0..length ]
    let expectedSeq = originalSeq |> List.skip skipCount

    // Act.
    let actualSeq = originalSeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is equal to collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount
    let originalSeq = [ 1..length ]

    // Act.
    let actualSeq = originalSeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is greater than collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount - 1
    let originalSeq = [ 0..length ]

    // Act.
    let actualSeq = originalSeq |> ListEx.skipSafe skipCount

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

/// endregion

/// region: Random Values

[<Fact>]
let ``"skipSafe" skips or does not skip items for random collection`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32List count
    let skipCount = TestDataCreator.CreateRandomInt32()

    // Act.
    let actualSeq = randomdSeq |> ListEx.skipSafe skipCount

    // Assert.
    if skipCount > count then
        actualSeq |> should be Empty
    else
        let expectedSeq = randomdSeq |> List.skip skipCount
        actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

// We cannot use explosive collection due to collection restriction for methods from ListEx.

/// endregion
