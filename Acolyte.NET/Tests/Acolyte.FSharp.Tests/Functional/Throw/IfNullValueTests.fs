module Acolyte.Functional.Tests.Throw.IfNullValueTests


open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional

[<Fact>]
let ``"ifNullValue" returns the same value and throws no exception if value is not null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act.
    let actualValue = Throw.ifNullValue expectedValue (nameof expectedValue)

    // Assert.
    Assert.NotNull(actualValue)
    Assert.Same(expectedValue, actualValue)

[<Fact>]
let ``"ifNullValue" throws an exception when value is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue null "nullValue" @>

[<Fact>]
let ``"ifNullValue" throws an exception when name of value is null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue expectedValue null @>

[<Fact>]
let ``"ifNullValue" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue null null @>
