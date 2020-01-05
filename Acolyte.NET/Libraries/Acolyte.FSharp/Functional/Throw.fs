/// <summary>
/// Represents F#-style usage of assertion extensions from
/// <see cref="Acolyte.Assertions.ThrowsExtensions" />.
/// </summary>
module Acolyte.Functional.Throw

open Acolyte.Assertions


let ifNullValue value (paramName: string) (assertOnPureValueTypes: bool) =
    paramName.ThrowIfNull("paramName") |> ignore // Replace with nameof operator which still does not compile now.
    value.ThrowIfNullValue(paramName, assertOnPureValueTypes) |> ignore

let ifNull obj (paramName: string) =
    paramName.ThrowIfNull("paramName") |> ignore // Replace with nameof operator which still does not compile now.
    obj.ThrowIfNull(paramName) |> ignore
