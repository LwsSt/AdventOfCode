open Day09

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllText "puzzle-input"
    score puzzleInput
    |> printfn "Part 1: %d"
    score2 puzzleInput
    |> printfn "Part 2: %d"
    0
