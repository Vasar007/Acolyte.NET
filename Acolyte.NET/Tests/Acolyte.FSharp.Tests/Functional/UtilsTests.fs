module Acolyte.Functional.Tests.UtilsTests

open Xunit
open Acolyte.Functional


/// region: Tests for "castAs" method.

[<Fact>]
let ``"castAs" converts non-null object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> :> seq<string>

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    Assert.NotNull(actualObj)

    let expectedType = typeof<array<string>>
    Assert.IsType(expectedType, actualObj)
    Assert.Same(arrayAsEnumerable, actualObj)

[<Fact>]
let ``If "castAs" cannot convert object to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> :> seq<string>

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    Assert.Null(actualObj)

    let (expectedObj: array<int32>) = null
    Assert.Same(expectedObj, actualObj)

[<Fact>]
let ``"castAs" can process null reference`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null

    // Act.
    let actualObj = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    Assert.Null(actualObj)

    let (expectedObj: array<int32>) = null
    Assert.Same(expectedObj, actualObj)

/// endregion

/// region: Tests for "castAsOption" method.

[<Fact>]
let ``"castAsOption" converts object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> :> seq<string>

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    Assert.NotNull(actualObj)

    let expectedType = typeof<Option<array<string>>>
    Assert.IsType(expectedType, actualObj)

    Assert.True(actualObj.IsSome)
    Assert.Same(arrayAsEnumerable, actualObj.Value)

[<Fact>]
let ``If "castAsOption" cannot convert object to the specified type, it returns the None value`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> :> seq<string>

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    let expectedObj = Option<array<int32>>.None
    Assert.Null(actualObj)
    Assert.Same(expectedObj, actualObj)

[<Fact>]
let ``"castAsOption" can process null reference`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> nullEnumerable

    // Assert.
    Assert.Null(actualObj)

    let expectedObj = Option<array<int32>>.None
    Assert.Same(expectedObj, actualObj)

/// endregion
