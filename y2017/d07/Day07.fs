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

let parseLines strs = Seq.map parseLine strs

let rootNode infos = 
    let leafProgs = 
        infos
        |> Seq.collect (fun {programs=p} -> p)
        |> Set.ofSeq
    infos
    |> Seq.filter (fun {name=n} -> Set.contains n leafProgs |> not)
    |> Seq.head

let constructTower (strs: string list): Tree =
    let infos = parseLines strs
    let rec buildTower progs rootName =
        let node = progs |> Seq.filter (fun {name=n} -> n = rootName) |> Seq.exactlyOne
        match node.programs with
        | [] -> Leaf (node.name, node.weight)
        | ps -> Node (node.name, node.weight, List.map (fun x -> buildTower progs x) ps)
    buildTower infos (rootNode infos).name

let towers (strs: string list): string =
    let infos = parseLines strs
    (rootNode infos).name

let balanceTower (strs: string list): int = failwith "Implement"