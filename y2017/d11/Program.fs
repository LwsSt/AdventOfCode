open Day11

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllText "puzzle-input"
    puzzleInput.Split [|','|]
    |> traverse
    |> printfn "Part 1: %d"
    puzzleInput.Split [|','|] 
    |> List.ofArray
    |> traverse2
    |> printfn "Part 2: %d"
    0
