module Phonebook.Logic

open System
open System.IO

type Name = Name of string
type Phone = Phone of string
type Entries = Map<Name, Phone>

let addEntry name phone (entries: Entries) = entries.Add(name, phone)

let findPhoneByName name (entries: Entries) = entries.TryFind name

let findNameByPhone phone (entries: Entries) =
    entries |> Map.toList |> List.tryFind (snd >> (=) phone) |> Option.map fst

let getAllEntries entries = Map.toList entries

let saveToFile path entries =
    let content =
        entries |> Map.toList |> List.map (fun (Name n, Phone p) -> $"{n},{p}")

    File.WriteAllLines(path, content)

let loadFromFile (path: string) : Result<Entries, string> =
    try
        let lines = File.ReadAllLines(path)

        let parsed =
            lines
            |> Array.map _.Split(',', StringSplitOptions.TrimEntries ||| StringSplitOptions.RemoveEmptyEntries)

        let invalidLines = parsed |> Array.filter (fun parts -> parts.Length <> 2)

        if invalidLines.Length > 0 then
            Error "Unexpected file format"
        else
            let newEntries =
                parsed
                |> Array.map (fun parts -> (Name parts[0], Phone parts[1]))
                |> Map.ofArray

            Ok newEntries
    with
    | :? FileNotFoundException -> Error "File does not find"
    | ex -> Error $"Loading error: {ex.Message}"
