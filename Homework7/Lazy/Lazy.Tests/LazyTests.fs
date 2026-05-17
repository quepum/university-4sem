// <copyright file="LazyTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module Lazy.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open Lazy

let implementations =
    [| TestCaseData("SingleThread", (fun (s: unit -> obj) -> SingleThreadLazy(s) :> ILazy<obj>))
       TestCaseData("MultiThread", (fun (s: unit -> obj) -> MultiThreadLazy(s) :> ILazy<obj>))
       TestCaseData("LockFree", (fun (s: unit -> obj) -> LockFreeLazy(s) :> ILazy<obj>)) |]

[<TestCaseSource(nameof implementations)>]
let ``Get returns same reference and calculates once`` (_: string, createLazy: (unit -> obj) -> ILazy<obj>) =
    let mutable callCount = 0

    let supplier =
        fun () ->
            callCount <- callCount + 1
            box (Guid.NewGuid())

    let lazyVal = createLazy supplier

    let res1 = lazyVal.Get()
    test <@ callCount = 1 @>

    let res2 = lazyVal.Get()

    test <@ callCount = 1 @>
    test <@ obj.ReferenceEquals(res1, res2) @>

[<TestCaseSource(nameof implementations)>]
let ``Concurrent Get returns consistent result`` (_: string, createLazy: (unit -> obj) -> ILazy<obj>) =
    let supplier = fun () -> box (Guid.NewGuid())
    let lazyVal = createLazy supplier

    [| 1..100 |] |> Array.Parallel.iter (fun _ -> lazyVal.Get() |> ignore)
    let stableResult = lazyVal.Get()

    let finalCheck = [| 1..100 |] |> Array.Parallel.map (fun _ -> lazyVal.Get())

    for res in finalCheck do
        test <@ obj.ReferenceEquals(res, stableResult) @>
