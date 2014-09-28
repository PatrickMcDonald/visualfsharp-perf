let inline collectTheGarbage() =
    System.GC.Collect()
    System.GC.WaitForPendingFinalizers()
    System.GC.Collect()
 
let time description iterations f =
    f()
    collectTheGarbage()
    let sw = System.Diagnostics.Stopwatch.StartNew()
    for i = 1 to iterations do
        f()
    collectTheGarbage()
    sw.Stop()
    printfn "%s took %O" description sw.Elapsed
 
let run iteri s =
    iteri (fun i x -> ignore (i,x)) s
 
let s = seq {1..1000000}
 
//#time "on"
 
time "New iteri" 10 (fun() -> run Seq.iteri s);;
time "Old iteri" 10 (fun() -> run Seq.iteriOld s);;
 
//#time "off"
