module Acolyte.Functional.Core.Option

// TODO: write test for this method.
let public bindAsync fAsync option =
    async {
        match option with
            | Some some -> return! fAsync some
            | None -> return None
    }

// TODO: write test for this method.
let public mapAsync fAsync option =
    async {
        match option with
            | Some some ->
                let! newSome = fAsync some
                return Some newSome
            | None -> return None
    }
