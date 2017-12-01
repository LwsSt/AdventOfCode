module Day01

let captcha (str: string) : int =
    let input = str + (string (str.Chars 0))
    let inline charToInt c = int c - int '0'
    let reducer (acc: (int * int)) (next: int) = 
        match acc with
        | (0, 0) -> (0, next)
        | (total, prev) when prev = next -> (total + next, next)
        | (total, _) -> (total, next)
    input
    |> Seq.map charToInt
    |> Seq.fold reducer (0, 0)
    |> fst
