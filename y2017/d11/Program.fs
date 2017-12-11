open Day11

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllText "puzzle-input"
    puzzleInput.Split [|','|]
    |> traverse
    |> printfn "Part 1: %d"
    0
