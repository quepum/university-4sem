// <copyright file="PowerSeriesTests.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module PowerSeries.Tests

open NUnit.Framework
open Swensen.Unquote
open PowerSeries.PowerSeries


[<Test>]
let ``getPowers returns empty list for negative n`` () = test <@ getPowers -1 5 = [] @>

[<Test>]
let ``getPowers returns empty list for negative m`` () = test <@ getPowers 3 -2 = [] @>

[<Test>]
let ``getPowers returns empty list for both negative`` () = test <@ getPowers -1 -1 = [] @>

[<Test>]
let ``getPowers with m=0 returns single element [2^n]`` () = test <@ getPowers 3 0 = [ 8I ] @>

[<Test>]
let ``getPowers with n=0 returns powers starting from 1`` () =
    test <@ getPowers 0 3 = [ 1I; 2I; 4I; 8I ] @>

[<Test>]
let ``getPowers generates correct sequence for n=2, m=3`` () =
    test <@ getPowers 2 3 = [ 4I; 8I; 16I; 32I ] @>
