// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>


module PrimeNumbers

let generatePrimes =
    let isPrime n =
        if n < 2 then
            false
        else
            let max = int (sqrt (float n))
            seq { 2..max } |> Seq.forall (fun i -> n % i <> 0)

    Seq.initInfinite ((+) 2) |> Seq.filter isPrime
