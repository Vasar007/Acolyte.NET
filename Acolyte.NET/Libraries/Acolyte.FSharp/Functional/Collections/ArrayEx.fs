/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.Array{T}" />.
/// </summary>
module Acolyte.Functional.Collections.ArrayEx


open Acolyte.Functional

let public skipSafe count (source: array<'T>) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toArray

let public asSeq (source: array<'T>) =
    source :> seq<'T>

let public appendSingleton item (source: array<'T>) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.appendSingleton item
        |> Seq.toArray
