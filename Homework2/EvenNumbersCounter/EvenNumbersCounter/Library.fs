// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace EvenNumbersCounter

module EvenNumbersCounter =
    let countEvenNumbersWithFilter ls =
        ls |> List.filter (fun i -> i % 2 = 0) |> List.length

    let countEvenNumbersWithMap ls =
        ls |> List.map (fun i -> if i % 2 = 0 then 1 else 0) |> List.sum

    let countEvenNumbersWithFold ls =
        List.fold (fun acc i -> if i % 2 = 0 then acc + 1 else acc) 0 ls
