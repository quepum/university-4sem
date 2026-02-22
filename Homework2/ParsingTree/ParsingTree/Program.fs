// <copyright file="Program.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

open ParsingTree
open ParsingTree.Parser

let expression = Binary(Const 10, Sub, Binary(Const 6, Div, Const 2))

match parseTree expression with
| Some value -> printfn $"%d{value}"
| None -> printfn "Error"
