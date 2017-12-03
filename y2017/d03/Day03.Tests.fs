module Day03Tests

open Day03
open FsUnit
open Xunit

let ``[Part 1] 1 returns 0 steps`` ()=
    memory 1 |> should equal 0

let ``[Part 1] 12 returns 3 steps`` ()=
    memory 12 |> should equal 3

let ``[Part 1] 23 returns 2 steps`` ()=
    memory 23 |> should equal 2

let ``[Part 1] 1024 returns 31 steps`` ()=
    memory 1025 |> should equal 31