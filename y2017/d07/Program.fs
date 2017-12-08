open Day07

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input" |> Array.toList
    puzzleInput
    |> towers
    |> printfn "Part 1: %s"
    0
