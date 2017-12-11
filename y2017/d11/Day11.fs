module Day11

let traverse (moves: string seq): int = failwith "Implement"

module Tests = 

    open FsUnit
    open Xunit
   
    let ``ne,ne,ne is 3 steps away`` ()=
        traverse ["ne"; "ne"; "ne"] |> should equal 3

    let ``ne,ne,sw,sw is 0 steps away`` ()=
        traverse ["ne"; "ne"; "sw"; "sw"] |> should equal 0

    let ``ne,ne,s,s is 2 steps away`` ()=
        traverse ["ne"; "ne"; "s"; "s"] |> should equal 2

    let ``se,sw,se,sw,sw is 3 steps away`` ()=
        traverse ["se"; "sw"; "se"; "sw"; "sw"] |> should equal 3