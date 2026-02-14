// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace PowerSeries

module PowerSeries =
    let getPowers n m =
        if n < 0 || m < 0 then
            []
        else
            let powerOfTwo = bigint.Pow(2, n)

            let rec calc curr i =
                if i > m then [] else curr :: calc (curr * 2I) (i + 1)

            calc powerOfTwo 0
