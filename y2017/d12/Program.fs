open Day12

[<EntryPoint>]
let main argv =
    System.IO.File.ReadLines "puzzle-input"
    |> plumb
    |> printfn "Part 1: %d"
    0
