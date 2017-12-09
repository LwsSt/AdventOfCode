open Day09

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllText "puzzle-input"
    score puzzleInput
    |> printfn "Part 1: %d"
    0
