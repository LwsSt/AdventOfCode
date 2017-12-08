module Day07Tests

open Day07
open FsUnit
open Xunit

let testInput = [
    "pbga (66)"
    "xhth (57)"
    "ebii (61)"
    "havc (66)"
    "ktlj (57)"
    "fwft (72) -> ktlj, cntj, xhth"
    "qoyq (66)"
    "padx (45) -> pbga, havc, qoyq"
    "tknk (41) -> ugml, padx, fwft"
    "jptl (61)"
    "ugml (68) -> gyxo, ebii, jptl"
    "gyxo (61)"
    "cntj (57)"
]

[<Fact(Skip = "Not implemented")>]
let ``[Part 1] Test input returns tknk`` ()=
    towers testInput |> should equal "tknk"

[<Fact>]
let ``Parse 'pbga (66)' returns correct value`` ()=
    parseLine "pbga (66)" |> should equal <| {name="pbga"; weight=66; programs=[]]}

let ``Parse 'fwft (72) -> ktlj, cntj, xhth' returns correct value`` ()=
    parseLine "fwft (72) -> ktlj, cntj, xhth" |> should equal <| {name= "fwft"; weight= 72; programs= ["ktlj"; "cntj"; "xhth"]}