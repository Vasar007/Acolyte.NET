module Acolyte.Functional.Option


let bindAsync fAsync option =
    async {
        match option with
            | Some some -> return! fAsync some
            | None -> return None
    }

let mapAsync fAsync option =
    async {
        match option with
            | Some some ->
                let! newSome = fAsync some
                return Some newSome
            | None -> return None
    }
