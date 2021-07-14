module Acolyte.Functional.Tests.Utils.CastAsTests


open Acolyte.Functional
open Acolyte.Functional.Collections
open FsUnit.Xunit
open Xunit

[<Fact>]
let public ``"castAs" converts non-null object to the specified type`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> |> SeqEx.asSeq

    // Act.
    let actualObj = Utils.castAs<array<string>> arrayAsEnumerable

    // Assert.
    actualObj |> should not' Null

    actualObj |> should be ofExactType<array<string>>
    actualObj |> should sameAs arrayAsEnumerable

[<Fact>]
let public ``If "castAs" cannot convert object to the specified type, it returns the null reference`` () =
    // Arrange.
    let arrayAsEnumerable = Array.empty<string> |> SeqEx.asSeq
    let (expectedObj: array<int32>) = null

    // Act.
    let actualObj = Utils.castAs<array<int32>> arrayAsEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj

[<Fact>]
let public ``"castAs" can process null reference`` () =
    // Arrange.
    let (nullEnumerable: seq<string>) = null
    let (expectedObj: array<int32>) = null

    // Act.
    let actualObj = Utils.castAs<array<int32>> nullEnumerable

    // Assert.
    actualObj |> should be Null
    actualObj |> should sameAs expectedObj
