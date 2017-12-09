module Day09

let score (input: string): int =
    let rec scoreImpl (str: char list) (score: int) (acc: int) =
        match str with
        | [] -> acc
        | '{'::tail -> scoreImpl tail (score + 1) (acc)
        | '<'::tail -> ignoreGarbage tail score acc
        | '}'::tail -> scoreImpl tail (score - 1) (acc + score)
        | ','::tail -> scoreImpl tail score acc
    and ignoreGarbage (str: char list) (score: int) (acc: int) =
        match str with 
        | '>'::tail -> scoreImpl tail score acc
        | '!'::tail -> ignoreGarbage (List.tail tail) score acc
        | _::tail -> ignoreGarbage tail score acc
    scoreImpl (input |> Seq.toList) 0 0

module Tests =
    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] {} scores 1`` ()=
        score "{}" |> should equal 1

    [<Fact>]
    let ``[Part 1] {{{}}} scores 6`` ()=
        score "{{{}}}" |> should equal 6

    [<Fact>]
    let ``[Part 1] {{},{}} scores 5`` ()=
        score "{{},{}}" |> should equal 5

    [<Fact>]
    let ``[Part 1] {{{},{},{{}}}} scores 16`` ()=
        score "{{{},{},{{}}}}" |> should equal 16

    [<Fact>]
    let ``[Part 1] {<a>,<a>,<a>,<a>} scores 1`` ()=
        score "{<a>,<a>,<a>,<a>}" |> should equal 1

    [<Fact>]
    let ``[Part 1] {{<ab>},{<ab>},{<ab>},{<ab>}} scores 9`` ()=
        score "{{<ab>},{<ab>},{<ab>},{<ab>}}" |> should equal 9

    [<Fact>]
    let ``[Part 1] {{<!!>},{<!!>},{<!!>},{<!!>}} scores 9`` ()=
        score "{{<!!>},{<!!>},{<!!>},{<!!>}}" |> should equal 9

    [<Fact>]
    let ``[Part 1] {{<a!>},{<a!>},{<a!>},{<ab>}} scores 3`` ()=
        score "{{<a!>},{<a!>},{<a!>},{<ab>}}" |> should equal 3