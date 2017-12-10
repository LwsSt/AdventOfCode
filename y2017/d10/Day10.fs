module Day10

open System.Collections.Generic

let cycle position length (input: List<int>): List<int> =
    let subList = new List<int>()
    for i in length do subList.Add(input.[(position + i) % input.Count])
    subList.Reverse()
    for i in length do input.Insert((position + i) % input.Count, subList.[i])
    input


let hash (lengths: int list) (input: int list): int = failwith "Implement"

module Tests =

    open FsUnit
    open Xunit

    let testList = Seq.init 5 id |> Seq.toList
    

    [<Fact>]
    let ``Test input produces 12`` ()=
        hash [3; 4; 1; 5] testList |> should equal 12

    let ``0, 1, 2, 3, 4 cycles to 2 1 0 3 4`` ()=
        let inputList = new List<int>()
        inputList.AddRange testList