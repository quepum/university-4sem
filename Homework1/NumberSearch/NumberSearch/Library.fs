// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace NumberSearch

module NumberSearch =
    let getPosition n ls =
        let rec calc index =
            function
            | [] -> None
            | head :: tail -> if head = n then Some index else calc (index + 1) tail

        calc 0 ls
