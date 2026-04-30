// <copyright file="Program.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

open System
open FParsec
open LambdaParser

let input = Console.ReadLine()

if String.IsNullOrWhiteSpace(input) then
    printfn "Input is empty."
else
    match parseString input with
    | Success(result, _, _) ->
        printfn "Successfully parsed:"
        printfn "%A" result
    | Failure(errorMsg, _, _) ->
        printfn "Parsing failed:"
        printfn "%s" errorMsg
