module Day12

open FParsec

let parse line = 
    let numbers = sepBy (pint32 |>> int) (pstring "," .>> spaces)
    let id = (pint32 |>> int)
    let pipe = spaces >>. pstring "<->" .>> spaces
    let parser = 
        (id .>> pipe .>>. numbers) 
        |>> (fun (id, nums) -> (List.collect (fun n -> [(id, n); (n, id)]) nums))
    match run parser line with
    | Success (res, _, _) -> res |> Set.ofList
    | Failure (err, _, _) -> failwith err


let plumb (input: string list): int = failwith "Implement"

module Tests = 
    
    open FsUnit
    open Xunit

    let testInput = [
        "0 <-> 2"
        "1 <-> 1"
        "2 <-> 0, 3, 4"
        "3 <-> 2, 4"
        "4 <-> 2, 3, 6"
        "5 <-> 6"
        "6 <-> 4, 5"
    ]

    [<Fact(Skip = "Not implemented")>]
    let ``Test input returns 6 programs`` ()=
        plumb testInput |> should equal 6

    [<Fact>]
    let ``Parse "0 <-> 2" returns [(0, 2), (2, 0)]`` ()=
        parse "0 <-> 2" |> should equal ([(0, 2); (2, 0)] |> Set.ofList)

    [<Fact>]
    let ``Parse "2 <-> 0, 3, 4" returns [(2, 0), (0, 2), (2, 3), (3, 2), (2, 4), (4, 2)]`` ()=
        parse "2 <-> 0, 3, 4" |> should equal ([(2, 0); (0, 2); (2, 3); (3, 2); (2, 4); (4, 2)] |> Set.ofList)

    [<Fact>]
    let ``Parse "1 <-> 1 returns [(1, 1)]`` ()=
        parse "1 <-> 1" |> should equal ([(1, 1)] |> Set.ofList)