module Day14

let defrag (str: string): int = failwith "Implement"

module Tests = 
    open FsUnit
    open Xunit

    [<Fact>]
    let ``flqrgnkx has 8108 used square`` ()=
        defrag "flqrgnkx" |> should equal 128