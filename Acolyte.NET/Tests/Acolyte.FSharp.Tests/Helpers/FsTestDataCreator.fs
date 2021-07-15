﻿module Acolyte.Functional.Tests.Helpers.FsTestDataCreator

open Acolyte.Functional.Collections
open Acolyte.Tests.Creators


let internal createRandomInt32Seq (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> SeqEx.asSeq

let internal createRandomInt32List (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> Seq.toList

let internal createRandomInt32Array (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> Seq.toArray
