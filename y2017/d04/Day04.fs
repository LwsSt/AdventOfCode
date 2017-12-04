module Day04

let isValid (str: string): bool =
    let words = str.Split [|' '|]
    let wordCount = words.Length
    let distinctWordCount = words |> Array.distinct |> Array.length
    wordCount = distinctWordCount

let isAnagram (a: string, b: string) = 
    let sort (s: string): string = s |> Seq.sort |> System.String.Concat
    a.Length = b.Length && (sort a) = (sort b)

let isValid2 (str: string) : bool = 
    let words = str.Split [|' '|] |> Array.toSeq
    Seq.allPairs words words
    |> Seq.filter (fun (a, b) -> a <> b)
    |> Seq.exists isAnagram
    |> not