module Day05

let escape (input: int[]) : int = failwith "Implement"

module Tests =

    open FsUnit
    open Xunit

    [<Fact>]
    let ``Example input takes 5 steps`` ()=
        escape [|0; 3; 0; 1; -3|] |> should equal 5