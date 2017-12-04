module Day04Tests

open Day04
open FsUnit
open Xunit

[<Fact>]
let ``[Part 1] "aa bb cc dd ee" is a valid passphrase`` ()=
    isValid "aa bb cc dd ee" |> should be True

[<Fact>]
let ``[Part 1] "aa bb cc dd aa" is not a valid passphrase`` ()=
    isValid "aa bb cc dd aa" |> should be False
   
[<Fact>]
let ``[Part 1] "aa bb cc dd aaa" is a valid passphrase`` ()=
    isValid "aa bb cc dd aaa" |> should be True

[<Fact>]
let ``[Part 2] "abcde fghhij" is a valid passphrase`` ()=
    isValid2 "abcdef fghij" |> should be True

[<Fact>]
let ``[Part 2] "abcde xyz ecdab" is not a valid passphrase`` ()=
    isValid2 "abcde xyz ecdab" |> should be False

[<Fact>]
let ``[Part 2] "a ab abc abd abf abj" is a valid passphrase`` ()=
    isValid2 "a ab abc abd abf abj" |> should be True

[<Fact>]
let ``[Part 2] "iiii oiii ooii oooi oooo" is a valid passphrase`` ()=
    isValid2 "iiii oiii ooii oooi oooo" |> should be True

[<Fact>]
let ``[Part 2] "oiii ioii iioi iiio" is not a valid passphrase`` ()=
    isValid2 "oiii ioii iioi iiio" |> should be False
