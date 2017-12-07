// Learn more about F# at http://fsharp.org

open Day06

[<EntryPoint>]
let main argv =
    let puzzleInput = "10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6"
    puzzleInput.Split [|'\t'|]
    |> Array.map int
    |> Array.toList
    |> escape
    |> printfn "Part 1: %d"
    0 // return an integer exit code
