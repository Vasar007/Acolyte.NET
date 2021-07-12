module Acolyte.Functional.Tests.Throw.CheckIfNullValueTests


open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional

[<Fact>]
let ``"checkIfNullValue" returns unit and throws no exception if value is not null`` () =
    // Arrange.
    let valueToCheck = obj()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    let expectedResult = ()
    Assert.Same(expectedResult, actualResult)

[<Fact>]
let ``"checkIfNullValue" throws an exception when value is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue null "nullValue" @>

[<Fact>]
let ``"checkIfNullValue" throws an exception when name of value is null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue expectedValue null @>

[<Fact>]
let ``"checkIfNullValue" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue null null @>
