module LocalNetwork.Tests

open LocalNetwork.Domain
open LocalNetwork.Logic
open NUnit.Framework
open Swensen.Unquote


let alwaysInfectMock =
    { new IRandomizer with
        member this.NextDouble() = 0.0 }

let neverInfectMock =
    { new IRandomizer with
        member this.NextDouble() = 1.0 }

[<Test>]
let ``Isolated computers never get infected`` () =
    let comps =
        [| Computer("Infected", Windows)
           Computer("Safe", Windows)
           Computer("Isolated", Windows) |]

    comps[0].IsInfected <- true

    let matrix =
        array2D [ [ false; true; false ]; [ true; false; false ]; [ false; false; false ] ]

    let network = Network(comps, matrix)

    network.Step(alwaysInfectMock)

    test <@ comps[1].IsInfected = true @>
    test <@ comps[2].IsInfected = false @>

[<Test>]
let ``With probability 1, virus acts like BFS`` () =
    let comps =
        [| Computer("A", Windows); Computer("B", Windows); Computer("C", Windows) |]

    comps[0].IsInfected <- true

    let matrix =
        array2D [ [ false; true; false ]; [ true; false; true ]; [ false; true; false ] ]

    let network = Network(comps, matrix)

    network.Step(alwaysInfectMock)
    test <@ comps[1].IsInfected = true @>
    test <@ comps[2].IsInfected = false @>

    network.Step(alwaysInfectMock)
    test <@ comps[2].IsInfected = true @>

[<Test>]
let ``Target is not infected multiple times in one step if connected to multiple infected nodes`` () =
    let comps =
        [| Computer("Infected1", Windows)
           Computer("Infected2", Windows)
           Computer("Target", Windows) |]

    comps[0].IsInfected <- true
    comps[1].IsInfected <- true

    let matrix =
        array2D [ [ false; false; true ]; [ false; false; true ]; [ true; true; false ] ]

    let network = Network(comps, matrix)

    network.Step(alwaysInfectMock)

    test <@ comps[2].IsInfected = true @>
    test <@ network.CanStateChange() = false @>


[<Test>]
let ``With probability 0, nobody gets infected`` () =
    let comps = [| Computer("A", Windows); Computer("B", Windows) |]
    comps[0].IsInfected <- true

    let matrix = array2D [ [ false; true ]; [ true; false ] ]

    let network = Network(comps, matrix)

    network.Step(neverInfectMock)

    test <@ comps[1].IsInfected = false @>
