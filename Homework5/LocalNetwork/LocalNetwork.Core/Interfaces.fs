module LocalNetwork.Core.Interfaces

type IOperatingSystem =
    abstract member Name: string
    abstract member InfectionProbability: float
