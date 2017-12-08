module Day08

let runRegisters (lines: string list): int = failwith "Implement"

module Tests =

    open FsUnit
    open Xunit

    let testInput = [
        "b inc 5 if a > 1"
        "a inc 1 if b < 5"
        "c dec -10 if a >= 1"
        "c inc -20 if c == 10"
        ]

    [<Fact>]
    let ``[Part 1] Test input returns correct result`` ()=
        runRegisters testInput |> should equal 1