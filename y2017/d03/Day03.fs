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

let getCoordForPosition pos = 
    let largestSquare = oddSquares |> Seq.skipWhile (fun x -> x < pos) |> Seq.head
    let side = (sqrt (float largestSquare) |> int) - 1
    let coords = (side / 2, side / 2 * -1)
    let difference = largestSquare - pos
    coords
    |> traverseSides side difference

let memory (data :int) :int =
    getCoordForPosition data
    |> fun (x, y) -> (abs x) + (abs y)

let sum (map: Map<(int * int), int>) (x, y) =
    let square = [
            (x - 1, y + 1); (x, y + 1); (x + 1,y + 1);
            (x - 1, y);     (x, y);     (x + 1, y);
            (x - 1, y - 1); (x, y - 1); (x + 1, y - 1)
        ]
    square
    |> List.map (fun c -> Map.tryFind c map)
    |> List.choose id
    |> List.sum

let generate: int seq = 
    let rec generateImpl map idx =
        seq {
            let coords = getCoordForPosition idx
            let value = sum map coords
            yield value
            yield! generateImpl (Map.add coords value map) (idx + 1)
        }
    let initialMap = [((0, 0), 1)] |> Map.ofSeq
    generateImpl initialMap 2

let memory2 (data: int): int =
    generate
    |> Seq.skipWhile (fun x -> x < data)
    |> Seq.head
