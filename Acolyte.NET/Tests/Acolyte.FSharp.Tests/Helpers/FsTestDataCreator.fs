module Acolyte.Functional.Tests.Helpers.FsTestDataCreator

open Acolyte.Tests.Creators
open Acolyte.Functional


/// region: Create String List

let internal createRandomStringSeq (count: int32) =
    TestDataCreator.CreateRandomStringList count
        |> seq

let internal createRandomStringList (count: int32) =
    TestDataCreator.CreateRandomStringList count
        |> Seq.toList

let internal createRandomStringArray (count: int32) =
    TestDataCreator.CreateRandomStringList count
        |> Seq.toArray

let internal createRandomNullableStringSeq (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> seq

let internal createRandomNullableStringList (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> Seq.toList

let internal createRandomNullableStringArray (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> Seq.toArray

let internal createRandomOptionStringSeq (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> Seq.map Utils.nullableReferenceToOption

let internal createRandomOptionStringList (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> Seq.map Utils.nullableReferenceToOption
        |> Seq.toList

let internal createRandomOptionStringArray (count: int32) =
    TestDataCreator.CreateRandomNullableStringList count
        |> Seq.map Utils.nullableReferenceToOption
        |> Seq.toArray

/// endregion

/// region: Create Int32 List

let internal createRandomInt32Seq (count: int32) =
    TestDataCreator.CreateRandomInt32List count
        |> seq

let internal createRandomInt32List (count: int32) =
    TestDataCreator.CreateRandomInt32List count
        |> Seq.toList

let internal createRandomInt32Array (count: int32) =
    TestDataCreator.CreateRandomInt32List count
        |> Seq.toArray

let internal createRandomNullableInt32Seq (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> seq

let internal createRandomNullableInt32List (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> Seq.toList

let internal createRandomNullableInt32Array (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> Seq.toArray

let internal createRandomOptionInt32Seq (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> Seq.map Utils.nullableValueToOption

let internal createRandomOptionInt32List (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> Seq.map Utils.nullableValueToOption
        |> Seq.toList

let internal createRandomOptionInt32Array (count: int32) =
    TestDataCreator.CreateRandomNullableInt32List count
        |> Seq.map Utils.nullableValueToOption
        |> Seq.toArray

/// endregion
