let time description f =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    f()
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

timeSeqToList 1
timeSeqToListOld 1

//#time "on"

time "New Seq -> List" (fun() -> timeSeqToList 10 s);;
time "Old Seq -> List" (fun() -> timeSeqToListOld 10 s);;

time "New List -> List" (fun() -> timeSeqToList 10000000 l);;
time "Old List -> List" (fun() -> timeSeqToListOld 10000000 l);;

time "New Array -> List" (fun() -> timeSeqToList 10 a);;
time "Old Array -> List" (fun() -> timeSeqToListOld 10 a);;

//#time "off"
