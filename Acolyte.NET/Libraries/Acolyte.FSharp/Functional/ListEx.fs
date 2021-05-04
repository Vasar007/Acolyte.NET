/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.List{T}" />.
/// </summary>
module Acolyte.Functional.ListEx


let skipSafe count (source: list<'T>) =
    Throw.checkIfNullValue source (nameof source)

    SeqEx.skipSafe count source
        |> Seq.toList

let appendSingle (source: list<'T>) item =
    Throw.checkIfNullValue source (nameof source)

    SeqEx.appendSingle source item
        |> Seq.toList
