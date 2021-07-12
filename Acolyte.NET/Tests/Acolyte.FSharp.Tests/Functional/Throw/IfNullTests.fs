module Acolyte.Functional.Tests.Throw.IfNullTests


open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional

[<Fact>]
let ``"ifNull" returns the same object and throws no exception if object is not null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act.
    let actualObj = Throw.ifNull expectedObj (nameof expectedObj)

    // Assert.
    Assert.NotNull(actualObj)
    Assert.Same(expectedObj, actualObj)

[<Fact>]
let ``"ifNull" throws an exception when object is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull null "nullObj" @>
    
[<Fact>]
let ``"ifNull" throws an exception when name of object is null`` () =
    // Arrange.
    let expectedObj = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull expectedObj null @>

[<Fact>]
let ``"ifNull" performs null check name of value at first`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull null null @>
