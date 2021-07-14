module Acolyte.Functional.Option

let public bindAsync fAsync option =
    async {
        match option with
            | Some some -> return! fAsync some
            | None -> return None
    }

let public mapAsync fAsync option =
    async {
        match option with
            | Some some ->
                let! newSome = fAsync some
                return Some newSome
            | None -> return None
    }
