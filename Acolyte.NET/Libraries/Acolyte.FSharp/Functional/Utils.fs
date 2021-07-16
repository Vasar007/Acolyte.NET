/// <summary>
/// Provides useful methods to work with F# values.
/// </summary>
module Acolyte.Functional.Utils

open System


let public castAs<'T when 'T : null> (obj: obj) =
    match obj with
        | :? 'T as res -> res
        | _            -> null

let public castAsOption<'T> (obj: obj) =
    match obj with
        | :? 'T as res -> Some(res)
        | _            -> None

let public nullableToOption (nullable : Nullable<_>) =
    if nullable.HasValue then
        Some nullable.Value
    else
        None
