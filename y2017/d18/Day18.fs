module Day18

open FParsec

type Arg = 
    | Register of char
    | Number of int64

type Instruction =
    | Send of char
    | Set of char * Arg
    | Add of char * Arg
    | Mul of char * Arg
    | Mod of char * Arg
    | Rec of char
    | Jump of Arg * Arg

let parse (str: string): Instruction =
    let parseArg = (letter |>> Register) <|> (pint64 |>> Number)
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

let tryFindOrDefault (key: 'T) (map: Map<'T, int64>) =
    match map |> Map.tryFind key with
    | Some x -> x
    | None -> 0L

let tryRead arg regs = 
    match arg with
    | Number n -> n
    | Register a -> tryFindOrDefault a regs

let doMethod reg arg registers f =
    let value = tryRead arg registers
    let register = tryFindOrDefault reg registers
    Map.add reg (f value register) registers

let run (instructions: string list): int64 = 
    let program = instructions |> List.map parse
    let rec runImpl  (idx: int) (frq: int64) (regs: Map<char, int64>) =
        match List.tryItem idx program with
        | None -> frq
        | Some instr -> 
            match instr with
            | Send (reg) -> 
                printfn "%A" regs
                let q = tryFindOrDefault reg regs
                runImpl (idx + 1) q regs
            | Rec (reg) ->
                printfn "%A" regs
                match tryFindOrDefault reg regs with
                | 0L -> runImpl (idx + 1) frq regs
                | _ -> frq
            | Set (reg, arg) -> 
                printfn "%A" regs
                let value = tryRead arg regs
                runImpl (idx + 1) frq (Map.add reg value regs)
            | Add (reg, arg) -> 
                printfn "%A" regs
                let registers = doMethod reg arg regs (+)
                runImpl (idx + 1) frq registers
            | Mul (reg, arg) -> 
                printfn "%A" regs
                let registers = doMethod reg arg regs (*)
                runImpl (idx + 1) frq registers
            | Mod (reg, arg) -> 
                printfn "%A" regs
                let registers = doMethod reg arg regs (fun value reg -> reg % value)
                runImpl (idx + 1) frq registers
            | Jump (reg, arg) -> 
                printfn "%A" regs
                match tryRead reg regs with
                | 0L -> runImpl (idx + 1) frq regs
                | _ -> runImpl (idx + (tryRead arg regs |> int)) frq regs
    runImpl 0 0L Map.empty

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

    [<Fact>]
    let ``[Part 1] Test input produces value of 4`` ()=
        run testInput |> should equal 4L

    [<Fact>]
    let ``Parse: set a 1 -> Set('a', Number 1)`` ()=
        parse "set a 1" |> should equal <| Set ('a', Number 1L)

    [<Fact>]
    let ``Parse: add a 2 -> Add('a', Number 2)`` ()=
        parse "add a 2" |> should equal <| Add ('a', Number 2L)

    [<Fact>]
    let ``Parse: mul a a -> Mul('a' Register 'a')`` ()=
        parse "mul a a" |> should equal <| Mul ('a', Register 'a')

    [<Fact>]
    let ``Parse: mod a 5 -> Mod('a', Number 5)`` ()=
        parse "mod a 5" |> should equal <| Mod ('a', Number 5L)

    [<Fact>]
    let ``Parse: snd a -> Send 'a'`` ()=
        parse "snd a" |> should equal <| Send 'a'

    [<Fact>]
    let ``Parse: rcv a -> Rec 'a'`` ()=
        parse "rcv a" |> should equal <| Rec 'a'

    [<Fact>]
    let ``Parse: jgz a -2 -> Jump (Register 'a', Number -2)`` ()=
        parse "jgz a -2" |> should equal <| Jump (Register 'a', Number -2L)

    [<Fact>]
    let ``Parse: jgz 1 2 -> Jump (Number 1, Number 2)`` ()=
        parse "jgz 1 2" |> should equal <| Jump (Number 1L, Number 2L)
