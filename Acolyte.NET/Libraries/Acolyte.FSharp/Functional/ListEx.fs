/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.List{T}" />.
/// </summary>
module Acolyte.Functional.ListEx

open System


let skipSafe count (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.skipSafe count
        |> Seq.toList


let appendSingleton item (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    source
        |> SeqEx.appendSingleton item
        |> Seq.toList

[<Obsolete("Use \Acolyte.Functional.ListEx.appendSingleton\" instead. This method will be remove in next major version.", false)>]
let appendSingle item source =
    appendSingleton item source
