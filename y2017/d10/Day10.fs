module Day10

let hash (lengths: int list) (input: int list): int = failwith "Implement"

module Tests =

    open FsUnit
    open Xunit

    [<Fact>]
    let ``Test input produces 12`` ()=
        let inputList = Seq.init 5 id |> Seq.toList
        hash [3; 4; 1; 5] inputList |> should equal 12