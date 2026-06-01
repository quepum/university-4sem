open System
open LocalNetwork.Domain
open LocalNetwork.Logic

type Randomizer(seed: int) =
    let rng = Random(seed)

    interface IRandomizer with
        member this.NextDouble() = rng.NextDouble()


let currentSeed = Environment.TickCount
let randomizer = Randomizer(currentSeed)

printfn $"Staring simulation with seed: {currentSeed}\n"

let comps =
    [| Computer("PC-1", Windows)
       Computer("PC-2", Linux)
       Computer("PC-3", Windows)
       Computer("PC-4", MacOS) |]

comps[0].IsInfected <- true

let matrix =
    array2D
        [ [ false; true; false; false ]
          [ true; false; true; true ]
          [ false; true; false; false ]
          [ false; true; false; false ] ]

let network = Network(comps, matrix)

printfn "Initial state"
network.PrintState()

let mutable step = 1

while network.CanStateChange() do
    network.Step(randomizer)
    printfn $"\nStep: {step}"
    network.PrintState()
    step <- step + 1

printfn "\nEnd of simulation"
