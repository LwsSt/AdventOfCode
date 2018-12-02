open Day01

[<EntryPoint>]
let main argv =
    let puzzleInput = System.IO.File.ReadAllLines("puzzle-input")
    printfn "Day01 part 1 %i" <| parsePuzzle1 (puzzleInput |> Seq.toList)
    0 // return an integer exit code