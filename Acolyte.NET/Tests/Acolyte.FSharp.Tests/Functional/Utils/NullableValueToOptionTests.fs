module Acolyte.Functional.Tests.Utils.NullableValueToOptionTests

open System
open Acolyte.Functional
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Xunit


// Cannot combine this tests with "nullableReferenceToOption" because input arguments are too different.

/// region: Private Methods

let customEquals (expected: Nullable<'T>) (actual: option<'T>) =
    if expected.HasValue then
        actual.IsSome |> should be True
        actual.Value |> should equal expected.Value
    else
        actual |> should be Null
        actual.IsNone |> should be True

/// endregion

/// region: Null Values

[<Fact>]
let public ``"nullableValueToOption" can process null reference`` () =
    // Arrange.
    let expectedResult = Nullable<int32>()

    // Act.
    let actualResult = Utils.nullableValueToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"nullableValueToOption" converts non-null empty object to the specified type`` () =
    // Arrange.
    let expectedResult = Nullable<int32>(0)

    // Act.
    let actualResult = Utils.nullableValueToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"nullableValueToOption" converts non-null object with predefined items to the specified type`` () =
    // Arrange.
    let expectedResult = Nullable<int32>(42)

    // Act.
    let actualResult = Utils.nullableValueToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"nullableValueToOption" converts non-null object with some items to the specified type``
    (count: int32) =
    // Arrange.
    let listWithNullables = FsTestDataCreator.createRandomNullableInt32List count

    // Act.
    let actualResult = listWithNullables |> List.map Utils.nullableValueToOption

    // Assert.
    actualResult
        |> List.map2 customEquals listWithNullables

/// endregion

/// region: Random Values

[<Fact>]
let public ``"nullableValueToOption" converts non-null object with random items to the specified type`` () =
    // Arrange.
    let expectedResult = TestDataCreator.CreateRandomNullableInt32()

    // Act.
    let actualResult = Utils.nullableValueToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Extended Logical Coverage

// No extended logical coverage for "nullableValueToOption" function.

/// endregion

