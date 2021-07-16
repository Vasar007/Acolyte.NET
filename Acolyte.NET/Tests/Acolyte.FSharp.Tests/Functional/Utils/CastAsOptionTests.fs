module Acolyte.Functional.Tests.Utils.CastAsOptionOptionTests

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
let public ``"castAsOption" can process null reference and return None`` () =
    // Arrange.
    let (nullEnumerable: seq<int32>) = null
    let expectedObj = option<array<int32>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

[<Fact>]
let public ``"castAsOption" can process null reference with another type and return None`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null
    let expectedObj = option<array<int32>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"castAsOption" converts non-null empty object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<option<array<int32>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAsOption" cannot convert empty object to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq
    let expectedObj = option<array<string>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"castAsOption" converts non-null object with predefined items to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<option<array<int32>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAsOption" cannot convert object with predefined items to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq
    let expectedObj = option<array<string>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"castAsOption" converts non-null object with some items to the specified type``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<option<array<int32>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``If "castAsOption" cannot convert object with some items to the specified type, it returns the null reference``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let expectedObj = option<array<string>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Random Values

[<Fact>]
let public ``"castAsOption" converts non-null object with random items to the specified type`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<option<array<int32>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAsOption" cannot convert object with random items to the specified type, it returns the null reference`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let expectedObj = option<array<string>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"castAsOption" should only cast (i.g. change type) of value without iteration`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualObj |> should not' Null
    actualObj |> should be ofExactType<option<array<int32>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs collection

[<Fact>]
let public ``"castAsOption" should only try to cast (i.g. change type) of value without iteration in case of failure`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let expectedObj = option<array<string>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<string>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion
