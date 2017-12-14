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

let defrag (str: string): int = 
    str
    |> genRows
    |> Seq.map (hash >> hexToBinary)
    |> Seq.collect (fun s -> s |> Seq.filter ((=) '1'))
    |> Seq.length

module Tests = 
    open FsUnit
    open Xunit

    [<Fact>]
    let ``flqrgnkx has 8108 used square`` ()=
        defrag "flqrgnkx" |> should equal 8108