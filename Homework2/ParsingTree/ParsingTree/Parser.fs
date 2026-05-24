// <copyright file="Parser.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module ParsingTree.Parser

open ParsingTree

let parseTree tree =
    let rec parseTreeRecursive node =
        match node with
        | Const value -> Some value
        | Binary(left, operation, right) ->
            match parseTreeRecursive left, parseTreeRecursive right with
            | Some lValue, Some rValue ->
                match operation with
                | Add -> Some(lValue + rValue)
                | Sub -> Some(lValue - rValue)
                | Mul -> Some(lValue * rValue)
                | Div -> if rValue = 0 then None else Some(lValue / rValue)
            | _ -> None

    parseTreeRecursive tree
