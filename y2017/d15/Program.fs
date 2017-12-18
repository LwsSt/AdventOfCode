open Day15

[<EntryPoint>]
let main argv =
    judgeGenerators 618L 814L
    |> printfn "Part 1: %d"
    judgeGenerators2 618L 814L
    |> printfn "Part 2: %d"
    0
