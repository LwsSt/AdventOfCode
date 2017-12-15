module Day15

let rec generate (prev: int64) (factor: int64): int64 seq = 
    seq {
        let next = (prev * factor) % 2147483647L
        yield next
        yield! generate next factor
    }

let generateFiltered (prev: int64) (factor: int64) (filter: int64): int64 seq = 
    generate prev factor |> Seq.filter (fun n -> n % filter = 0L)

let bitMask16 (n: int64) = n &&& ((1L <<< 16) - 1L)

let judgeGenerators (initialA: int64) (initialB: int64): int =
    let genA = generate initialA 16807L |> Seq.map bitMask16
    let genB = generate initialB 48271L |> Seq.map bitMask16
    genA
    |> Seq.zip genB
    |> Seq.take 40_000_000
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.length

let judgeGenerators2 (initialA: int64) (initialB: int64): int =
    let genA = generateFiltered initialA 16807L 4L |> Seq.map bitMask16
    let genB = generateFiltered initialB 48271L 8L |> Seq.map bitMask16
    genA
    |> Seq.zip genB
    |> Seq.take 5_000_000
    |> Seq.filter (fun (a, b) -> a = b)
    |> Seq.length

module Tests = 
    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] Gen A: 65, Gen B: 8921 generates 588 matching pairs`` ()=
        judgeGenerators 65L 8921L |> should equal 588

    [<Fact>]
    let `` [Part 1] Generator A produces the correct initial sequence`` ()=
        generate 65L 16807L |> Seq.take 5 |> Seq.toList |> should equal [1092455L; 1181022009L; 245556042L; 1744312007L; 1352636452L]

    [<Fact>]
    let ``[Part 1] Generator B produces the correct initial sequence`` ()=
        generate 8921L 48271L |> Seq.take 5 |> Seq.toList |> should equal [430625591L; 1233683848L; 1431495498L; 137874439L; 285222916L]

    [<Fact>]
    let ``[Part 1] Gen A: 65 % 4, Gen B: 8921 % 8 generates 309 matching pairs`` ()=
        judgeGenerators2 65L 8921L |> should equal 309

    [<Fact>]
    let ``[Part 2] Generator A produces correct initial sequence`` ()=
        generateFiltered 65L 16807L 4L |> Seq.take 5 |> Seq.toList |> should equal [1352636452L; 1992081072L; 530830436L; 1980017072L; 740335192L]

    [<Fact>]
    let ``[Part 2] Generator B produces correct initial sequence`` ()=
        generateFiltered 8921L 48271L 8L |> Seq.take 5 |> Seq.toList |> should equal [1233683848L; 862516352L; 1159784568L; 1616057672L; 412269392L]