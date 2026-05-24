// <copyright file="RoundingWorkflowTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module RoundingWorkflow.Tests

open NUnit.Framework
open RoundingWorkflow
open Swensen.Unquote

let rounding accuracy = RoundingBuilder(accuracy)

[<Test>]
let ``Should round intermediate and final results according to accuracy`` () =
    test
        <@
            rounding 3 {
                let! a = 2.0 / 12.0
                let! b = 3.5
                return a / b
            } = 0.048
        @>

[<Test>]
let ``Should handle zero accuracy correctly`` () =
    test
        <@
            rounding 0 {
                let! x = 2.6
                let! y = 1.2
                return x + y
            } = 4.0
        @>

[<Test>]
let ``Should propagate rounding through multiple steps`` () =
    test
        <@
            rounding 1 {
                let! a = 1.55
                let! b = 1.55
                let! c = a + b
                return c
            } = 3.2
        @>
