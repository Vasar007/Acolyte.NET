/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.Array{T}" />.
/// </summary>
module Acolyte.Functional.ArraytEx


let skipSafe count (source: 'T[]) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toArray

let appendSingle item (source: 'T[]) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.appendSingle item
        |> Seq.toArray
