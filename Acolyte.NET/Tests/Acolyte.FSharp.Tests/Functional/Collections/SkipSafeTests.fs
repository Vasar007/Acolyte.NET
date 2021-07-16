module Acolyte.Functional.Tests.Collections.SkipSafeTests

open System
open Acolyte.Functional.Collections
open Acolyte.Functional.Tests.Cases.Collections.SkipSafe
open Acolyte.Functional.Tests.Helpers
open Acolyte.Tests.Collections
open Acolyte.Tests.Creators
open FsUnit.Xunit
open Swensen.Unquote
open Xunit


/// region: Null Values

[<Theory>]
[<ClassData(typeof<SkipSafeTestCases<int32>>)>]
let public ``"skipSafe" throw an exception if argument "source" is null``
    (parameters: TestCaseParameters<int32>) =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ parameters.ActualFactory Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Theory>]
[<ClassData(typeof<SkipSafeTestCases<int32>>)>]
let public ``"skipSafe" does not do anything if collection is empty``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let emptySeq = Seq.empty |> parameters.Conversion
    let skipCount = 1

    // Act.
    let actualSeq = emptySeq |> parameters.ActualFactory skipCount

    // Assert.
    actualSeq |> should be Empty

/// endregion

/// region: Predefined Values

[<Theory>]
[<ClassData(typeof<SkipSafeTestCases<int32>>)>]
let public ``"skipSafe" skips some items for prefefined collection``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let predefinedSeq = [ 1..3 ]
                        |> seq
                        |> parameters.Conversion
    let skipCount = 1
    let expectedSeq = predefinedSeq
                      |> parameters.ExpectedFactory skipCount
                      |> Seq.toList

    // Act.
    let actualSeq = predefinedSeq
                    |> parameters.ActualFactory skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<SkipSafeWithNegativeAndZeroTestCases<int32>>)>]
let public ``"skipSafe" does not skip any items if "count" parameter is not positive``
    (parameters: TestCaseParametersWithSkipCount<int32>) =
    // Arrange.
    let originalSeq = [ 1..3 ]
                      |> seq
                      |> parameters.Common.Conversion
    let expectedSeq = originalSeq |> Seq.toList

    // Act.
    let actualSeq = originalSeq
                    |> parameters.Common.ActualFactory parameters.SkipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<SkipSafeWithPositiveTestCases<int32>>)>]
let public ``"skipSafe" can skip specified number of items if "count" parameter is less than collection length``
    (parameters: TestCaseParametersWithSkipCount<int32>) =
    // Arrange.
    let length = parameters.SkipCount + 1
    let originalSeq = [ 0..length ]
                      |> seq
                      |> parameters.Common.Conversion
    let expectedSeq = originalSeq
                      |> parameters.Common.ExpectedFactory parameters.SkipCount
                      |> Seq.toList

    // Act.
    let actualSeq = originalSeq
                    |> parameters.Common.ActualFactory parameters.SkipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<SkipSafeWithPositiveTestCases<int32>>)>]
let public ``"skipSafe" can skip specified number of items if "count" parameter is equal to collection length``
    (parameters: TestCaseParametersWithSkipCount<int32>) =
    // Arrange.
    let length = parameters.SkipCount
    let originalSeq = [ 1..length ]
                      |> seq
                      |> parameters.Common.Conversion

    // Act.
    let actualSeq = originalSeq
                    |> parameters.Common.ActualFactory parameters.SkipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

[<Theory>]
[<ClassData(typeof<SkipSafeWithPositiveTestCases<int32>>)>]
let public ``"skipSafe" can skip specified number of items if "count" parameter is greater than collection length``
    (parameters: TestCaseParametersWithSkipCount<int32>) =
    // Arrange.
    let length = parameters.SkipCount - 1
    let originalSeq = [ 0..length ]
                      |> seq
                      |> parameters.Common.Conversion

    // Act.
    let actualSeq = originalSeq
                    |> parameters.Common.ActualFactory parameters.SkipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

/// endregion

/// region: Random Values

[<Theory>]
[<ClassData(typeof<SkipSafeTestCases<int32>>)>]
let public ``"skipSafe" skips or does not skip items for random collection``
    (parameters: TestCaseParameters<int32>) =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = FsTestDataCreator.createRandomInt32Seq count
                     |> parameters.Conversion
    let skipCount = TestDataCreator.CreateRandomInt32()

    // Act.
    let actualSeq = randomdSeq
                    |> parameters.ActualFactory skipCount
                    |> Seq.toList

    // Assert.
    if skipCount > count then
        actualSeq |> should be Empty
    else
        let expectedSeq = randomdSeq
                          |> parameters.ExpectedFactory skipCount
                          |> Seq.toList
        actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let public ``"skipSafe" should iterate through the whole collection but skip specified number of items from source collection`` () =
    // Arrange.
    let collection = [ 1..4 ]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let skipCount = 2
    let expectedSeq = collection
                      |> Seq.skip skipCount
                      |> Seq.toList

    // Act.
    let actualSeq = explosive
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq
    CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection))

/// endregion
