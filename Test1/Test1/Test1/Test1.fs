// <copyright file="Test1.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace Test1

// Solutions for tasks from 1st test.
module Test1 =

    /// Finds the minimum number in a list of integers.
    let findMinNumberInList ls =
        match ls with
        | [] -> None
        | _ -> List.reduce (fun i x -> if x < i then x else i) ls |> Some

    /// Generates lines for square.
    let getSquareLines n =
        let repeat = String.replicate

        let makeRow (row: int) : string =
            match n with
            | 1 -> "*"
            | _ when row = 0 || row = n - 1 -> repeat n "*"
            | _ -> $"""*{repeat (n - 2) " "}*"""

        [ 0 .. n - 1 ] |> List.map makeRow

    // Prints a square using getSquareLines.
    let printSquare n =
        getSquareLines n |> List.iter (printfn "%s")
        
    // Hash Table implementation.
    type HashTable<'T>(capacity, hashFunc) =
       
        let mutable buckets: list<'T> array = Array.init capacity (fun _ -> [])
        
        let getBucketIndex item =
            let hash = hashFunc item
            abs hash % capacity
            
        member this.Clear() =
            buckets <- Array.init (Array.length buckets) (fun _ -> [])
            
