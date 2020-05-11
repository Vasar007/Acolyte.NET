module Acolyte.Functional.Tests.SeqExTests

open System
open Swensen.Unquote
open Xunit
open Acolyte.Tests
open Acolyte.Functional


/// region: Tests for "skipSafe" method.

[<Fact>]
let ``Method "skipSafe" throw an exception if argument is null`` () =
    // Arrange & Act & Assert.
    raises<ArgumentNullException> <@ SeqEx.skipSafe Unchecked.defaultof<int> null @>

[<Theory>]
[<InlineData(TestHelper.ZeroCollectionSize)>]
[<InlineData(TestHelper.NegativeOneCollectionSize)>]
[<InlineData(TestHelper.NegativeTwoCollectionSize)>]
[<InlineData(TestHelper.NegativeFiveCollectionSie)>]
[<InlineData(TestHelper.NegativeTenCollectionSize)>]
[<InlineData(TestHelper.NegativeHundredCollectionSize)>]
[<InlineData(TestHelper.NegativeTenThousandCollectionSize)>]
[<InlineData(TestHelper.NegativeMaxCollectionSize)>]
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
[<InlineData(TestHelper.OneCollectionSize)>]
[<InlineData(TestHelper.TwoCollectionSize)>]
[<InlineData(TestHelper.FiveCollectionSie)>]
[<InlineData(TestHelper.TenCollectionSize)>]
[<InlineData(TestHelper.HundredCollectionSize)>]
[<InlineData(TestHelper.TenThousandCollectionSize)>]
[<InlineData(TestHelper.MaxCollectionSize)>]
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
[<InlineData(TestHelper.OneCollectionSize)>]
[<InlineData(TestHelper.TwoCollectionSize)>]
[<InlineData(TestHelper.FiveCollectionSie)>]
[<InlineData(TestHelper.TenCollectionSize)>]
[<InlineData(TestHelper.HundredCollectionSize)>]
[<InlineData(TestHelper.TenThousandCollectionSize)>]
[<InlineData(TestHelper.MaxCollectionSize)>]
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
[<InlineData(TestHelper.OneCollectionSize)>]
[<InlineData(TestHelper.TwoCollectionSize)>]
[<InlineData(TestHelper.FiveCollectionSie)>]
[<InlineData(TestHelper.TenCollectionSize)>]
[<InlineData(TestHelper.HundredCollectionSize)>]
[<InlineData(TestHelper.TenThousandCollectionSize)>]
[<InlineData(TestHelper.MaxCollectionSize)>]
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
