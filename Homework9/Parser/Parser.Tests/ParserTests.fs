// <copyright file="ParserTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module Parser.Tests

open NUnit.Framework
open FsUnit
open Ast
open LambdaParser
open FParsec

let parse str =
    match parseString str with
    | Success(result, _, _) -> result
    | Failure(msg, _, _) -> failwithf "Parse failed: %s" msg

[<Test>]
let ``Basic variable and application parse correctly`` () =
    let input = "x y"

    let expected =
        { Definitions = []
          Main = App(Var "x", Var "y") }

    parse input |> should equal expected

[<Test>]
let ``Lambda with multiple arguments desugars correctly`` () =
    let input = "\\x y. x"

    let expected =
        { Definitions = []
          Main = Abs("x", Abs("y", Var "x")) }

    parse input |> should equal expected

[<Test>]
let ``Application is left-associative`` () =
    let input = "f x y"

    let expected =
        { Definitions = []
          Main = App(App(Var "f", Var "x"), Var "y") }

    parse input |> should equal expected

let ``Omega combinator parses correctly`` () =
    let input = "(\\x. x x) (\\x. x x)"
    let xBody = Abs("x", App(Var "x", Var "x"))

    let expected =
        { Definitions = []
          Main = App(xBody, xBody) }

    parse input |> should equal expected

[<Test>]
let ``Nested lambdas parse into deep AST structure`` () =
    let input = "\\x. \\y. x y"

    let expected =
        { Definitions = []
          Main = Abs("x", Abs("y", App(Var "x", Var "y"))) }

    parse input |> should equal expected

[<Test>]
let ``Parentheses override precedence`` () =
    let input = "x (y z)"

    let expected =
        { Definitions = []
          Main = App(Var "x", App(Var "y", Var "z")) }

    parse input |> should equal expected

[<Test>]
let ``Reserved keywords fail to parse as identifiers`` () =
    (fun () -> parse "let let = x" |> ignore)
    |> should throw typeof<System.Exception>
