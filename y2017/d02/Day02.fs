module Day02

let checksumLine (line: int[]) :int =
    let max = Array.max line
    let min = Array.min line
    max - min

let checksum1 (lines: string[]) :int =
    let tabSplit (s: string) = s.Split [| '\t' |]
    Array.sumBy (tabSplit >> (Array.map int) >> checksumLine) lines

let checksumLine2 (line: int[]) : int = 
    (Array.allPairs line line |> Array.filter (fun (a, b) -> a <> b && a % b = 0))
    |> Array.sumBy (fun (a, b) -> a / b)

let checksum2 (lines: string[]) : int =
    let tabSplit (s: string) = s.Split [| '\t' |]
    Array.sumBy (tabSplit >> (Array.map int) >> checksumLine2) lines