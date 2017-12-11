module Day10

open System
open System.Collections.Generic

let cycle position length (input: List<int>): List<int> =
    let inputLength = input.Count
    let subList = new List<int>()
    for i = 0 to length - 1 do subList.Add(input.[(position + i) % input.Count])
    subList.Reverse()
    for i = 0 to length - 1 do
        let idx = (position + i) % inputLength
        input.RemoveAt idx
        input.Insert(idx, subList.[i])
    input

let rec hashRound position skipSize (list: List<int>) lengths =
    match lengths with
    | [] -> (list, position, skipSize)
    | nxt::tail -> hashRound (position + nxt + skipSize) (skipSize + 1) (cycle position nxt list) tail

let hash1 (lengths: int list) (input: int list): int =
    let l = new List<int>(input)
    let (outList, _, _) = hashRound 0 0 l lengths
    outList.[0] * outList.[1]


let makeLengths (str: string) =
    let input = str |> Seq.map int
    [input; [17; 31; 73; 47; 23] |> List.toSeq]
    |> Seq.concat
    |> Seq.toList

let makeDenseHash sparseHash =
    sparseHash
    |> Seq.chunkBySize 16
    |> Seq.map (fun s -> s |> Seq.reduce (^^^))

let stringHash (denseHash: seq<int>) =
    denseHash
    |> Seq.map (fun i -> i.ToString("x2"))
    |> Seq.reduce (+)

let hash2 (str: string) (input: int list): string =
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

module Tests =

    open FsUnit
    open Xunit

    let testList = Seq.init 5 id |> Seq.toList
    

    [<Fact>]
    let ``Test input produces 12`` ()=
        hash1 [3; 4; 1; 5] testList |> should equal 12

    [<Fact>]
    let ``0, 1, 2, 3, 4 cycles to 2 1 0 3 4`` ()=
        let inputList = new List<int>(testList)
        cycle 0 3 inputList |> List.ofSeq |> should equal [2; 1; 0; 3; 4]

    [<Fact>]
    let ``2, 1, 0, 3, 4 cycles to 4, 3, 0, 1, 2`` ()=
        let inputList = new List<int>([2; 1; 0; 3; 4])
        cycle 3 4 inputList |> List.ofSeq |> should equal [4; 3; 0; 1; 2]

    [<Fact>]
    let ``4, 3, 0, 1, 2 cycles to 4, 3, 0, 1, 2`` ()=
        let inputList = new List<int>([4; 3; 0; 1; 2])
        cycle 2 1 inputList |> List.ofSeq |> should equal [4; 3; 0; 1; 2]

    [<Fact>]
    let ``4, 3, 0, 1, 2 cycles to 3, 4, 2, 1, 0`` ()=
        let inputList = new List<int>([4; 3; 0; 1; 2])
        cycle 1 5 inputList |> List.ofSeq |> should equal [3; 4; 2; 1; 0]

    let fullList = Seq.init 256 id |> Seq.toList

    [<Fact>]
    let ``"" becomes a2582a3a0e66e6e86e3812dcb672a272`` ()=
        hash2 "" fullList |> should equal "a2582a3a0e66e6e86e3812dcb672a272"

    [<Fact>]
    let ``AoC 2017 becomes 33efeb34ea91902bb2f59c9920caa6cd`` ()=
        hash2 "AoC 2017" fullList |> should equal "33efeb34ea91902bb2f59c9920caa6cd"

    [<Fact>]
    let ``1,2,3 becomes 3efbe78a8d82f29979031a4aa0b16a9d`` ()=
        hash2 "1,2,3" fullList |> should equal "3efbe78a8d82f29979031a4aa0b16a9d"

    [<Fact>]
    let ``1,2,4 becomes 63960835bcdc130f0b66d7ff4f6a5a8e`` ()=
        hash2 "1,2,4" fullList |> should equal "63960835bcdc130f0b66d7ff4f6a5a8e"