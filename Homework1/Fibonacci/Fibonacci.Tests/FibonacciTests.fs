// <copyright file="FibonacciTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module Fibonacci.Tests

open NUnit.Framework
open Program
open Swensen.Unquote

[<Test>]
let ``fibonacci of negative number returns None`` () = test <@ fibonacci -1 = None @>

[<Test>]
let ``fibonacci 0 returns Some 0`` () = test <@ fibonacci 0 = Some 0I @>

[<Test>]
let ``fibonacci 1 returns Some 1`` () = test <@ fibonacci 1 = Some 1I @>

[<Test>]
let ``fibonacci 7 returns Some 13`` () = test <@ fibonacci 7 = Some 13I @>

[<Test>]
let ``fibonacci 10 returns Some 55`` () = test <@ fibonacci 10 = Some 55I @>
