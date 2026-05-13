// <copyright file="BlockingQueueTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module BlockingQueue.Tests

open System.Threading
open System.Threading.Tasks
open NUnit.Framework
open FsUnit
open BlockingQueue

[<Test>]
let ``Basic Enqueue and Dequeue should work in FIFO order`` () =
    let q = BlockingQueue<int>()

    q.Enqueue(10)
    q.Enqueue(20)

    q.Dequeue() |> should equal 10
    q.Dequeue() |> should equal 20
    q.Count |> should equal 0

[<Test>]
let ``Dequeue should block until element is added`` () =
    let q = BlockingQueue<string>()
    let mutable result = ""

    let consumer = Task.Run(fun () -> result <- q.Dequeue())
    Thread.Sleep(200)
    q.Enqueue("hello")

    consumer.Wait(1000) |> should equal true
    result |> should equal "hello"

[<Test>]
let ``Count property should return correct size`` () =
    let q = BlockingQueue<int>()
    q.Enqueue(1)
    q.Enqueue(2)
    q.Enqueue(3)

    q.Count |> should equal 3
