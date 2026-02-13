module Tests

open System
open Factorial
open Xunit
open Swensen.Unquote
open Factorial.Factorial

[<Fact>]
let ``factorial 0 = 1`` () =
    test <@ factorial 0 = Some 1I @>

[<Fact>]
let ``factorial 6 = 720`` () =
    test <@ factorial 6 = Some 720I @>

[<Fact>]
let ``factorial negative = None`` () =
    test <@ factorial -1 = None @>