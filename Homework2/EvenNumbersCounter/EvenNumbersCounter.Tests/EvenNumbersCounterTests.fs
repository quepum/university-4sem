// <copyright file="EvenNumbersCounterTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module EvenNumbersCounter.Tests

open FsCheck
open NUnit.Framework
open Swensen.Unquote
open EvenNumbersCounter.EvenNumbersCounter


let testCases () =
    [ [], 0
      [ 1 ], 0
      [ 2 ], 1
      [ -2 .. 0 ], 2
      [ -4; -2 ], 2
      [ 1; 3; 5; 7 ], 0
      [ 1..10 ], 5 ]
    |> List.map (fun (list, res) -> TestCaseData(list, res))

[<TestCaseSource("cases")>]
let ``count using filter returns expected result`` list expected =
    test <@ countEvenNumbersWithFilter list = expected @>

[<TestCaseSource("cases")>]
let ``count using map returns expected result`` list expected =
    test <@ countEvenNumbersWithMap list = expected @>

[<TestCaseSource("cases")>]
let ``count using fold returns expected result`` list expected =
    test <@ countEvenNumbersWithFold list = expected @>


let checkFunctionEquivalence (list: int list) =
    let mapResult = countEvenNumbersWithMap list

    countEvenNumbersWithFilter list = mapResult
    && countEvenNumbersWithFold list = mapResult

[<Test>]
let ``all implementations are equivalent`` () =
    Check.QuickThrowOnFailure checkFunctionEquivalence
