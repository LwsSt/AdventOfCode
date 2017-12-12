module Day11

let n (a, b) = (a, b - 1)
let s (a, b) = (a, b + 1)
let nw (a, b) = (a - 1, b)
let se (a, b) = (a + 1, b)
let ne = n >> se
let sw = s >> nw

let parse = function
| "n" -> n
| "s" -> s
| "ne" -> ne
| "nw" -> nw
| "sw" -> sw
| "se" -> se
| _ -> failwith "Invalid direction"

let toCube (x, y) = (x, y, (-x) - y)

let cubeDistance (x1, y1, z1) (x2, y2, z2)=
    let dx = abs (x1 - x2)
    let dy = abs (y1 - y2)
    let dz = abs (z1 - z2)
    max dx dy |> max dz

let distanceFromOrigin = cubeDistance (0, 0, 0)

let traverse (moves: string seq): int = 
    let moveFunc = 
        moves
        |> Seq.map parse
        |> Seq.reduce (>>)
    moveFunc (0, 0)
    |> toCube
    |> cubeDistance (0, 0, 0)

let traverse2 (moves: string list): int =
    let movefs = moves |> List.map parse
    let rec traverseImpl fs results prev =
        match fs with
        | [] -> results
        | f::gs -> 
            let next = f prev
            traverseImpl gs (next::results) next
    let rs = traverseImpl movefs [] (0, 0)
    rs
    |> List.map (toCube >> distanceFromOrigin)
    |> List.max

module Tests = 

    open FsUnit
    open Xunit
   
    [<Fact>]
    let ``ne,ne,ne is 3 steps away`` ()=
        traverse ["ne"; "ne"; "ne"] |> should equal 3

    [<Fact>]
    let ``ne,ne,sw,sw is 0 steps away`` ()=
        traverse ["ne"; "ne"; "sw"; "sw"] |> should equal 0

    [<Fact>]
    let ``ne,ne,s,s is 2 steps away`` ()=
        traverse ["ne"; "ne"; "s"; "s"] |> should equal 2

    [<Fact>]
    let ``se,sw,se,sw,sw is 3 steps away`` ()=
        traverse ["se"; "sw"; "se"; "sw"; "sw"] |> should equal 3