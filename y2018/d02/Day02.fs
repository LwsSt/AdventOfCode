module Day02

let puzzle1 (input:string list): int =
    let letterCount (str: string): (char * int) list =
        str
        |> Seq.groupBy id
        |> Seq.map (fun (a, b) -> (a, Seq.length b))
        |> Seq.toList
    let letters2 =
        input
        |> Seq.map letterCount
        |> Seq.filter (fun ls -> ls |> Seq.exists (fun (_, count) -> count = 2))
        |> Seq.length
    let letters3 =
        input
        |> Seq.map letterCount
        |> Seq.filter (fun ls -> ls |> Seq.exists (fun (_, count) -> count = 3))
        |> Seq.length
    letters2 * letters3

let puzzle2 (input:string list): (string*string) = failwith "Not implemented"

module Tests = 
    open FsUnit
    open Xunit

    let exampleInput = [
        "abcdef"
        "bababc"
        "abbcde"
        "abcccd"
        "aabcdd"
        "abcdee"
        "ababab"
    ]

    [<Fact>]
    let ``Example input produces 12`` () =
        puzzle1 exampleInput |> should equal 12