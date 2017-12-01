module Day01

let captcha (str: string) : int =
    let input = str + (string (str.Chars 0))
    let inline charToInt c = int c - int '0'
    input
    |> Seq.map charToInt
    |> Seq.pairwise
    |> Seq.filter (fun (x, y) -> x = y)
    |> Seq.map fst
    |> Seq.sum
