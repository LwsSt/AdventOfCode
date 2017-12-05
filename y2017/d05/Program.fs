// Learn more about F# at http://fsharp.org

open System
open Day05

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines "puzzle-input"
    puzzleInput
    |> Seq.map int
    |> Seq.toArray
    |> escape
    |> printfn "Part 1: %d"
    0 // return an integer exit code
