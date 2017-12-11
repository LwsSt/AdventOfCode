open Day10

[<EntryPoint>]
let main argv =
    let puzzleInput1 = [31; 2; 85; 1; 80; 109; 35; 63; 98; 255; 0; 13; 105; 254; 128; 33]
    let puzzleInput2 = "31,2,85,1,80,109,35,63,98,255,0,13,105,254,128,33"
    let input = Seq.init 256 id |> Seq.toList
    hash1 puzzleInput1 input
    |> printfn "Part 1: %d"
    hash2 puzzleInput2 input
    |> printfn "Part 2: %s"
    0
