module Acolyte.Functional.Tests.Throw.IfNullValueTests

open System
open Acolyte.Functional
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


[<Fact>]
let public ``"ifNullValue" returns the same value and throws no exception if value is not null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act.
    let actualValue = Throw.ifNullValue expectedValue (nameof expectedValue)

    // Assert.
    actualValue |> should not' Null
    actualValue |> should sameAs expectedValue

[<Fact>]
let public ``"ifNullValue" throws an exception when value is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue null "nullValue" @>

[<Fact>]
let public ``"ifNullValue" throws an exception when name of value is null`` () =
    // Arrange.
    let expectedValue = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue expectedValue null @>

[<Fact>]
let public ``"ifNullValue" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNullValue null null @>
