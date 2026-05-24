// <copyright file="Program.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

let fibonacci n =
    match n with
    | n when n < 0 -> None
    | 0 -> Some 0I
    | 1 -> Some 1I
    | _ ->
        let rec calc i a b =
            if i = n then b else calc (i + 1) b (b + a)

        Some(calc 1 0I 1I)

let n = 9

match fibonacci n with
| None -> printfn "Invalid argument"
| Some res -> printfn $"{res}"
