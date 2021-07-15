module Acolyte.Functional.Tests.Throw.IfNullTests

open System
open Acolyte.Functional
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


[<Fact>]
let public ``"ifNull" returns the same object and throws no exception if object is not null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act.
    let actualObj = Throw.ifNull expectedObj (nameof expectedObj)

    // Assert.
    actualObj |> should not' Null
    actualObj |> should sameAs expectedObj

[<Fact>]
let public ``"ifNull" throws an exception when object is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull null "nullObj" @>
    
[<Fact>]
let public ``"ifNull" throws an exception when name of object is null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull expectedObj null @>

[<Fact>]
let public ``"ifNull" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull null null @>
