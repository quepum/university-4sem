module Phonebook.Logic

type Entries = Map<string, string>

let addEntry name phone (entries: Entries) = entries.Add(name, phone)

let findPhoneByName name (entries: Entries) = entries.TryFind name

let findNameByPhone phone (entries: Entries) =
    entries
    |> Map.toList
    |> List.tryFind (fun (_, p) -> p = phone)
    |> Option.map fst

let getAllEntries entries = Map.toList entries
