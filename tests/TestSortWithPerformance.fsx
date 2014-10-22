let linqSortWith f list =
    let source = Seq.ofList list
    System.Linq.Enumerable.OrderBy(source, System.Func<_,_>(id), ComparisonIdentity.FromFunction f)
    |> List.ofSeq

let rnd1 = new System.Random(12345)
let rnd2 = new System.Random(12345)
let rnd3 = new System.Random(12345)

let size = 10
let iterations = 100000

let list1 = List.init size (fun i -> rnd1.NextDouble())
let list2 = List.init size (fun i -> rnd2.NextDouble())
let list3 = List.init size (fun i -> rnd3.NextDouble())

if list1 <> list2 then failwith "Invalid lists"
if list1 <> list3 then failwith "Invalid lists"

let list1' = List.sortWith (fun a b -> compare b a) list1;;
let list2' = List.sortWithNew (fun a b -> compare b a) list2;;
let list3' = linqSortWith (fun a b -> compare b a) list3;;

if list1' <> list2' then failwith "Invalid sorted lists"
if list1' <> list3' then failwith "Invalid sorted lists"

printfn "Size: %d; Iterations %d" size iterations

#time "on"

printfn "internal List stable sort implementation"
for i = 1 to iterations do
    List.sortWith (fun a b -> compare b a) list1 |> ignore

;;

printfn "Array.stableSortWith implementation"
for i = 1 to iterations do
    List.sortWithNew (fun a b -> compare b a) list2 |> ignore

;;

printfn "Linq OrderBy implementation"
for i = 1 to iterations do
    linqSortWith (fun a b -> compare b a) list3 |> ignore

;;

#time "off"

