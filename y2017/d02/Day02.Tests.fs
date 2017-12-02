module Day02Tests

open Day02
open FsUnit
open Xunit

let testInput = [|"5\t1\t9\t5"; "7\t5\t3"; "2\t4\t6\t8"|]

[<Fact>]
let ``[Part 1] Test input returns 18`` ()=
    checksum1 testInput 
    |> should equal 18