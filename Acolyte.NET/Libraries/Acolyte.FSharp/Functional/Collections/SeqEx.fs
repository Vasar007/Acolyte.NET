/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.seq{T}" />.
/// </summary>
module Acolyte.Functional.Collections.SeqEx

open Acolyte.Functional


let public skipSafe count (source: seq<'T>) =
    Throw.checkIfNull source (nameof source)

    seq {
        use enumerator = source.GetEnumerator()
        let index = ref 0
        let loop = ref true
        while !index < count && !loop do
            if not (enumerator.MoveNext()) then
                loop := false
            index := !index + 1

        while enumerator.MoveNext() do
            yield enumerator.Current
    }

let public appendSingleton item source =
    Throw.checkIfNull source (nameof source)

    source
        |> Seq.append (Seq.singleton item)
