module Day07

open FParsec

type ProgramInfo = {
    name: string
    weight: int
    programs: string list
}

type Tree =
    | Leaf of string * int
    | Node of string * int * Tree list

let parseLine (str: string): ProgramInfo = failwith "Implement"

let towers (str: string list): string = failwith "Implement"