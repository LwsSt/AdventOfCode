open Day02
open System.IO

[<EntryPoint>]
let main argv =
    let puzzleInput = File.ReadAllLines("puzzle-input") |> Array.toList
    printfn "Day02 Part 1: %i" (puzzle1 puzzleInput)
    0
