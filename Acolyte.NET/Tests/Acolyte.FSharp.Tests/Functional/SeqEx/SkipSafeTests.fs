module Acolyte.Functional.Tests.SeqEx.SkipSafeTests


open System
open Acolyte.Tests.Cases.Parameterized
open Acolyte.Functional
open FsUnit.Xunit
open Swensen.Unquote
open Xunit
open Acolyte.Tests.Creators
open Acolyte.Tests.Collections

/// region: Null Values

[<Fact>]
let ``"skipSafe" throw an exception if argument is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ SeqEx.skipSafe Unchecked.defaultof<int> null @>

/// endregion

/// region: Empty Values

[<Fact>]
let ``"skipSafe" does not do anything if source is empty`` () =
    // Arrange.
    let emptySeq = Seq.empty
    let skipCount = 1

    // Act.
    let actualSeq = emptySeq |> SeqEx.skipSafe skipCount

    // Assert.
    actualSeq |> should be Empty

/// endregion

/// region: Predefined Values

[<Fact>]
let ``"skipSafe" skips some items for prefefined source`` () =
    // Arrange.
    let predefinedSeq = [ 1..3 ]
    let skipCount = 1
    let expectedSeq = predefinedSeq
                      |> Seq.skip skipCount
                      |> Seq.toList

    // Act.
    let actualSeq = predefinedSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should equal expectedSeq

/// endregion

/// region: Some Values

[<Theory>]
[<ClassData(typeof<NegativeWithZeroTestCases>)>]
let ``"skipSafe" does not skip any items if "count" parameter is not positive``
    (skipCount: int32) =
    // Arrange.
    let expectedSeq = [ 1..3 ]

    // Act.
    let actualSeq = expectedSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is less than collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount + 1
    let originalSeq = [ 0..length ]
    let expectedSeq = originalSeq
                      |> Seq.skip skipCount
                      |> Seq.toList

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should not' Empty
    actualSeq |> should equal expectedSeq

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is equal to collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount
    let originalSeq = [ 1..length ]

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

[<Theory>]
[<ClassData(typeof<PositiveTestCases>)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is greater than collection length``
    (skipCount: int32) =
    // Arrange.
    let length = skipCount - 1
    let originalSeq = [ 0..length ]

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    actualSeq |> should not' Null
    actualSeq |> should be Empty

/// endregion

/// region: Random Values

[<Fact>]
let ``"skipSafe" skips or does not skip items for random source`` () =
    // Arrange.
    let count = TestDataCreator.GetRandomCountNumber()
    let randomdSeq = TestDataCreator.CreateRandomInt32List(count)
    let skipCount = TestDataCreator.CreateRandomInt32()

    // Act.
    let actualSeq = randomdSeq
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    if skipCount > count then
        actualSeq |> should be Empty
    else
        let expectedSeq = randomdSeq
                         |> Seq.skip skipCount
                         |> Seq.toList
        actualSeq |> should equal expectedSeq

/// endregion

/// region: Extended Logical Coverage

[<Fact>]
let ``"skipSafe" should iterate through the whole collection but skip specified number of itmes from source`` () =
    // Arrange.
    let collection = [ 1..4 ]
    let explosive = ExplosiveEnumerable.CreateNotExplosive(collection)
    let skipCount = 2

    // Act.
    let actualSeq = explosive
                    |> SeqEx.skipSafe skipCount
                    |> Seq.toList

    // Assert.
    let expectedSeq = collection
                        |> Seq.skip skipCount
                        |> Seq.toList
    actualSeq |> should equal expectedSeq
    CustomAssert.True(explosive.VerifyOnceEnumerateWholeCollection(collection))

/// endregion
