module PointFree.Tests


open FsCheck
open NUnit.Framework
open PointFree

[<Test>]
let ``point-free func equals original`` () =
    let property x (l: int list) =
        func'4 x l = List.map (fun y -> y * x) l
    Check.QuickThrowOnFailure property