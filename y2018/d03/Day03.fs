module Day03

open System.Text.RegularExpressions

let applyClaim l t w h (array: int[,]) =
    for x = l to (l + w - 1) do
        for y = t to (t + h - 1) do
            array.[x, y] <- (array.[x, y] + 1)
    ()

let regex = new Regex("""#(?<id>\d+) @ (?<left>\d+),(?<top>\d+): (?<width>\d+)x(?<height>\d+)""", RegexOptions.Compiled)

let processLine (line:string): int[,] -> unit =
    let args = regex.Match(line).Groups
    let left = int args.["left"].Value
    let top = int args.["top"].Value
    let width = int args.["width"].Value
    let height = int args.["height"].Value
    applyClaim left top width height

let puzzle1 (input:string list) = 
    let fabric = Array2D.zeroCreate 1000 1000
    input
    |> List.map processLine
    |> List.iter (fun f -> f fabric)
    let mutable count = 0
    for x =0 to (Array2D.length1 fabric - 1) do
        for y = 0 to (Array2D.length2 fabric - 1) do
            if fabric.[x,y] > 1 then 
                count <- count + 1 
            else 
                ()
    count


module Test =
    open FsUnit
    open Xunit

    let testInput = [
        "#1 @ 1,3: 4x4"
        "#2 @ 3,1: 4x4"
        "#3 @ 5,5: 2x2"
    ]

    [<Fact>]
    let ``Test input produces 4`` () =
        puzzle1 testInput |> should equal 4