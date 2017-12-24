open Day18

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input" |> Array.toList
    run puzzleInput
    |> printfn "Part 1: %d"
    0 // return an integer exit code
