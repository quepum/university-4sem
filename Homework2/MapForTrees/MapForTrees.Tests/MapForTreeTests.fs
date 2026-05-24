// <copyright file="MapForTreeTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module MapForTrees.Tests

open NUnit.Framework
open Swensen.Unquote
open MapForTrees

[<Test>]
let ``treeMap returns Empty for Empty tree`` () =
    let result = treeMap id Empty
    test <@ result = Empty @>

[<Test>]
let ``treeMap applies function to single node`` () =
    let input = Node(5, Empty, Empty)
    let result = treeMap ((*) 2) input
    let expected = Node(10, Empty, Empty)
    test <@ result = expected @>

[<Test>]
let ``treeMap with identity function returns same tree`` () =
    let input = Node("test", Empty, Node("right", Empty, Empty))

    let result = treeMap id input
    test <@ result = input @>

[<Test>]
let ``treeMap preserves tree structure with multiple nodes`` () =
    let input = Node(1, Node(2, Empty, Empty), Node(3, Empty, Empty))

    let result = treeMap string input

    let expected = Node("1", Node("2", Empty, Empty), Node("3", Empty, Empty))

    test <@ result = expected @>
