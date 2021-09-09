module Acolyte.Functional.Core.Result

// TODO: write test for this method.
let public bindAsync fAsync result =
    async {
        match result with
            | Ok ok -> return! fAsync ok
            | Error error -> return Error error
    }

// TODO: write test for this method.
let public mapAsync fAsync result =
    async {
        match result with
            | Ok ok ->
                let! newOk = fAsync ok
                return Ok newOk
            | Error error -> return Error error
    }
