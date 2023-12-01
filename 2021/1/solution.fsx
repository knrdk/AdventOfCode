open System.IO
open System.Linq

let inputFile = "input.txt"

let readLines filePath = File.ReadAllLines(filePath) |> Seq.cast<string> |> Seq.map System.Int32.Parse |> Seq.toList
let numbers = readLines inputFile

let calculate (x : list<int>) =
    let mutable sum = 0;
    for i = 0 to x.Count() - 2 do
        let a = x[i]
        let b = x[i+1]
        if b > a then
            sum <- sum + 1
    sum         

let sliding = [for i = 0 to numbers.Count()-3 do yield numbers[i]+numbers[i+1]+numbers[i+2]]

let first = calculate numbers
let second = calculate sliding
printfn "%d" first
printfn "%d" second