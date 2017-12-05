// Learn more about F# at http://fsharp.org

open System
open Day05

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input"
    let instructions = 
        puzzleInput
        |> Seq.map int
        |> Seq.toArray
    escape instructions |> printfn "Part 1: %d"
    escape2 instructions |> printfn "Part 2: %d"
    0 // return an integer exit code
