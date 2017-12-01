module Day01
let inline charToInt (c: char) = int c - int '0'

let captcha (str: string) : int =
    let input = str + (string (str.Chars 0))
    input
    |> Seq.map charToInt
    |> Seq.pairwise
    |> Seq.filter (fun (x, y) -> x = y)
    |> Seq.map fst
    |> Seq.sum

let captcha2 (str: string) : int = 
    let length = String.length str
    let input = Seq.map charToInt str
    let inputOffset = str.Substring(length/2) + str |> Seq.map charToInt
    input
    |> Seq.zip inputOffset
    |> Seq.filter (fun (x, y) -> x = y)
    |> Seq.map fst
    |> Seq.sum