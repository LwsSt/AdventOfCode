module Day15

    let generate (prev: int) (factor: int): int seq = failwith "Implement"

    let judgeGenerators (initialA: int) (initialB: int): int = failwith "Implement"

    module Tests = 
        open FsUnit
        open Xunit

        [<Fact>]
        let ``Gen A: 65, Gen B: 8921 generates 588 matching pairs`` ()=
            judgeGenerators 65 8921 |> should equal 588

        [<Fact>]
        let `` Generator A produces the correct initial sequence`` ()=
            generate 65 16807 |> Seq.take 5 |> Seq.toList |> should equal [1092455; 1181022009; 245556042; 1744312007; 1352636452]

        [<Fact>]
        let `` Generator B produces the correct initial sequence`` ()=
            generate 8921 48271 |> Seq.take 5 |> Seq.toList |> should equal [430625591; 1233683848; 1431495498; 137874439; 285222916]
