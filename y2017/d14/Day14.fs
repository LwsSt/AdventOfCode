module Day14

open KnotHash

let hash s = knotHash s (Seq.init 256 id |> Seq.toList)

let lookup = [
    ( '0', "0000" )
    ( '1', "0001" )
    ( '2', "0010" )
    ( '3', "0011" )
    ( '4', "0100" )
    ( '5', "0101" )
    ( '6', "0110" )
    ( '7', "0111" )
    ( '8', "1000" )
    ( '9', "1001" )
    ( 'a', "1010" )
    ( 'b', "1011" )
    ( 'c', "1100" )
    ( 'd', "1101" )
    ( 'e', "1110" )
    ( 'f', "1111" )] |> Map.ofSeq

let hexToBinary (str: string) =
    str
    |> Seq.map (fun s -> Map.find s lookup)
    |> Seq.reduce (+)
    

let genRows str = Seq.init 128 (fun n -> sprintf "%s-%d" str n) |> Seq.toList

let makeMemGrid str = 
    str
    |> genRows
    |> Seq.map (hash >> hexToBinary)

let defrag (str: string): int = 
    str
    |> makeMemGrid
    |> Seq.collect (fun s -> s |> Seq.filter ((=) '1'))
    |> Seq.length

let get (x, y) (grid: int list list) = 
    match List.tryItem x grid with
    | Some ys -> 
        match List.tryItem y ys with
        | Some num -> num
        | None -> 0
    | None -> 0    

let neighbours (x, y) = [
        (x - 1, y + 1); (x, y + 1); (x + 1, y + 1)
        (x - 1, y); (x + 1, y);
        (x - 1, y - 1); (x, y - 1); (x + 1, y - 1)
    ]

let coords = 
    Seq.init 128 (fun x -> Seq.init 128 (fun y -> x, y)) 
    |> Seq.concat 
    |> Seq.toList

let regions (str: string): int = 
    let rec mark coord grid visited = 
        
    let rec scan coords grid visited regions =
        match grid with
        | [] -> regions
        | ((x, y) as c)::cs when not <| Set.contains c coords -> 0
        | c::cs -> scan cs grid visited regions

    let memGrid = 
        str
        |> makeMemGrid
        |> Seq.map (fun s -> s |> Seq.map int |> Seq.toList)
        |> Seq.toList
    let i = memGrid.[0]
    0

module Tests = 
    open FsUnit
    open Xunit

    [<Fact>]
    let ``flqrgnkx has 8108 used square`` ()=
        defrag "flqrgnkx" |> should equal 8108

    let ``flqrgnkx has 1242 regions`` ()=
        regions "flqrgnkx" |> should equal 1242