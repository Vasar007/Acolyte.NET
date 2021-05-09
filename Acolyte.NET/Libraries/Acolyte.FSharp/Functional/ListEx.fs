/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.List{T}" />.
/// </summary>
module Acolyte.Functional.ListEx


let skipSafe count (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toList

let appendSingle item (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.appendSingle item
        |> Seq.toList
