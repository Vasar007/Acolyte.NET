/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.Array{T}" />.
/// </summary>
module Acolyte.Functional.ArraytEx


let skipSafe count (source: 'T[]) =
    Throw.checkIfNull source (nameof source)

    SeqEx.skipSafe count source
        |> Seq.toArray

let appendSingle (source: 'T[]) item =
    Throw.checkIfNull source (nameof source)

    SeqEx.appendSingle source item
        |> Seq.toArray
