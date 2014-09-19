
let test startIndex count iterations arr (sub: 'T[] -> int -> int -> 'T[]) =
    for i = 1 to iterations do
        sub arr startIndex count |> ignore

type ArrayParameters = {
    Name: string
    Array: int []
    Test: int[] -> (int[] -> int -> int -> int[]) -> unit
}

let runTest subName sub (ap:ArrayParameters) =
    printf "%s " ap.Name
    printfn "%s" subName
    ap.Test ap.Array sub

let runTestNewSub = runTest "new sub" Array.sub
let runTestOldSub = runTest "old sub" Array.subOld

let mega = {
    Name = "megaArray"
    Array = [| 1..1000000 |]
    Test = test 0 1000000 100
}

let big = {
    Name = "bigArray"
    Array = [| 1..50000 |]
    Test = test 0 50000 1000
}

let midSize = 64

let mid = {
    Name = "midArray"
    Array = [| 1..midSize |]
    Test = test 0 midSize 1000000
}

let midLower = {
    Name = "midArray - 1"
    Array = [| 1..midSize-1 |]
    Test = test 0 (midSize-1) 1000000
}

let single = {
    Name = "singleEntryArray"
    Array = [| 1 |]
    Test = test 0 1 1000000
}


// Warm up
printfn "Warming up..."
runTestNewSub mega;;
runTestNewSub big;;
runTestNewSub mid;;
runTestNewSub midLower;;
runTestNewSub single;;
runTestOldSub mega;;
runTestOldSub big;;
runTestOldSub mid;;
runTestOldSub midLower;;
runTestOldSub single;;

;;

#time "on"

printfn ""
runTestNewSub mega;;
runTestOldSub mega;;

printfn ""
runTestNewSub big;;
runTestOldSub big;;

printfn ""
runTestNewSub mid;;
runTestOldSub mid;;

printfn ""
runTestNewSub midLower;;
runTestOldSub midLower;;

printfn ""
runTestNewSub single;;
runTestOldSub single;;

#time "off"
