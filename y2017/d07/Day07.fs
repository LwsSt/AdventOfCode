module Day07

open FParsec

type ProgramInfo = {
    name: string
    weight: int
    programs: string list
}

let construct n w ps = {name= n; weight= w; programs= ps}

type Tree =
    | Leaf of string * int
    | Node of string * int * Tree list

let parseLine (str: string): ProgramInfo = 
    let combine n w ops =
        match ops with
        | Some ps -> construct n w ps
        | None -> construct n w []
    let name = many1Chars letter
    let weight = between (pstring "(") (pstring ")") pint32 |>> int
    let programs = opt (spaces1 >>. pstring "->" >>. spaces1 >>. sepBy name (pstring "," >>. spaces))
    let parseLineImpl = pipe3 (name .>> spaces) weight programs combine
    match run parseLineImpl str with
    | Success (info, _, _) -> info
    | Failure (err, _, _) -> failwith err


let towers (str: string list): string = failwith "Implement"