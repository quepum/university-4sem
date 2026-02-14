// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace Factorial

module Factorial =
    let factorial number =
        if number < 0 then
            None
        else
            let rec calc i acc =
                if i <= 1 then acc else calc (i - 1) (acc * bigint i)

            Some(calc number 1I)
