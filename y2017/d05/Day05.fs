module Day05

let escape (input: int[]): int = 
    let mutable ptr = 0
    let mutable steps = 0
    while ptr < input.Length do
        let jump = input.[ptr]
        input.[ptr] <- input.[ptr] + 1
        ptr <- ptr + jump
        steps <- steps + 1
    steps

let escape2 (input: int[]): int = 
    let nextVal i = if i >= 3 then i - 1 else i + 1
    let mutable ptr = 0
    let mutable steps = 0
    while ptr < input.Length do
        let jump = input.[ptr]
        input.[ptr] <- nextVal input.[ptr]
        ptr <- ptr + jump
        steps <- steps + 1
    steps

module Tests =

    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] Example input takes 5 steps`` ()=
        escape [|0; 3; 0; 1; -3|] |> should equal 5

    [<Fact>]
    let ``[Part 2] Example input takes 10 steps`` ()=
        escape2 [|2; 3; 2; 3; -1|]