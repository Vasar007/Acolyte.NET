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
    let (expectedObj: array<int32>) = null

    // Act.
    let actualObj = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

[<Fact>]
let public ``"castAs" can process null reference with another type`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null
    let (expectedObj: array<int32>) = null

    // Act.
    let actualObj = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"castAs" converts non-null empty object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<array<int32>>

    actualObj |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert empty object to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<int32> |> seq
    let (expectedObj: array<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"castAs" converts non-null object with predefined items to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<array<int32>>

    actualObj |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert object with predefined items to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |] |> seq
    let (expectedObj: array<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"castAs" converts non-null object with some items to the specified type``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<array<int32>>

    actualObj |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``If "castAs" cannot convert object with some items to the specified type, it returns the null reference``
    (count: int32) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let (expectedObj: array<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Random Values

[<Fact>]
let public ``"castAs" converts non-null object with random items to the specified type`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<array<int32>>

    actualObj |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert object with random items to the specified type, it returns the null reference`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count |> seq
    let (expectedObj: array<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"castAs" should only cast (i.g. change type) of value without iteration`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)

    // Act.
    let actualObj = Utils.castAs<array<int32>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualObj |> should not' Null
    actualObj |> should be ofExactType<array<int32>>

    actualObj |> should sameAs collection

[<Fact>]
let public ``"castAs" should only try to cast (i.g. change type) of value without iteration in case of failure`` () =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let (expectedObj: array<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<string>> collection

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

/// endregion
