﻿module Acolyte.Functional.Tests.Helpers.FsTestDataCreator


open Acolyte.Tests.Creators

let createRandomInt32Seq (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> Seq.cast

let createRandomInt32List (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> Seq.toList

let createRandomInt32Array (count: int32) =
    TestDataCreator.CreateRandomInt32List(count) |> Seq.toArray
