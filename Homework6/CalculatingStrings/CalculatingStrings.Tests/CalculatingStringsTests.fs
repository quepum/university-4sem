// <copyright file="CalculatingStringsTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module CalculatingStrings.Tests

open NUnit.Framework
open Swensen.Unquote
open CalculatingStrings

let calculate = CalculatingStringsBuilder()

[<Test>]
let ``Calculate workflow returns Some when all strings are valid integers`` () =
    test
        <@
            calculate {
                let! x = "1"
                let! y = "2"
                let z = x + y
                return z
            } = Some 3
        @>

[<Test>]
let ``Calculate workflow returns None when any string is invalid`` () =
    test
        <@
            calculate {
                let! x = "1"
                let! y = "."
                let z = x + y
                return z
            } = None
        @>

[<Test>]
let ``Calculate workflow handles empty string as error`` () =
    test
        <@
            calculate {
                let! x = ""
                return x
            } = None
        @>

[<Test>]
let ``Calculate workflow handles negative numbers correctly`` () =
    test
        <@
            calculate {
                let! x = "-5"
                let! y = "10"
                return x + y
            } = Some 5
        @>
