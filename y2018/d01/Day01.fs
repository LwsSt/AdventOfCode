module Day01

let parsePuzzle1 (input: string list): int = failwith "Not implemented"

module Tests = 

    open FsUnit
    open Xunit

    [<Fact>]
    let ``[+1; -2; +3; +1] results in 3`` () =
        parsePuzzle1 ["+1"; "-2"; "+3"; "+1"] |> should equal 3

    [<Fact>]
    let ``[+1; +1; +1] results in 3`` () =
        parsePuzzle1 ["+1"; "+1"; "+1"] |> should equal 3

    [<Fact>]
    let ``[+1; +1; -2] results in 0`` () =
        parsePuzzle1 ["+1"; "+1"; "-2"] |> should equal 0

    [<Fact>]
    let ``[-1; -2; -3] results in -6`` () =
        parsePuzzle1 ["-1"; "-2"; "-3"] |> should equal -6
