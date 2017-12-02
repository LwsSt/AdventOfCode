open System.IO

open Day02

[<EntryPoint>]
let main argv =
    let puzzleInput1 = File.ReadAllLines "puzzle-input-1"
    checksum1 puzzleInput1 |> printf "%d"
    0
