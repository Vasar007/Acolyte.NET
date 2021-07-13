/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.Array{T}" />.
/// </summary>
module Acolyte.Functional.ArrayEx

open System


let skipSafe count (source: 'T[]) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toArray

let appendSingleton item (source: 'T[]) =
    Throw.checkIfNull source (nameof source)

    source
        |> SeqEx.appendSingleton item
        |> Seq.toArray

[<Obsolete("Use \Acolyte.Functional.ArraytEx.appendSingleton\" instead. This method will be remove in next major version.", false)>]
let appendSingle item source =
    appendSingleton item source
