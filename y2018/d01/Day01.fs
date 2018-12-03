module Day01

let parsePuzzle1 (input: string list): int = Seq.sumBy int input

let parsePuzzle2 (input: string list): int =
    let cycle xs = seq { while true do yield! xs }
    let deltas = Seq.map int input
    cycle deltas
    |> Seq.scan (fun (f, seen) d -> f + d, Set.add f seen) (0, Set.empty)
    |> Seq.find (fun (f, seen) -> Set.contains f seen)
    |> fst


module Tests = 

    open FsUnit
    open Xunit

    [<Fact>]
    let ``[+1; -2; +3; +1] results in 3`` () =
        parsePuzzle1 ["+1"; "-2"; "+3"; "+1"] |> should equal 3

    [<Fact>]
    let ``[+1; +1; +1] results in 3`` () =
        parsePuzzle1 ["+1"; "+1"; "+1"] |> should equal 3

    [<Fact>]
    let ``[+1; +1; -2] results in 0`` () =
        parsePuzzle1 ["+1"; "+1"; "-2"] |> should equal 0

    [<Fact>]
    let ``[-1; -2; -3] results in -6`` () =
        parsePuzzle1 ["-1"; "-2"; "-3"] |> should equal -6

    [<Fact>]
    let ``[+1; -2; +3; +1] repeats frequency 2`` () =
        parsePuzzle1 ["+1"; "-2"; "+3"; "+1"] |> should equal 3

    [<Fact>]
    let ``[+1; -1] repeats frequency 0`` () =
        parsePuzzle2 ["+1"; "-1"] |> should equal 0

    [<Fact>]
    let ``[+3; +3; +4; -2; -4] repeats frequency 10`` () =
        parsePuzzle2 ["+3"; "+3"; "+4"; "-2"; "-4"] |> should equal 10

    let ``[-6; +3; +8; +5; -6] repeats frequency 5`` () =
        parsePuzzle2 ["-6"; "+3"; "+8"; "+5"; "-6"] |> should equal 5

    let ``[+7; +7; -2; -7; -4] repeats frequency  14`` () =
        parsePuzzle2 ["+7"; "+7"; "-2"; "-7"; "-4"] |> should equal 14
