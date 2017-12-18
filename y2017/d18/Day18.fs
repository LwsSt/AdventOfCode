module Day18

let run (program: string list): int = failwith "Implement"

module Tests =

    open FsUnit
    open Xunit

    let testInput = [
        "set a 1"
        "add a 2"
        "mul a a"
        "mod a 5"
        "snd a"
        "set a 0"
        "rcv a"
        "jgz a -1"
        "set a 1"
        "jgz a -2"
    ]

    [<Fact>]
    let ``Test input produces value of 4`` ()=
        run testInput |> should equal 4
