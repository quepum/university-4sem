// <copyright file="Task1Tests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace Test1

module Task1Tests =
    open NUnit.Framework
    open Swensen.Unquote

    [<Test>]
    let ``findMinNumberInList returns Some minimum from non-empty list`` () =
        test <@ Test1.findMinNumberInList [ 3; 1; 4; 1; 5; 9; 2; 6 ] = Some 1 @>

    [<Test>]
    let ``findMinNumberInList returns None for empty list`` () =
        test <@ Test1.findMinNumberInList [] = None @>

    [<Test>]
    let ``findMinNumberInList works with single element list`` () =
        test <@ Test1.findMinNumberInList [ 1 ] = Some 1 @>

    [<Test>]
    let ``findMinNumberInList works with negative numbers`` () =
        test <@ Test1.findMinNumberInList [ -1; -2; -10; -3 ] = Some -10 @>

    [<Test>]
    let ``findMinNumberInList works with all same values`` () =
        test <@ Test1.findMinNumberInList [ 1; 1; 1; 1 ] = Some 1 @>

    [<Test>]
    let ``findMinNumberInList works with sorted ascending list`` () =
        test <@ Test1.findMinNumberInList [ 1; 2; 3; 4; 5 ] = Some 1 @>

    [<Test>]
    let ``findMinNumberInList works with sorted descending list`` () =
        test <@ Test1.findMinNumberInList [ 5; 4; 3; 2; 1 ] = Some 1 @>
