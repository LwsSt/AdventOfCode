module Day06

let cycle (input: int list): int list = failwith "Implement"

let escape (input: int list): int = failwith "Implement"

module Test = 

    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] {0, 2, 7, 0} returns 5 cycles`` ()=
        escape [0; 2; 7; 0] |> should equal 5

    [<Fact>]
    let ``[Part 1] {0, 2, 7, 0} cycles to {2, 4, 1, 2}`` ()=
        cycle [0; 2; 7; 0] |> should equal [2; 4; 1; 2]
    
    [<Fact>]
    let ``[Part 1] {2, 4, 1, 2} cycles to {3, 1, 2, 3}`` ()=
        cycle [2; 4; 1; 2] |> should equal [3; 1; 2; 3]

    [<Fact>]
    let ``[Part 1] {3, 1, 2, 3} cycles to {0, 2, 3, 4}`` ()=
        cycle [3; 1; 2; 3] |> should equal [0; 2; 3; 4]

    [<Fact>]
    let ``[Part 1] {0, 2, 3, 4} cycles to {1, 3, 4, 1}`` ()=
        cycle [0; 2; 3; 4] |> should equal [1; 3; 4; 1]

    [<Fact>]
    let ``[Part 1] {1, 3, 4, 1} cycles to {2, 4, 1, 2}`` ()=
        cycle [1; 3; 4; 1] |> should equal [2; 4; 1; 2]