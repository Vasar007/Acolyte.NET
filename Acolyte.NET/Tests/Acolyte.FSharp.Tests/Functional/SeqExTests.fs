module Acolyte.Functional.Tests.SeqExTests

open System
open Swensen.Unquote
open Xunit
open Acolyte.Functional


/// region: Tests for "skipSafe" method.

[<Fact>]
let ``Method "skipSafe" throw an exception if argument is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ SeqEx.skipSafe Unchecked.defaultof<int> null @>

[<Theory>]
[<InlineData(0)>]
[<InlineData(-1)>]
[<InlineData(-2)>]
[<InlineData(-5)>]
[<InlineData(-10)>]
[<InlineData(-100)>]
let ``Method "skipSafe" does not skip any items if "count" parameter is not positive``
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
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(5)>]
[<InlineData(10)>]
[<InlineData(100)>]
let ``Method "skipSafe" can skip specified number of items if "count" parameter is less than collection length``
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
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(5)>]
[<InlineData(10)>]
[<InlineData(100)>]
let ``Method "skipSafe" can skip specified number of items if "count" parameter is equal to collection length``
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
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(5)>]
[<InlineData(10)>]
[<InlineData(100)>]
let ``Method "skipSafe" can skip specified number of items if "count" parameter is greater than collection length``
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