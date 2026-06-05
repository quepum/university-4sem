module LocalNetwork.Domain

type OS =
    | Windows
    | Linux
    | MacOS

let getInfectionProbability =
    function
    | Windows -> 0.75
    | Linux -> 0.20
    | MacOS -> 0.10

type IRandomizer =
    abstract member NextDouble: unit -> float
