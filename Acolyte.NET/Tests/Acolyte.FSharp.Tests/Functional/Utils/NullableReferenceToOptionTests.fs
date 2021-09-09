module Acolyte.Functional.Tests.Utils.NullableReferenceToOptionTests

open System
open Acolyte.Functional
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Xunit


// Cannot combine this tests with "nullableValueToOption" because input arguments are too different.

/// region: Private Methods

let customEquals (expected: 'T) (actual: option<'T>) =
    if not (isNull expected) then
        actual.IsSome |> should be True
        actual.Value |> should equal expected
    else
        actual |> should be Null
        actual.IsNone |> should be True

/// endregion

/// region: Null Values

[<Fact>]
let public ``"nullableReferenceToOption" can process null reference`` () =
    // Arrange.
    let expectedResult = Unchecked.defaultof<string>

    // Act.
    let actualResult = Utils.nullableReferenceToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"nullableReferenceToOption" converts non-null empty object to the specified type`` () =
    // Arrange.
    let expectedResult = String.Empty

    // Act.
    let actualResult = Utils.nullableReferenceToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"nullableReferenceToOption" converts non-null object with predefined items to the specified type`` () =
    // Arrange.
    let expectedResult = "SomeString123"

    // Act.
    let actualResult = Utils.nullableReferenceToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"nullableReferenceToOption" converts non-null object with some items to the specified type``
    (count: int32) =
    // Arrange.
    let listWithNullables = FsTestDataCreator.createRandomNullableStringList count

    // Act.
    let actualResult = listWithNullables |> List.map Utils.nullableReferenceToOption

    // Assert.
    actualResult
        |> List.map2 customEquals listWithNullables

/// endregion

/// region: Random Values

[<Fact>]
let public ``"nullableReferenceToOption" converts non-null object with random items to the specified type`` () =
    // Arrange.
    let expectedResult = TestDataCreator.CreateRandomNullableString()

    // Act.
    let actualResult = Utils.nullableReferenceToOption expectedResult

    // Assert.
    actualResult |> customEquals expectedResult

/// endregion

/// region: Extended Logical Coverage

// No extended logical coverage for "nullableReferenceToOption" function.

/// endregion
