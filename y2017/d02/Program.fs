open System.IO

open Day02

[<EntryPoint>]
let main argv =
    let puzzleInput = File.ReadAllLines "puzzle-input"
    checksum2 puzzleInput |> printfn "Part 2: %d"
    checksum1 puzzleInput |> printfn "Part 1: %d"
    0
