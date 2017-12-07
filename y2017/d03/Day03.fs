module Day03

let oddSquares =
    Seq.initInfinite (fun x -> x * x)
    |> Seq.filter (fun x -> x % 2 <> 0)

let left (x:int, y:int) = (x - 1, y)
let right (x:int, y:int) = (x + 1, y)
let up (x:int, y:int) = (x, y + 1)
let down (x:int, y:int) = (x, y - 1)

let side n f = 
    List.replicate n f

let leftSide n = side n left
let rightSide n = side n right
let upSide n = side n up
let downSide n = side n down

let traverseSides sideLength n =
    [leftSide sideLength; upSide sideLength; rightSide sideLength; downSide (sideLength - 1)]
    |> Seq.concat
    |> Seq.take n
    |> Seq.fold (>>) id

let memory (data :int) :int =
    let largestSquare = oddSquares |> Seq.skipWhile (fun x -> x <= data) |> Seq.head
    let side = (sqrt (float largestSquare) |> int) - 1
    let coords = (side / 2, side / 2 * -1)
    let difference = largestSquare - data
    coords
    |> traverseSides side difference
    |> fun (x, y) -> (abs x) + (abs y)