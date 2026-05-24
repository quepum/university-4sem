// <copyright file="PointFreeTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module PointFree.Tests

open FsCheck
open NUnit.Framework
open PointFree

[<Test>]
let ``point-free func equals original`` () =
    let property x (l: int list) = mulEach'5 x l = mulEach x l

    Check.QuickThrowOnFailure property
