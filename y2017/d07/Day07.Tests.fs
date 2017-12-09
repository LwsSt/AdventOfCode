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

let testTower = 
    Node ("tknk", 41, 
        [
        Node ("ugml", 68, [Leaf ("gyxo", 61); Leaf ("ebii", 61); Leaf ("jptl", 61)])
        Node ("padx", 45, [Leaf ("pbga", 66); Leaf ("havc", 66); Leaf ("qoyq", 66)])
        Node ("fwft", 72, [Leaf ("ktlj", 57); Leaf ("cntj", 57); Leaf ("xhth", 57)])
        ])

[<Fact>]
let ``[Part 1] Test input returns tknk`` ()=
    towers testInput |> should equal "tknk"

[<Fact>]
let ``[Part 2] Test input returns 60`` ()=
    balanceTower testInput |> should equal 60

[<Fact>]
let ``Test input creates correct tower`` ()=
    constructTower testInput |> should equal testTower

[<Fact>]
let ``Parse 'pbga (66)' returns correct value`` ()=
    parseLine "pbga (66)" |> should equal <| {name="pbga"; weight=66; programs=[]}

[<Fact>]
let ``Parse 'fwft (72) -> ktlj, cntj, xhth' returns correct value`` ()=
    parseLine "fwft (72) -> ktlj, cntj, xhth" |> should equal <| {name= "fwft"; weight= 72; programs= ["ktlj"; "cntj"; "xhth"]}