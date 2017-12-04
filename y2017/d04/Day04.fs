module Day04

let isValid (str: string): bool =
    let words = str.Split [|' '|]
    let wordCount = words.Length
    let distinctWordCount = words |> Array.distinct |> Array.length
    wordCount = distinctWordCount

let isValid2 (str: string) : bool = failwith "Implement"