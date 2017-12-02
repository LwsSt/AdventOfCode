module Day02

let checksumLine (line: string[]) :int =
    let vals = Array.map int line
    let max = Array.max vals
    let min = Array.min vals
    max - min


let checksum1 (lines: string[]) :int =
    let tabSplit (s: string) = s.Split [| '\t' |]
    Array.sumBy (tabSplit >> checksumLine) lines