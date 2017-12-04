open System.IO

open Day04

[<EntryPoint>]
let main argv =
    let puzzleInput = File.ReadAllLines "puzzle-input"
    let solutionPart1 =
        puzzleInput
        |> Seq.filter isValid
        |> Seq.length
    let solutionPart2 =
        puzzleInput
        |> Seq.filter isValid2
        |> Seq.length    
    printfn "Part 1: %d" solutionPart1
    printfn "Part 2: %d" solutionPart2
    0
