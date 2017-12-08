open Day08

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input" |> List.ofArray
    puzzleInput
    |> runRegisters
    |> printfn "Part 1: %d"
    0
