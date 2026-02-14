// <copyright file="ListFlipperTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module ListFlipper.Tests

open NUnit.Framework
open Swensen.Unquote
open ListFlipper.ListFlipper

[<Test>]
let ``flip returns empty list for empty input`` () = test <@ flip [] = [] @>

[<Test>]
let ``flip returns same list for single element`` () = test <@ flip [ 1 ] = [ 1 ] @>

[<Test>]
let ``flip reverses list with multiple elements`` () =
    test <@ flip [ 1; 2; 3; 4 ] = [ 4; 3; 2; 1 ] @>

[<Test>]
let ``flip handles list with duplicates correctly`` () =
    test <@ flip [ 1; 2; 2; 3 ] = [ 3; 2; 2; 1 ] @>

[<Test>]
let ``flip works with strings`` () =
    test <@ flip [ "a"; "b"; "c" ] = [ "c"; "b"; "a" ] @>
