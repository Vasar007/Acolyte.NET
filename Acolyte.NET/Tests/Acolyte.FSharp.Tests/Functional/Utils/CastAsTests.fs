module Acolyte.Functional.Tests.Utils.CastAsTests


open Xunit
open Acolyte.Functional

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
