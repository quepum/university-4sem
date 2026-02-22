// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace PrimeNumbers

module PrimeNumbers =
    let generatePrimes =
        let isPrime n =
            if n < 2 then
                false
            else
                let max = int (sqrt (float n))
                [ 2..max ] |> List.forall (fun i -> n % i <> 0)

        seq {
            yield 2
            yield! Seq.initInfinite ((+) 3) |> Seq.filter isPrime
        }
