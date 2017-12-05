// Learn more about F# at http://fsharp.org

open System
open Day05

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input"
    let instructions = 
        puzzleInput
        |> Seq.map int
    instructions
    |> Seq.toArray
    |> escape  
    |> printfn "Part 1: %d"
    instructions
    |> Seq.toArray
    |> escape2
    |> printfn "Part 2: %d"
    0 // return an integer exit code
