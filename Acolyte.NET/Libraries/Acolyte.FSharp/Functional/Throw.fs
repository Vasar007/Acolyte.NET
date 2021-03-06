﻿/// <summary>
/// Represents F#-style usage of assertion extensions.
/// </summary>
module Acolyte.Functional.Throw


let private (|NotNull|_|) value = 
    if obj.ReferenceEquals(value, null) then None
    else Some()

let ifNullValue value (paramName: string) =
    if isNull paramName then
        nullArg (nameof paramName)

    match value with
        | NotNull -> ()
        | _       -> nullArg paramName

    value

let ifNull obj (paramName: string) =
    if isNull paramName then
        nullArg (nameof paramName)

    if isNull obj then
        nullArg paramName

    obj

let checkIfNullValue value (paramName: string) =
    ifNullValue value paramName |> ignore

let checkIfNull obj (paramName: string) =
    ifNull obj paramName |> ignore
