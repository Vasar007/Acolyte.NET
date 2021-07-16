﻿module Acolyte.Functional.Tests.Throw.IfNullTests

open System
open Acolyte.Functional
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


/// region: Null Values

[<Fact>]
let public ``"ifNull" returns the same object and throws no exception if value is not null`` () =
    // Arrange.
    let expectedResult = obj()

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"ifNull" throws an exception when value is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull null "nullValue" @>

[<Fact>]
let public ``"ifNull" throws an exception when name of value is null`` () =
    // Arrange.
    let valueToCheck = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.ifNull valueToCheck null @>

[<Fact>]
let public ``"ifNull" performs null check name of value at first`` () =
    // Arrange.
    let throwFunc = fun () -> Throw.ifNull null null

    // Act & Assert.
    throwFunc
        |> FsTestHelper.funcShouldThrowWithParamName<ArgumentNullException> "paramName"
        |> ignore

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"ifNull" returns unit and throws no exception if value is empty`` () =
    // Arrange.
    let expectedResult = Array.empty

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"ifNull" returns unit and throws no exception if value has predefined items`` () =
    // Arrange.
    let expectedResult = [| 1..3 |]

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"ifNull" returns unit and throws no exception if value has predefined nullable items`` () =
    // Arrange.
    let expectedResult = [| Some 1; Some 2; None; Some 3 |]

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"ifNull" returns unit and throws no exception if value has some items``
    (count: int32) =
    // Arrange.
    let expectedResult = FsTestDataCreator.createRandomInt32Array count

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"ifNull" returns unit and throws no exception if value has some nullable items``
    (count: int32) =
    // Arrange.
    let expectedResult = FsTestDataCreator.createRandomNullableInt32Array count

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Random Values

[<Fact>]
let public ``"ifNull" returns unit and throws no exception if value has random items`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let expectedResult = FsTestDataCreator.createRandomInt32Array count

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"ifNull" returns unit and throws no exception if value has random nullable items`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let expectedResult = FsTestDataCreator.createRandomNullableInt32Array count

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"ifNull" should only perfron null-check value without iteration`` () =
    // Arrange.
    let expectedResult = [| 1..3 |]

    // Act.
    let actualResult = Throw.ifNull expectedResult (nameof expectedResult)

    // Assert.
    actualResult |> should not' Null
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"ifNull" should only perfron null-check value with nullable items without iteration`` () =
    // Arrange.
    let collection = [| Some 1; Some 2; None; Some 3 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)

    // Act.
    let actualResult = Throw.ifNull explosive (nameof explosive)

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualResult |> should not' Null
    actualResult |> should sameAs collection

/// endregion
