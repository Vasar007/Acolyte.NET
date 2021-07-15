module Acolyte.Functional.Tests.Utils.CastAsOptionTests

open Acolyte.Functional
open Acolyte.Functional.Collections
open FsUnit.Xunit
open Xunit


[<Fact>]
let public ``"castAsOption" converts object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> |> seq

    // Act.
    let actualObj = Utils.castAsOption<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null
    actualObj |> should be ofExactType<Option<array<string>>>

    actualObj.IsSome |> should be True
    actualObj.Value |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAsOption" cannot convert object to the specified type, it returns the None value`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> |> seq
    let expectedObj = Option<array<int32>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

[<Fact>]
let public ``"castAsOption" can process null reference`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null
    let expectedObj = Option<array<int32>>.None

    // Act.
    let actualObj = Utils.castAsOption<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj
