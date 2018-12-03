module Day02

open System

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

let puzzle2 (input:string list): (string*string) =
    let compareString (str1:string) (str2:string):string =
        str1
        |> Seq.filter (str2.Contains)
        |> String.Concat
    input
    |> Seq.collect (fun s1 -> Seq.map (fun s2 -> (s1, compareString s1 s2)) input)
    |> Seq.filter (fun (a, b) -> String.length a = ((String.length b) - 1))
    |> Seq.head


module Tests = 
    open FsUnit
    open Xunit

    let exampleInput1 = [
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
        puzzle1 exampleInput1 |> should equal 12

    let exampleInput2 = [
        "abcde"
        "fghij"
        "klmno"
        "pqrst"
        "fguij"
        "axcye"
        "wvxyz"
    ]

    let ``Example input produces (fghij, fguij)`` () =
        puzzle2 exampleInput2 |> should equal ("fghij", "fguij")