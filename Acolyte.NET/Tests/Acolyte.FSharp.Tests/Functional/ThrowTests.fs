module Acolyte.Functional.Tests.ThrowTests

open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional


/// region: Tests for "ifNullValue" method.

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

/// endregion

/// region: Tests for "ifNull" method.

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

/// endregion

/// region: Tests for "checkIfNullValue" method.

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

/// endregion

/// region: Tests for "checkIfNull" method.

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

/// endregion
