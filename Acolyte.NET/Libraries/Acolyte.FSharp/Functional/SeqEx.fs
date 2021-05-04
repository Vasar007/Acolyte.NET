﻿/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.seq{T}" />.
/// </summary>
module Acolyte.Functional.SeqEx


let skipSafe count (source: seq<'T>) =
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

let appendSingle source item =
    Throw.checkIfNull source (nameof source)

    Seq.append source (Seq.singleton item)
