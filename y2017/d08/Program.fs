open Day08

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input" |> List.ofArray
    puzzleInput
    |> runRegisters
    |> printfn "Part 1: %d"
    puzzleInput
    |> runRegisters2
    |> printfn "Part 2: %d"
    0
