// <copyright file="PrimeNumbersTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module PrimeNumbers.Tests

open NUnit.Framework
open Swensen.Unquote
open PrimeNumbers.PrimeNumbers


[<Test>]
let ``first 10 primes are correct`` () =
    let expected = [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ]
    let actual = generatePrimes |> Seq.take 10 |> List.ofSeq
    test <@ actual = expected @>
