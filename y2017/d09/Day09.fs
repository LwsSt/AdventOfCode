module Day09

let score (input: string): int = failwith "Implement"

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
    let ``[Part 1] {{<a!>},{<a!>},{<a!>},{<ab>}} scores 2`` ()=
        score "{{<a!>},{<a!>},{<a!>},{<ab>}}" |> should equal 2    