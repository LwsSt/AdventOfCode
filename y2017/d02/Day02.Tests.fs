module Day02Tests

open Day02
open FsUnit
open Xunit

let testInput1 = [|"5\t1\t9\t5"; "7\t5\t3"; "2\t4\t6\t8"|]
let testInput2 = [|"5\t9\t2\t8"; "9\t4\t7\t3"; "3\t8\t6\t5" |]

[<Fact>]
let ``[Part 1] Test input returns 18`` ()=
    checksum1 testInput1
    |> should equal 18

[<Fact>]
let ``[Part 2] Test input returns 9`` ()=
    checksum2 testInput2
    |> should equal 9