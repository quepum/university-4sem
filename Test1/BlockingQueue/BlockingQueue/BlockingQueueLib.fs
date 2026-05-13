// <copyright file="BlockingQueueLib.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace BlockingQueue

open System.Collections.Generic
open System.Threading

/// A blocking queue implementation.
type BlockingQueue<'T>() =
    let queue = Queue<'T>()
    let lockObj = obj ()

    /// Adds an element to the queue.
    member this.Enqueue(elem) =
        lock lockObj (fun () ->
            queue.Enqueue elem
            Monitor.Pulse lockObj)

    /// Retrieves an element from the queue. Blocks if the queue is empty.
    member this.Dequeue(?ct) =
        let ct = defaultArg ct CancellationToken.None

        lock lockObj (fun () ->
            while queue.Count = 0 do
                ct.ThrowIfCancellationRequested()

                if ct = CancellationToken.None then
                    Monitor.Wait lockObj |> ignore
                else
                    Monitor.Wait(lockObj, 100) |> ignore

            queue.Dequeue())

    /// Gets the current number of elements in the queue.
    member this.Count = lock lockObj (fun () -> queue.Count)
