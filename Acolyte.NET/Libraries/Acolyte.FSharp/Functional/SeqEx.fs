/// <summary>
/// Contains additional methods to work with <see cref="Microsoft.FSharp.Collections.seq{T}" />.
/// </summary>
[<System.Obsolete("Use \Acolyte.Functional.Collections.SeqEx.skipSafe\" instead. This method will be remove in next major version.", error = false)>]
module Acolyte.Functional.SeqEx


open System

[<Obsolete("Use \Acolyte.Functional.Collections.SeqEx.skipSafe\" instead. This method will be remove in next major version.", error = false)>]
let public skipSafe count (source: seq<'T>) =
    Acolyte.Functional.Collections.SeqEx.skipSafe count source

[<Obsolete("Use \Acolyte.Functional.Collections.SeqEx.appendSingleton\" instead. This method will be remove in next major version.", error = false)>]
let public appendSingle item source =
    Acolyte.Functional.Collections.SeqEx.appendSingleton item source
