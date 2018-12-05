open Day03

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines("puzzle-input") |> Array.toList
    printfn "Day 03 Part 1 %i" (puzzle1 puzzleInput)
    0 // return an integer exit code
