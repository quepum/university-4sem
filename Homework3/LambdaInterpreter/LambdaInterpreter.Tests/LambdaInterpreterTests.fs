// <copyright file="LambdaInterpreterTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module LambdaInterpreter.Tests

open NUnit.Framework
open Swensen.Unquote
open LambdaInterpreter.Library
open LambdaInterpreter.LambdaTerm

let defaultSteps = 100

[<Test>]
let ``normalize returns identity unchanged`` () =
    let term = Abstraction("x", Variable "x")
    test <@ normalize defaultSteps term = Some term @>


[<Test>]
let ``beta reduction of identity application`` () =
    let term = Application(Abstraction("x", Variable "x"), Variable "y")
    let expected = Variable "y"
    test <@ normalize defaultSteps term = Some expected @>


[<Test>]
let ``normalization handles nested abstractions`` () =
    let term =
        Application(Abstraction("f", Application(Variable "f", Variable "x")), Abstraction("y", Variable "y"))

    let expected = Variable "x"
    test <@ normalize defaultSteps term = Some expected @>


[<Test>]
let ``alpha conversion prevents variable capture`` () =
    let term =
        Application(Abstraction("x", Abstraction("y", Variable "x")), Variable "y")

    let result = normalize defaultSteps term

    match result with
    | Some(Abstraction(param, body)) ->
        test <@ body = Variable "y" @>
        test <@ param <> "y" @>
    | _ -> failwith "Expected an abstraction after normalization"


[<Test>]
let ``normalization terminates on non-normalizable term`` () =
    let omega = Abstraction("x", Application(Variable "x", Variable "x"))
    let term = Application(omega, omega)

    test <@ normalize 10 term = None @>


[<Test>]
let ``free variables are preserved`` () =
    let term = Application(Abstraction("x", Variable "x"), Variable "z")
    test <@ normalize defaultSteps term = Some(Variable "z") @>


[<Test>]
let ``already normalized term remains unchanged`` () =
    let term =
        Abstraction("f", Abstraction("x", Application(Variable "f", Variable "x")))

    test <@ normalize defaultSteps term = Some term @>
