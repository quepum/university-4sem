// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>


module PowerSeries
let getPowers n m =
    match m < 0 with
    | true -> []
    | false ->
        let powerOfTwo = bigint (2.0 ** float n)

        let rec calc curr i acc =
            match i > m with
            | true -> List.rev acc
            | false -> calc (curr * 2I) (i + 1) (curr :: acc)

        calc powerOfTwo 0 []
