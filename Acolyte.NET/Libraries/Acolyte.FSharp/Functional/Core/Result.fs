module Acolyte.Functional.Core.Result

let public bindAsync fAsync result =
    async {
        match result with
            | Ok ok -> return! fAsync ok
            | Error error -> return Error error
    }

let public mapAsync fAsync result =
    async {
        match result with
            | Ok ok ->
                let! newOk = fAsync ok
                return Ok newOk
            | Error error -> return Error error
    }
