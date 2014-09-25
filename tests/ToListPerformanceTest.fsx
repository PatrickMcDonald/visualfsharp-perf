let inline collectTheGarbage() =
    System.GC.Collect()
    System.GC.WaitForPendingFinalizers()
    System.GC.Collect()

let time description f =
    collectTheGarbage()
    let sw = System.Diagnostics.Stopwatch.StartNew()
    f()
    collectTheGarbage()
    sw.Stop()
    printfn "%s took %O" description sw.Elapsed

let s = seq {1..1000000}
let l = [1..1000000]
let a = [|1..1000000|]

let timeSeqToList iterations s =
    for i = 1 to iterations do
        Seq.toList s |> ignore

let timeSeqToListOld iterations s =
    for i = 1 to iterations do
        Seq.toListOld s |> ignore

timeSeqToList 1 s
timeSeqToListOld 1 s
timeSeqToList 1 l
timeSeqToListOld 1 l
timeSeqToList 1 a
timeSeqToListOld 1 a

//#time "on"

time "New Seq -> List" (fun() -> timeSeqToList 10 s);;
time "Old Seq -> List" (fun() -> timeSeqToListOld 10 s);;

time "New List -> List" (fun() -> timeSeqToList 10000000 l);;
time "Old List -> List" (fun() -> timeSeqToListOld 10000000 l);;

time "New Array -> List" (fun() -> timeSeqToList 10 a);;
time "Old Array -> List" (fun() -> timeSeqToListOld 10 a);;

//#time "off"
