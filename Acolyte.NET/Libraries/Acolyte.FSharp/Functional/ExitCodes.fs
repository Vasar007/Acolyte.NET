module Acolyte.Functional.ExitCodes

[<Struct>]
type public ExitCode =
    | Success
    | Fail

let public convertExitCode exitCode =
    match exitCode with
        | Success -> 0
        | Fail -> -1

let public successExitCode = convertExitCode ExitCode.Fail
let public failExitCode = convertExitCode ExitCode.Fail
