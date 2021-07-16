module Acolyte.Functional.Tests.Utils.CastAsTests

open Acolyte.Functional
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Cases.Parameterized;
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Xunit

// Note: using array in tests to allow work with null reference.

/// region: Null Values

[<Fact>]
let public ``"castAs" can process null reference`` () =
    // Arrange.
    let (nullEnumerable: seq<int32>) = null
    let (expectedResult: array<int32>) = null

    // Act.
    let actualResult = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"castAs" can process null reference with another type`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null
    let (expectedResult: array<int32>) = null

    // Act.
    let actualResult = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"castAs" converts non-null empty object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq

    // Act.
    let actualResult = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult |> should be ofExactType<array<int32>>

    actualResult |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert empty object to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq
    let (expectedResult: array<string>) = null

    // Act.
    let actualResult = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"castAs" converts non-null object with predefined items to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq

    // Act.
    let actualResult = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult |> should be ofExactType<array<int32>>

    actualResult |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert object with predefined items to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq
    let (expectedResult: array<string>) = null

    // Act.
    let actualResult = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"castAs" converts non-null object with some items to the specified type``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualResult = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult |> should be ofExactType<array<int32>>

    actualResult |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``If "castAs" cannot convert object with some items to the specified type, it returns the null reference``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let (expectedResult: array<string>) = null

    // Act.
    let actualResult = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Random Values

[<Fact>]
let public ``"castAs" converts non-null object with random items to the specified type`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualResult = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult |> should be ofExactType<array<int32>>

    actualResult |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert object with random items to the specified type, it returns the null reference`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let (expectedResult: array<string>) = null

    // Act.
    let actualResult = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"castAs" should only cast (i.g. change type) of value without iteration`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)

    // Act.
    let actualResult = Utils.castAs<array<int32>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualResult |> should not' Null
    actualResult |> should be ofExactType<array<int32>>

    actualResult |> should sameAs collection

[<Fact>]
let public ``"castAs" should only try to cast (i.g. change type) of value without iteration in case of failure`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let (expectedResult: array<string>) = null

    // Act.
    let actualResult = Utils.castAs<array<string>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualResult |> should be Null
    actualResult |> should sameAs expectedResult

/// endregion
