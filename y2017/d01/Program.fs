open System
open Day01

[<EntryPoint>]
let main argv =
    let puzzleInput1 = System.IO.File.ReadAllText "puzzle-input-1"
    let puzzleInput2 = System.IO.File.ReadAllText "puzzle-input-2"
    captcha puzzleInput1 |> printfn "Part 1: %d"
    captcha2 puzzleInput2 |> printfn "Part 2: %d"
    0