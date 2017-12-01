open System
open Day01

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllText "puzzle-input-1"
    let solution = captcha puzzleInput
    printfn "%d" solution
    0