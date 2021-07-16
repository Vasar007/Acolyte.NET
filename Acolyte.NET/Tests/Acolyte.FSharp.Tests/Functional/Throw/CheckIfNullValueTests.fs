module Acolyte.Functional.Tests.Throw.CheckIfNullValueTests

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
    let valueToCheck = obj()

    // Act & Assert.
    raises<ArgumentNullException> <@ Throw.checkIfNullValue valueToCheck null @>

[<Fact>]
let public ``"checkIfNullValue" performs null check name of value at first`` () =
    // Arrange.
    let throwFunc = fun () -> Throw.checkIfNullValue null null

    // Act & Assert.
    throwFunc
        |> FsTestHelper.actionShouldThrowWithParamName<ArgumentNullException> "paramName"
        |> ignore

/// endregion

/// region: Empty Values

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value is empty`` () =
    // Arrange.
    let valueToCheck = List.empty
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Predefined Values

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has predefined items`` () =
    // Arrange.
    let valueToCheck = [ 1..3 ]
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has predefined nullable items`` () =
    // Arrange.
    let valueToCheck = [ Some 1; Some 2; None; Some 3 ]
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has some items``
    (count: int32) =
    // Arrange.
    let valueToCheck = FsTestDataCreator.createRandomInt32List count
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has some nullable items``
    (count: int32) =
    // Arrange.
    let valueToCheck = FsTestDataCreator.createRandomNullableInt32List count
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Random Values

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has random items`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let valueToCheck = FsTestDataCreator.createRandomInt32List count
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"checkIfNullValue" returns unit and throws no exception if value has random nullable items`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let valueToCheck = FsTestDataCreator.createRandomNullableInt32List count
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"checkIfNullValue" should only perfron null-check value without iteration`` () =
    // Arrange.
    let valueToCheck = [ 1..3 ]
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Fact>]
let public ``"checkIfNullValue" should only perfron null-check value with nullable items without iteration`` () =
    // Arrange.
    let collection = [ Some 1; Some 2; None; Some 3 ]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue explosive (nameof explosive)

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualResult |> should sameAs expectedResult

/// endregion
