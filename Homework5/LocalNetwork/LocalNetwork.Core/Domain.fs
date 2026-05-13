module LocalNetwork.Core.Domain

open LocalNetwork.Core.Interfaces

type Computer(id: int, os: IOperatingSystem) =
    let mutable isInfected = false
    member val Id: int = id
    member val Os: IOperatingSystem = os
    member c.IsInfected: bool = isInfected

    member c.Infect: unit = isInfected <- true
    member c.Reset: unit = isInfected <- false
