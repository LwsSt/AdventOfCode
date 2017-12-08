module Day08

open FParsec

type Modifier = Inc | Dec

type Condition =
    | LT | LTE
    | GT | GTE
    | EQ | NEQ

type Instruction = {
    register: string
    modifier: Modifier
    modifyAmount: int
    condition: Condition
    conditionRegister: string
    conditionAmount: int
}

let construct reg modi modiAmount condReg cond condAmount = {
    register=reg
    modifier=modi
    modifyAmount=modiAmount
    conditionRegister=condReg
    condition=cond
    conditionAmount=condAmount
    }

let evalCondition {condition=cond; conditionAmount=amount} =
    match cond with
    | LT ->  fun x -> x < amount
    | LTE -> fun x -> x <= amount
    | GT ->  fun x -> x > amount
    | GTE -> fun x -> x >= amount
    | EQ ->  fun x -> x = amount
    | NEQ -> fun x -> x <> amount

let modifier = function
    | Inc -> (+)
    | Dec -> (-)

let evalInstruction registers instr  =
    let {register=reg; modifier=modi; modifyAmount=amount; conditionRegister=conditionReg} = instr
    let registerValue = match Map.tryFind reg registers with Some x -> x | None -> 0
    let conditionValue = match Map.tryFind conditionReg registers with Some x -> x | None -> 0
    let newValue = if evalCondition instr conditionValue then modifier modi registerValue amount else registerValue
    registers |> Map.add reg newValue

let parseLine (str: string): Instruction = 
    let pipe6 p1 p2 p3 p4 p5 p6 g = 
        p1 >>= fun a ->
        p2 >>= fun b ->
        p3 >>= fun c ->
        p4 >>= fun d ->
        p5 >>= fun e -> 
        p6 >>= fun f -> preturn (g a b c d e f)
    let parseCondition =
        stringReturn "<" LT <|>
        stringReturn "<=" LTE <|>
        stringReturn ">" GT <|>
        stringReturn ">=" GTE <|>
        stringReturn "==" EQ <|>
        stringReturn "!=" NEQ
    let register = manyChars letter
    let modify = stringReturn "inc" Inc <|> stringReturn "dec" Dec
    let instructionParser = pipe6 (register .>> spaces1) (modify .>> spaces1) (pint32 .>> spaces1 .>> pstring "if" .>> spaces1 |>> int) (register .>> spaces1) (parseCondition .>> spaces1) (pint32 |>> int) construct
    match run instructionParser str with
    | Success (instr, _, _) -> instr
    | Failure (err, _, _) -> failwith err


let runRegisters (lines: string list): int =
    lines 
    |> Seq.map parseLine
    |> Seq.fold evalInstruction Map.empty
    |> Map.toSeq
    |> Seq.map snd
    |> Seq.max

module Tests =

    open FsUnit
    open Xunit

    let testInput = [
        "b inc 5 if a > 1"
        "a inc 1 if b < 5"
        "c dec -10 if a >= 1"
        "c inc -20 if c == 10"
        ]

    [<Fact>]
    let ``[Part 1] Test input returns correct result`` ()=
        runRegisters testInput |> should equal 1

    [<Fact>]
    let ``Parse line returns expected value`` ()=
        parseLine "b inc 5 if a > 1" |> should equal {
            register="b"
            modifier = Inc
            modifyAmount = 5
            condition = GT
            conditionAmount = 1
            conditionRegister = "a"
            }