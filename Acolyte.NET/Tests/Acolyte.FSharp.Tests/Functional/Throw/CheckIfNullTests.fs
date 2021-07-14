module Acolyte.Functional.Tests.Throw.CheckIfNullTests


open System
open Acolyte.Functional
open FsUnit.Xunit
open Swensen.Unquote
open Xunit

[<Fact>]
let public ``"checkIfNull" returns unit and throws no exception if object is not null`` () =
    // Arrange.
    let objToCheck = obj()
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNull objToCheck (nameof objToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"checkIfNull" throws an exception when object is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull null "nullObj" @>

[<Fact>]
let public ``"checkIfNull" throws an exception when name of object is null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull expectedObj null @>

[<Fact>]
let public ``"checkIfNull" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull null null @>
