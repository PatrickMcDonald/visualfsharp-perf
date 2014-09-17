let rnd1 = new System.Random(12345)
let rnd2 = new System.Random(12345)

let size = 10000
let iterations = 100

let list1 = List.init size (fun i -> rnd1.NextDouble())
let list2 = List.init size (fun i -> rnd2.NextDouble())

if list1 <> list2 then failwith "Invalid lists"

let list1' = List.sortWith (fun a b -> compare b a) list1;;
let list2' = List.sortWithNew (fun a b -> compare b a) list2;;

if list1' <> list2' then failwith "Invalid sorted lists"

#time "on"

printfn "internal List stable sort implementation"
for i = 1 to iterations do
    List.sortWith (fun a b -> compare b a) list1 |> ignore

;;

printfn "Array.stableSortWith implementation"
for i = 1 to iterations do
    List.sortWithNew (fun a b -> compare b a) list2 |> ignore

;;

#time "off"

