
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

let big = {
    Name = "bigArray"
    Array = [| 1..50000 |]
    Test = test 0 50000 1000
}

let single = {
    Name = "singleEntryArray"
    Array = [| 1 |]
    Test = test 0 1 1000000
}

let midSize = 15

let mid = {
    Name = "midArray"
    Array = [| 1..midSize |]
    Test = test 0 midSize 1000000
}

;;

#time "on"

printfn ""
runTestNewSub big;;
runTestOldSub big;;

printfn ""
runTestNewSub single;;
runTestOldSub single;;

printfn ""
runTestNewSub mid;;
runTestOldSub mid;;

#time "off"
