module Day03Tests

open Day03
open FsUnit
open Xunit

[<Fact(Skip = "Weird edge case")>]
let ``[Part 1] 1 returns 0 steps`` ()=
    memory 1 |> should equal 0

[<Fact>]
let ``[Part 1] 2 returns 1 steps`` ()=
    memory 2 |> should equal 1

[<Fact>]
let ``[Part 1] 3 returns 2 steps`` ()=
    memory 3 |> should equal 2

[<Fact>]
let ``[Part 1] 4 returns 1 steps`` ()=
    memory 4 |> should equal 1

[<Fact>]
let ``[Part 1] 12 returns 3 steps`` ()=
    memory 12 |> should equal 3

[<Fact>]
let ``[Part 1] 23 returns 2 steps`` ()=
    memory 23 |> should equal 2

[<Fact>]
let ``[Part 1] 1024 returns 31 steps`` ()=
    memory 1024 |> should equal 31