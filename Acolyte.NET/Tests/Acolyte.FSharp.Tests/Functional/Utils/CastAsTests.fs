module Acolyte.Functional.Tests.Utils.CastAsTests

open Acolyte.Functional.Tests.Cases.Utils.CastAs
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Xunit


// Note: using array in tests to allow work with null reference.

/// region: Null Values

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<int32> >>)>]
let public ``"castAs" can process null reference``
    (parameters: TestCaseParameters< array<int32>, array<int32> >) =
    // Arrange.
    let (nullValue: array<int32>) = null

    // Act.
    let actualResult = parameters.ActualFactory nullValue

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<string> >>)>]
let public ``"castAs" can process null reference with another type``
    (parameters: TestCaseParameters< array<int32>, array<string> >) =
    // Arrange.
    let (nullValue: array<int32>) = null

    // Act.
    let actualResult = parameters.ActualFactory nullValue

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion

/// region: Empty Values

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<int32> >>)>]
let public ``"castAs" converts non-null empty object to the specified type``
    (parameters: TestCaseParameters< array<int32>, array<int32> >) =
    // Arrange.
    // Force array creation because "sameAs" can fail with pure "Array.empty" value
    let arrayAsEnumerable = Array.empty |> Seq.toArray

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult.IsSome |> should be True
    actualResult.Value |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<string> >>)>]
let public ``If "castAs" cannot convert empty object to the specified type, it returns the null reference``
    (parameters: TestCaseParameters< array<int32>, array<string> >) =
    // Arrange.
    let arrayAsEnumerable = Array.empty

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion

/// region: Predefined Values

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<int32> >>)>]
let public ``"castAs" converts non-null object with predefined items to the specified type``
    (parameters: TestCaseParameters< array<int32>, array<int32> >) =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |]

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult.IsSome |> should be True
    actualResult.Value |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<string> >>)>]
let public ``If "castAs" cannot convert object with predefined items to the specified type, it returns the null reference``
    (parameters: TestCaseParameters< array<int32>, array<string> >) =
    // Arrange.
    let arrayAsEnumerable = [| 1..3 |]

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<CastAsWithPositiveTestCases< array<int32>, array<int32> >>)>]
let public ``"castAs" converts non-null object with some items to the specified type``
    (parameters: TestCaseParametersWithCount< array<int32>, array<int32> >) =
    // Arrange.
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array parameters.Count

    // Act.
    let actualResult = parameters.Common.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult.IsSome |> should be True
    actualResult.Value |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<CastAsWithPositiveTestCases< array<int32>, array<string> >>)>]
let public ``If "castAs" cannot convert object with some items to the specified type, it returns the null reference``
    (parameters: TestCaseParametersWithCount< array<int32>, array<string> >) =
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array parameters.Count

    // Act.
    let actualResult = parameters.Common.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion

/// region: Random Values

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<int32> >>)>]
let public ``"castAs" converts non-null object with random items to the specified type``
    (parameters: TestCaseParameters< array<int32>, array<int32> >) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should not' Null
    actualResult.IsSome |> should be True
    actualResult.Value |> should sameAs arrayAsEnumerable

[<Theory>]
[<ClassData(typeof<CastAsTestCases< array<int32>, array<string> >>)>]
let public ``If "castAs" cannot convert object with random items to the specified type, it returns the null reference``
    (parameters: TestCaseParameters< array<int32>, array<string> >) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let arrayAsEnumerable = FsTestDataCreator.createRandomInt32Array count

    // Act.
    let actualResult = parameters.ActualFactory arrayAsEnumerable

    // Assert.
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion

/// region: Extended Logical Coverage

// We cast value, not iterate it. So, we can only have test for failed cast case.

[<Theory>]
[<ClassData(typeof<CastAsTestCases< seq<int32>, array<string> >>)>]
let public ``"castAs" should only try to cast (i.g. change type) of value without iteration in case of failure``
    (parameters: TestCaseParameters< seq<int32>, array<string> >) =
    // Arrange.
    let collection = [| 1..4 |]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)

    // Act.
    let actualResult = parameters.ActualFactory (explosive |> seq)

    // Assert.
    CustomAssert.True(explosive.VerifyNoIterationsNoGetEnumeratorCalls())
    actualResult |> should be Null
    actualResult.IsNone |> should be True

/// endregion
