// <copyright file="LazyLib.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace Lazy

open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a

type private LazyState<'a> =
    | NotCalculated of supplier: (unit -> 'a)
    | Calculated of value: 'a

/// Single-threaded implementation.
type SingleThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable state = NotCalculated supplier

    interface ILazy<'a> with
        member _.Get() =
            match state with
            | Calculated v -> v
            | NotCalculated f ->
                let res = f ()
                state <- Calculated res
                res

/// Multithreaded implementation.
type MultiThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable state = NotCalculated supplier
    let syncRoot = obj ()

    interface ILazy<'a> with
        member _.Get() =
            match state with
            | Calculated v -> v
            | NotCalculated _ ->
                lock syncRoot (fun () ->
                    match state with
                    | Calculated v -> v
                    | NotCalculated f ->
                        let res = f ()
                        state <- Calculated res
                        res)

/// Lock-free implementation.
type LockFreeLazy<'a when 'a: not struct>(supplier: unit -> 'a) =
    let mutable stateStore: obj = box (NotCalculated supplier)

    interface ILazy<'a> with
        member _.Get() =
            let capturedState = stateStore
            let currentState = capturedState :?> LazyState<'a>

            match currentState with
            | Calculated v -> v
            | NotCalculated f ->
                let res = f ()
                let newState = box (Calculated res)

                let actualOldState =
                    Interlocked.CompareExchange(&stateStore, newState, capturedState)

                if LanguagePrimitives.PhysicalEquality actualOldState capturedState then
                    res
                else
                    match actualOldState :?> LazyState<'a> with
                    | Calculated v -> v
                    | NotCalculated _ -> res

