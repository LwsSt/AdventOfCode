module KnotHash

open System.Collections.Generic

let private cycle position length (input: List<int>): List<int> =
    let inputLength = input.Count
    let subList = new List<int>()
    for i = 0 to length - 1 do subList.Add(input.[(position + i) % input.Count])
    subList.Reverse()
    for i = 0 to length - 1 do
        let idx = (position + i) % inputLength
        input.RemoveAt idx
        input.Insert(idx, subList.[i])
    input

let private  makeLengths (str: string) =
    let input = str |> Seq.map int
    [input; [17; 31; 73; 47; 23] |> List.toSeq]
    |> Seq.concat
    |> Seq.toList

let rec private hashRound position skipSize (list: List<int>) lengths =
    match lengths with
    | [] -> (list, position, skipSize)
    | nxt::tail -> hashRound (position + nxt + skipSize) (skipSize + 1) (cycle position nxt list) tail

let private makeDenseHash sparseHash =
    sparseHash
    |> Seq.chunkBySize 16
    |> Seq.map (fun s -> s |> Seq.reduce (^^^))

let private stringHash (denseHash: seq<int>) =
    denseHash
    |> Seq.map (fun i -> i.ToString("x2"))
    |> Seq.reduce (+)

let knotHash (str: string) (input: int list): string =
    let lengths = makeLengths str
    let rec hashImpl position skipSize (list: List<int>) ls count =
        match count with 
        | 0 -> list |> Seq.toList
        | n -> 
            let (l, p, s) = hashRound position skipSize list ls
            hashImpl p s l ls (n - 1)
    hashImpl 0 0 (new List<int>(input)) lengths 64
    |> makeDenseHash
    |> stringHash
