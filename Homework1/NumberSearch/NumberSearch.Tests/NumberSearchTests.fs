// <copyright file="NumberSearchTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module NumberSearch.Tests

open NUnit.Framework
open Swensen.Unquote
open NumberSearch.NumberSearch

[<Test>]
let ``getPosition returns None for empty list`` () = test <@ getPosition 5 [] = None @>

[<Test>]
let ``getPosition returns None when element is not found`` () =
    test <@ getPosition 5 [ 1; 2; 3 ] = None @>

[<Test>]
let ``getPosition returns Some 0 for first element`` () =
    test <@ getPosition 1 [ 1; 2; 3 ] = Some 0 @>

[<Test>]
let ``getPosition returns correct index for middle element`` () =
    test <@ getPosition 2 [ 1; 2; 3 ] = Some 1 @>

[<Test>]
let ``getPosition returns correct index for last element`` () =
    test <@ getPosition 3 [ 1; 2; 3 ] = Some 2 @>

[<Test>]
let ``getPosition returns first occurrence in list with duplicates`` () =
    test <@ getPosition 2 [ 1; 2; 3; 2; 4 ] = Some 1 @>

[<Test>]
let ``getPosition works with negative numbers`` () =
    test <@ getPosition -3 [ -1; -2; -3; -4 ] = Some 2 @>
