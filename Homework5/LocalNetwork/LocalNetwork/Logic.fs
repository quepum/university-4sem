module LocalNetwork.Logic

open System.Collections.Generic
open LocalNetwork.Domain

type Computer(id: string, os: OS) =
    let mutable isInfected = false

    member this.Id = id
    member this.OS = os

    member this.IsInfected
        with get () = isInfected
        and set v = isInfected <- v

    member this.TryInfect(randomizer: IRandomizer) =
        if not isInfected then
            let probability = getInfectionProbability os

            if randomizer.NextDouble() < probability then
                isInfected <- true

type Network(computers: Computer array, adjacencyMatrix: bool[,]) =
    member this.Computers = computers

    member this.CanStateChange() =
        let mutable canChange = false

        for i in 0 .. computers.Length - 1 do
            if computers[i].IsInfected then
                for j in 0 .. computers.Length - 1 do
                    if adjacencyMatrix[i, j] && not computers[j].IsInfected then
                        canChange <- true

        canChange

    member this.Step(randomizer: IRandomizer) =
        let targets = HashSet<Computer>()

        for i in 0 .. computers.Length - 1 do
            if computers[i].IsInfected then
                for j in 0 .. computers.Length - 1 do
                    if adjacencyMatrix[i, j] && not computers[j].IsInfected then
                        targets.Add(computers[j]) |> ignore

        for target in targets do
            target.TryInfect(randomizer)

    member this.PrintState() =
        for c in computers do
            let status = if c.IsInfected then "Infected" else "Safe"
            printf $"[{c.Id}: {status}]"

        printfn ""
