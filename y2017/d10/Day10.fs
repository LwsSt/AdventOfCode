module Day10

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


let hash (lengths: int list) (input: int list): int =
    let l = new List<int>(input)
    let rec hashImpl position skipSize (list: List<int>) lengths =
        match lengths with
        | [] -> list.[0] * list.[1]
        | nxt::tail -> hashImpl (position + nxt + skipSize) (skipSize + 1) (cycle position nxt list) tail
    hashImpl 0 0 l lengths

module Tests =

    open FsUnit
    open Xunit

    let testList = Seq.init 5 id |> Seq.toList
    

    [<Fact>]
    let ``Test input produces 12`` ()=
        hash [3; 4; 1; 5] testList |> should equal 12

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
