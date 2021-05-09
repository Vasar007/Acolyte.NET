module Acolyte.Functional.Result


let bindAsync fAsync result =
    async {
        match result with
            | Ok ok -> return! fAsync ok
            | Error error -> return Error error
    }

let mapAsync fAsync result =
    async {
        match result with
            | Ok ok ->
                let! newOk = fAsync ok
                return Ok newOk
            | Error error -> return Error error
    }
