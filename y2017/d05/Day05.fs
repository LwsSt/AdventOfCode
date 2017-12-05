module Day05

let escape (input: int[]) : int = 
    let mutable ptr = 0
    let mutable steps = 0
    while ptr < input.Length do
        let jump = input.[ptr]
        input.[ptr] <- input.[ptr] + 1
        ptr <- ptr + jump
        steps <- steps + 1
    steps

module Tests =

    open FsUnit
    open Xunit

    [<Fact>]
    let ``Example input takes 5 steps`` ()=
        escape [|0; 3; 0; 1; -3|] |> should equal 5