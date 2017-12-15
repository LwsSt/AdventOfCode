module Day15

let rec generate (prev: int64) (factor: int64): int64 seq = 
    seq {
        let next = (prev * factor) % 2147483647L
        yield next
        yield! generate next factor
    }

let bitMask16 (n: int64) = n &&& ((1L <<< 16) - 1L)

let judgeGenerators (initialA: int64) (initialB: int64): int =
    let genA = generate initialA 16807L |> Seq.map bitMask16
    let genB = generate initialB 48271L |> Seq.map bitMask16
    genA
    |> Seq.zip genB
    |> Seq.take 40_000_000
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.length

module Tests = 
    open FsUnit
    open Xunit

    [<Fact>]
    let ``Gen A: 65, Gen B: 8921 generates 588 matching pairs`` ()=
        judgeGenerators 65L 8921L |> should equal 588

    [<Fact>]
    let `` Generator A produces the correct initial sequence`` ()=
        generate 65L 16807L |> Seq.take 5 |> Seq.toList |> should equal [1092455L; 1181022009L; 245556042L; 1744312007L; 1352636452L]

    [<Fact>]
    let `` Generator B produces the correct initial sequence`` ()=
        generate 8921L 48271L |> Seq.take 5 |> Seq.toList |> should equal [430625591L; 1233683848L; 1431495498L; 137874439L; 285222916L]
