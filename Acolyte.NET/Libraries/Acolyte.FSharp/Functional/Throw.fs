module Acolyte.Functional.Throw

open Acolyte.Assertions


let ifNullValue value (paramName: string) (assertOnPureValueTypes: bool) =
    paramName.ThrowIfNull("paramName") |> ignore // Replace with nameof operator which still does not compile now.
    value.ThrowIfNullValue(paramName, assertOnPureValueTypes) |> ignore

let ifNull obj (paramName: string) =
    paramName.ThrowIfNull("paramName") |> ignore // Replace with nameof operator which still does not compile now.
    obj.ThrowIfNull(paramName) |> ignore
