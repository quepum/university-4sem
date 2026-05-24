// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace NumberSearch

module NumberSearch =
    let getPosition n ls =
        let rec calc index =
            function
            | [] -> None
            | head :: _ when head = n -> Some index
            | _ :: tail -> calc (index + 1) tail

        calc 0 ls
