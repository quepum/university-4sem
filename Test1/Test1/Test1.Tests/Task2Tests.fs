// <copyright file="Task2Tests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace Test1

module Task2Tests =
    open NUnit.Framework
    open Swensen.Unquote

    [<Test>]
    let ``getSquareLines n=4 produces square`` () =
        let result = Test1.getSquareLines 4
        let expected = [ "****"; "*  *"; "*  *"; "****" ]
        test <@ result = expected @>

    [<Test>]
    let ``getSquareLines n=1 produces single *`` () =
        let result = Test1.getSquareLines 1
        test <@ result = [ "*" ] @>

    [<Test>]
    let ``getSquareLines n=0 produces empty list`` () =
        let result = Test1.getSquareLines 0
        test <@ result = [] @>

    [<Test>]
    let ``getSquareLines n=3 produces correct square`` () =
        let result = Test1.getSquareLines 3
        let expected = [ "***"; "* *"; "***" ]
        test <@ result = expected @>
