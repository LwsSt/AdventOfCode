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

[<Fact>]
let ``[Part 2] 1212 returns 6`` () =
    captcha2 "1212" |> should equal 6

[<Fact>]
let ``[Part 2] 1221 returns 0`` () = 
    captcha2 "1221" |> should equal 0

[<Fact>]
let ``[Part 2] 123425 return 4`` () =
    captcha2 "123425" |> should equal 4

[<Fact>]
let ``[Part 2] 123123 returns 12`` () =
    captcha2 "123123" |> should equal 12

[<Fact>]
let ``[Part 2] 12131415 returns 4`` () =
    captcha2 "12131415" |> should equal 4