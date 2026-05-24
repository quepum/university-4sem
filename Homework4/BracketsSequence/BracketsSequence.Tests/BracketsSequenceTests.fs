module BracketSequence.Tests

open NUnit.Framework
open Swensen.Unquote
open BracketsSequence.BracketsSequence

[<Test>]
let ``Simple valid cases should return true`` () =
    test <@ isCorrectSeq "()" @>
    test <@ isCorrectSeq "[]" @>
    test <@ isCorrectSeq "{}" @>
    test <@ isCorrectSeq "()[]{}" @>
    test <@ isCorrectSeq "({[]})" @>

[<Test>]
let ``Valid sequences with text should return true`` () =
    test <@ isCorrectSeq "let x = (1 + [2 * 3])" @>
    test <@ isCorrectSeq "printfn \"Hello\"" @>

[<Test>]
let ``Invalid cases should return false`` () =
    test <@ isCorrectSeq "(" = false @>
    test <@ isCorrectSeq ")" = false @>
    test <@ isCorrectSeq "(]" = false @>
    test <@ isCorrectSeq "([)]" = false @>
