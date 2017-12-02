module Day02

let checksumLine (line: int[]) :int =
    let max = Array.max line
    let min = Array.min line
    max - min

let checksum1 (lines: string[]) :int =
    let tabSplit (s: string) = s.Split [| '\t' |]
    Array.sumBy (tabSplit >> (Array.map int) >> checksumLine) lines