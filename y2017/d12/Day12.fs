module Day12

let parse line = failwith "Implement"

let plumb (input: string list): int = failwith "Implement"

module Tests = 
    
    open FsUnit
    open Xunit

    let testInput = [
        "0 <-> 2"
        "1 <-> 1"
        "2 <-> 0, 3, 4"
        "3 <-> 2, 4"
        "4 <-> 2, 3, 6"
        "5 <-> 6"
        "6 <-> 4, 5"
    ]

    [<Fact>]
    let ``Test input returns 6 programs`` ()=
        plumb testInput |> should equal 6