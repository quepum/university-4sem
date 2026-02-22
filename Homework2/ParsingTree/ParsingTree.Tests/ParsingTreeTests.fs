// <copyright file="ParsingTreeTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module ParsingTree.Tests

open NUnit.Framework
open ParsingTree.Parser
open Swensen.Unquote

[<Test>]
let ``literal returns its value`` () =
    let expr = Const 5
    test <@ parseTree expr = Some 5 @>

[<Test>]
let ``addition works correctly`` () =
    let expr = Binary(Const 3, Add, Const 5)
    test <@ parseTree expr = Some 8 @>

[<Test>]
let ``subtraction works correctly`` () =
    let expr = Binary(Const 10, Sub, Const 4)
    test <@ parseTree expr = Some 6 @>

[<Test>]
let ``multiplication works correctly`` () =
    let expr = Binary(Const 7, Mul, Const 6)
    test <@ parseTree expr = Some 42 @>

[<Test>]
let ``division works correctly`` () =
    let expr = Binary(Const 15, Div, Const 3)
    test <@ parseTree expr = Some 5 @>

[<Test>]
let ``division by zero returns None`` () =
    let expr = Binary(Const 5, Div, Const 0)
    test <@ parseTree expr = None @>

[<Test>]
let ``negative numbers work correctly`` () =
    let expr = Binary(Const(-10), Sub, Const(-3))
    test <@ parseTree expr = Some(-7) @>
