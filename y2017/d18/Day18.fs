module Day18

open FParsec

type Arg = 
    | Register of char
    | Number of int

type Instruction =
    | Send of char
    | Set of char * Arg
    | Add of char * Arg
    | Mul of char * Arg
    | Mod of char * Arg
    | Rec of char
    | Jump of Arg * Arg

let parse (str: string): Instruction =
    let parseArg = (letter |>> Register) <|> (pint32 |>> Number)
    let parseMethod name f = pstring name .>> spaces >>. (letter .>> spaces) .>>. parseArg |>> f
    let parseSound name f = pstring name .>> spaces >>. letter |>> f
    let parseJump = pstring "jgz" .>> spaces >>. (parseArg .>> spaces) .>>. parseArg |>> Jump
    let parseImpl =
        parseSound "snd " Send <|>
        parseSound "rcv" Rec <|>
        parseMethod "set" Set <|>
        parseMethod "add" Add <|>
        parseMethod "mul" Mul <|>
        parseMethod "mod" Mod <|>
        parseJump
    match run parseImpl str with
    | Success (r, _, _) -> r
    | Failure (err, _, _) -> failwith err

let tryFindOrDefault (key: 'T) (map: Map<'T, int>) =
    match map |> Map.tryFind key with
    | Some x -> x
    | None -> 0

let tryRead arg regs = 
    match arg with
    | Number n -> n
    | Register a -> tryFindOrDefault a regs

let run (program: string list): int = 
    let instructions = program |> List.map parse
    let rec runImpl (prog: Instruction list) (idx: int) (frq: int) (regs: Map<char, int>) =
        match List.tryItem idx prog with
        | None -> frq
        | Some instr -> 
            match instr with
            | Send (reg) -> 
                let q = tryFindOrDefault reg regs
                runImpl prog (idx + 1) q regs
            | Rec (reg) ->
                match tryFindOrDefault reg regs with
                | 0 -> runImpl prog (idx + 1) frq regs
                | x -> x
            | Set (reg, arg) -> 
                let value = tryRead arg regs
                runImpl prog (idx + 1) frq (Map.add reg value regs)
            | Add (reg, arg) -> 0
            | Mul (reg, arg) -> 0
            | Mod (reg, arg) -> 0
            | Jump (reg, arg) -> 0
    runImpl instructions 0 0 Map.empty

module Tests =

    open FsUnit
    open Xunit

    let testInput = [
        "set a 1"
        "add a 2"
        "mul a a"
        "mod a 5"
        "snd a"
        "set a 0"
        "rcv a"
        "jgz a -1"
        "set a 1"
        "jgz a -2"
    ]

    [<Fact(Skip = "Not implemented")>]
    let ``[Part 1] Test input produces value of 4`` ()=
        run testInput |> should equal 4

    [<Fact>]
    let ``Parse: set a 1 -> Set('a', Number 1)`` ()=
        parse "set a 1" |> should equal <| Set ('a', Number 1)

    [<Fact>]
    let ``Parse: add a 2 -> Add('a', Number 2)`` ()=
        parse "add a 2" |> should equal <| Add ('a', Number 2)

    [<Fact>]
    let ``Parse: mul a a -> Mul('a' Register 'a')`` ()=
        parse "mul a a" |> should equal <| Mul ('a', Register 'a')

    [<Fact>]
    let ``Parse: mod a 5 -> Mod('a', Number 5)`` ()=
        parse "mod a 5" |> should equal <| Mod ('a', Number 5)

    [<Fact>]
    let ``Parse: snd a -> Send 'a'`` ()=
        parse "snd a" |> should equal <| Send 'a'

    [<Fact>]
    let ``Parse: rcv a -> Rec 'a'`` ()=
        parse "rcv a" |> should equal <| Rec 'a'

    [<Fact>]
    let ``Parse: jgz a -2 -> Jump (Register 'a', Number -2)`` ()=
        parse "jgz a -2" |> should equal <| Jump (Register 'a', Number -2)

    [<Fact>]
    let ``Parse: jgz 1 2 -> Jump (Number 1, Number 2)`` ()=
        parse "jgz 1 2" |> should equal <| Jump (Number 1, Number 2)
