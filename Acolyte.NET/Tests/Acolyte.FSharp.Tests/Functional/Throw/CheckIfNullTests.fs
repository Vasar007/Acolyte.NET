module Acolyte.Functional.Tests.Throw.CheckIfNullTests

open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional

[<Fact>]
let ``"checkIfNull" returns unit and throws no exception if object is not null`` () =
    // Arrange.
    let objToCheck = obj()

    // Act.
    let actualResult = Throw.checkIfNull objToCheck (nameof objToCheck)

    // Assert.
    let expectedResult = ()
    Assert.Same(expectedResult, actualResult)

[<Fact>]
let ``"checkIfNull" throws an exception when object is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull null "nullObj" @>

[<Fact>]
let ``"checkIfNull" throws an exception when name of object is null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull expectedObj null @>

[<Fact>]
let ``"checkIfNull" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNull null null @>
