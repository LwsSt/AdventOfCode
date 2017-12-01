module Day01Tests

open Day01
open Xunit
open FsUnit.Xunit

[<Fact>]
let ``[Part 1] 1122 returns 3`` () =
    captcha "1122" |> should equal 3

[<Fact>]
let ``[Part 1] 1111 returns 4`` () =
    captcha "1111" |> should equal 4

[<Fact>]
let ``[Part 1] 1234 returns 0`` () =
    captcha "1234" |> should equal 0

[<Fact>]
let ``[Part 1] 91212129 returns 9`` () =
    captcha "91212129" |> should equal 9