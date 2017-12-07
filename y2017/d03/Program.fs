open Day03

[<EntryPoint>]
let main argv =
    memory 368078
    |> printfn "Part 1: %d"
    memory2 368078
    |> printfn "Part 2: %d"
    0