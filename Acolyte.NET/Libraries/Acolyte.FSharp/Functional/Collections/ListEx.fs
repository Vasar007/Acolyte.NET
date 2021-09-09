/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.List{T}" />.
/// </summary>
module Acolyte.Functional.Collections.ListEx


open Acolyte.Functional

let public skipSafe count (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toList

let public appendSingleton item (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.appendSingleton item
        |> Seq.toList
