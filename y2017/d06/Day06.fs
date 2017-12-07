module Day06

let cycle (input: int list): int list = 
    let length = List.length input
    let max = List.max input
    let maxIdx = input |> List.findIndex (fun x -> x = max)
    let inputArr = input |> List.toArray
    inputArr.[maxIdx] <- 0
    let rec cycleImpl (arr: int[]) index allocs = 
        if allocs = 0 then arr else
        arr.[index] <- arr.[index] + 1
        cycleImpl arr ((index + 1) % length) (allocs - 1)
    cycleImpl inputArr ((maxIdx + 1) % length) max
    |> List.ofArray

let escape (input: int list): int =
    let rec escapeImpl coll alloc =
        let nextCycle = cycle alloc
        match List.contains nextCycle coll with
        | true -> List.length coll
        | false -> escapeImpl (nextCycle :: coll) nextCycle
    escapeImpl [input] input

let escape2 (input: int list): int =
    let rec escapeImpl coll alloc =
        let nextCycle = cycle alloc
        match List.contains nextCycle coll with
        | true -> (nextCycle, coll)
        | false -> escapeImpl (nextCycle :: coll) nextCycle
    let (lastAlloc, _) = escapeImpl [input] input
    escapeImpl [lastAlloc] lastAlloc
    |> snd
    |> List.length

module Test = 

    open FsUnit
    open Xunit

    [<Fact>]
    let ``[Part 1] {0, 2, 7, 0} returns 5 cycles`` ()=
        escape [0; 2; 7; 0] |> should equal 5

    let ``[Part 2] {0, 2, 7, 0} returns 4 cycles`` ()=
        escape2 [0; 2; 7; 0] |> should equal 4    

    [<Fact>]
    let ``[Part 1] {0, 2, 7, 0} cycles to {2, 4, 1, 2}`` ()=
        cycle [0; 2; 7; 0] |> should equal [2; 4; 1; 2]
    
    [<Fact>]
    let ``[Part 1] {2, 4, 1, 2} cycles to {3, 1, 2, 3}`` ()=
        cycle [2; 4; 1; 2] |> should equal [3; 1; 2; 3]

    [<Fact>]
    let ``[Part 1] {3, 1, 2, 3} cycles to {0, 2, 3, 4}`` ()=
        cycle [3; 1; 2; 3] |> should equal [0; 2; 3; 4]

    [<Fact>]
    let ``[Part 1] {0, 2, 3, 4} cycles to {1, 3, 4, 1}`` ()=
        cycle [0; 2; 3; 4] |> should equal [1; 3; 4; 1]

    [<Fact>]
    let ``[Part 1] {1, 3, 4, 1} cycles to {2, 4, 1, 2}`` ()=
        cycle [1; 3; 4; 1] |> should equal [2; 4; 1; 2]
