module Acolyte.Functional.Tests.Throw.CheckIfNullValueTests


open System
open Acolyte.Functional
open FsUnit.Xunit
open Swensen.Unquote
open Xunit

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value is not null`` () =
    // Arrange.
    let valueToCheck = obj()
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"checkIfNullValue" throws an exception when value is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue null "nullValue" @>

[<Fact>]
let public ``"checkIfNullValue" throws an exception when name of value is null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue expectedValue null @>

[<Fact>]
let public ``"checkIfNullValue" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue null null @>
