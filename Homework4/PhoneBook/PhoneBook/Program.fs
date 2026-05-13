open System
open System.IO
open Phonebook.Logic

let showMenu () =
    printfn "\n--- Phone Book ---"
    printfn "1. Exit"
    printfn "2. Add entry"
    printfn "3. Find phone by name"
    printfn "4. Find name by phone"
    printfn "5. Show all entries"
    printfn "6. Save to file"
    printfn "7. Load from file"
    printf "Choose an option: "

let rec mainLoop (db: Entries) =
    showMenu ()

    match Console.ReadLine() with
    | "1" ->
        printfn "Goodbye!"
        ()
    | "2" ->
        printf "Name: "
        let name = Console.ReadLine()
        printf "Phone: "
        let phone = Console.ReadLine()

        if not (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(phone)) then
            mainLoop (addEntry name phone db)
        else
            printfn "Name and phone cannot be empty."
            mainLoop db
    | "3" ->
        printf "Enter name: "
        let name = Console.ReadLine()

        match findPhoneByName name db with
        | Some p -> printfn $"Phone: {p}"
        | None -> printfn "Entry not found."

        mainLoop db
    | "4" ->
        printf "Enter phone: "
        let phone = Console.ReadLine()

        match findNameByPhone phone db with
        | Some n -> printfn $"Name: {n}"
        | None -> printfn "Entry not found."

        mainLoop db
    | "5" ->
        printfn "\nAll contacts:"
        getAllEntries db |> List.iter (fun (n, p) -> printfn $"{n}: {p}")
        mainLoop db
    | "6" ->
        printf "File name: "
        let path = Console.ReadLine()

        if not (String.IsNullOrWhiteSpace(path)) then
            try
                let content = getAllEntries db |> List.map (fun (n, p) -> $"{n},{p}")
                File.WriteAllLines(path, content)
                printfn "Data saved."
            with ex ->
                printfn $"Save error: {ex.Message}"
        else
            printfn "File name cannot be empty."

        mainLoop db
    | "7" ->
        printf "File name: "
        let path = Console.ReadLine()

        if not (String.IsNullOrWhiteSpace(path)) && File.Exists(path) then
            try
                let newDb =
                    File.ReadAllLines(path)
                    |> Array.choose (fun line ->
                        match line.Split([| ',' |], 2) with
                        | [| n; p |] when not (String.IsNullOrWhiteSpace(n) || String.IsNullOrWhiteSpace(p)) ->
                            Some(n.Trim(), p.Trim())
                        | _ -> None)
                    |> Array.toList
                    |> Map.ofList

                printfn "Data loaded."
                mainLoop newDb
            with ex ->
                printfn $"Load error: {ex.Message}"
                mainLoop db
        else
            printfn "File not found or name is empty."
            mainLoop db
    | _ ->
        printfn "Invalid input, please try again."
        mainLoop db

[<EntryPoint>]
let main _ =
    mainLoop Map.empty
    0
