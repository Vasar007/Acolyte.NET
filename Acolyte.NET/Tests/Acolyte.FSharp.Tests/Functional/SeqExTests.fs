module Acolyte.Functional.Tests.SeqExTests

open System
open Swensen.Unquote
open Xunit
open Acolyte.Tests
open Acolyte.Functional


/// region: Tests for "skipSafe" method.

[<Fact>]
let ``"skipSafe" throw an exception if argument is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ SeqEx.skipSafe Unchecked.defaultof<int> null @>

[<Theory>]
[<InlineData(TestConstants._0)>]
[<InlineData(TestConstants.Negative_1)>]
[<InlineData(TestConstants.Negative_2)>]
[<InlineData(TestConstants.Negative_5)>]
[<InlineData(TestConstants.Negative_10)>]
[<InlineData(TestConstants.Negative_100)>]
[<InlineData(TestConstants.Negative_10_000)>]
let ``"skipSafe" does not skip any items if "count" parameter is not positive``
    (count: int32) =
    // Arrange.
    let expectedSeq = [ 1..3 ]

    // Act.
    let actualSeq = expectedSeq
                    |> SeqEx.skipSafe count
                    |> Seq.toList

    // Assert.
    Assert.NotNull(actualSeq)
    Assert.NotEmpty(actualSeq)
    Assert.Equal<seq<int>>(expectedSeq, actualSeq)

[<Theory>]
[<InlineData(TestConstants._1)>]
[<InlineData(TestConstants._2)>]
[<InlineData(TestConstants._5)>]
[<InlineData(TestConstants._10)>]
[<InlineData(TestConstants._100)>]
[<InlineData(TestConstants._10_000)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is less than collection length``
    (count: int32) =
    // Arrange.
    let length = count + 1
    let originalSeq = [ 0..length ]

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe count
                    |> Seq.toList

    // Assert.
    Assert.NotNull(actualSeq)
    Assert.NotEmpty(actualSeq)

    let expectedSeq = originalSeq
                      |> Seq.skip count
                      |> Seq.toList
    Assert.Equal<seq<int>>(expectedSeq, actualSeq)

[<Theory>]
[<InlineData(TestConstants._1)>]
[<InlineData(TestConstants._2)>]
[<InlineData(TestConstants._5)>]
[<InlineData(TestConstants._10)>]
[<InlineData(TestConstants._100)>]
[<InlineData(TestConstants._10_000)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is equal to collection length``
    (count: int32) =
    // Arrange.
    let length = count
    let originalSeq = [ 1..length ]

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe count
                    |> Seq.toList

    // Assert.
    Assert.NotNull(actualSeq)
    Assert.Empty(actualSeq)

[<Theory>]
[<InlineData(TestConstants._1)>]
[<InlineData(TestConstants._2)>]
[<InlineData(TestConstants._5)>]
[<InlineData(TestConstants._10)>]
[<InlineData(TestConstants._100)>]
[<InlineData(TestConstants._10_000)>]
let ``"skipSafe" can skip specified number of items if "count" parameter is greater than collection length``
    (count: int32) =
    // Arrange.
    let length = count - 1
    let originalSeq = [ 0..length ]

    // Act.
    let actualSeq = originalSeq
                    |> SeqEx.skipSafe count
                    |> Seq.toList

    // Assert.
    Assert.NotNull(actualSeq)
    Assert.Empty(actualSeq)

/// endregion
