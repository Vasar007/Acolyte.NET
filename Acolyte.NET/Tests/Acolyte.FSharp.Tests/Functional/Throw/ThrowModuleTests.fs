module Acolyte.Functional.Tests.Throw.ThrowModuleTests

open System
open Acolyte.Functional
open Acolyte.Functional.Tests.Cases.Throw.ThrowModule
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


// Note: using array in tests to allow work with null reference.

/// region: Null Values

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function returns unit and throws no exception if value is not null``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let valueToCheck = [| 1..3 |]
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function throws an exception when value is null``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ parameters.ActualFactory null "nullValue" @>

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function throws an exception when name of value is null``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let valueToCheck = [| 1..3 |]

    // Act & Assert.
    raises<ArgumentNullException> <@ parameters.ActualFactory valueToCheck null @>

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function performs null check name of value at first``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let throwFunc = fun () -> (parameters.ActualFactory null null) |> ignore

    // Act & Assert.
    throwFunc
        |> FsTestHelper.actionShouldThrowWithParamName<ArgumentNullException> "paramName"
        |> ignore

/// endregion

/// region: Empty Values

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function returns unit and throws no exception if value is empty``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    // Force array creation because "sameAs" can fail with pure "Array.empty" value
    let valueToCheck = Array.empty |> Seq.toArray
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Predefined Values

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has predefined items``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let valueToCheck = [| 1..3 |]
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<option<int32>> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has predefined nullable items``
    (parameters: TestCaseParameters< array<option<int32>> >) =
    // Arrange.
    let valueToCheck = [| Some 1; Some 2; None; Some 3 |]
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<ThrowModuleWithPositiveTestCases< array<int32> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has some items``
    (parameters: TestCaseParametersWithCount< array<int32> >) =
    // Arrange.
    let valueToCheck = FsTestDataCreator.createRandomInt32Array parameters.Count
    let expectedResult = parameters.Common.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.Common.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<ThrowModuleWithPositiveTestCases< array<option<int32>> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has some nullable items``
    (parameters: TestCaseParametersWithCount< array<option<int32>> >) =
    // Arrange.
    let valueToCheck = FsTestDataCreator.createRandomOptionInt32Array parameters.Count
    let expectedResult = parameters.Common.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.Common.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Random Values

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has random items``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let valueToCheck = FsTestDataCreator.createRandomInt32Array count
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<option<int32>> >>)>]
let public ``Throw.Function returns unit and throws no exception if value has random nullable items``
    (parameters: TestCaseParameters< array<option<int32>> >) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let valueToCheck = FsTestDataCreator.createRandomOptionInt32Array count
    let expectedResult = parameters.ExpectedFactory valueToCheck

    // Act.
    let actualResult = parameters.ActualFactory valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

/// endregion

/// region: Extended Logical Coverage

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function should only perfron null-check value without iteration``
    (parameters: TestCaseParameters< array<int32> >) =
    // Arrange.
    let valueToCheck = [ 1..3 ]
    let expectedResult = ()

    // Act.
    let actualResult = Throw.checkIfNullValue valueToCheck (nameof valueToCheck)

    // Assert.
    actualResult |> should sameAs expectedResult

[<Theory>]
[<ClassData(typeof<ThrowModuleTestCases< array<int32> >>)>]
let public ``Throw.Function should only perfron null-check value with nullable items without iteration``
    (parameters: TestCaseParameters< array<int32> >) =
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
