open System
open Phonebook.Logic

let showMenu () =
    printfn "\n--- Phone Book ---"
    printfn "  exit"
    printfn "  add <name> <phone>"
    printfn "  find phone <name>"
    printfn "  find name <phone>"
    printfn "  show all"
    printfn "  save <filename>"
    printfn "  load <filename>"
    printfn "  help"

let rec mainLoop (db: Entries) =
    printf "\n> "
    let input = Console.ReadLine()

    if String.IsNullOrWhiteSpace(input) then
        mainLoop db
    else
        let args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.toList

        match args with
        | [ "exit" ] ->
            printfn "Goodbye!"
            ()

        | [ "add"; name; phone ] ->
            printfn $"Added: {name} -> {phone}"
            mainLoop (addEntry (Name name) (Phone phone) db)

        | [ "find"; "phone"; name ] ->
            match findPhoneByName (Name name) db with
            | Some(Phone p) -> printfn $"Phone: {p}"
            | None -> printfn "Entry not found."

            mainLoop db

        | [ "find"; "name"; phone ] ->
            match findNameByPhone (Phone phone) db with
            | Some(Name n) -> printfn $"Name: {n}"
            | None -> printfn "Entry not found."

            mainLoop db

        | [ "show"; "all" ] ->
            printfn "\nAll contacts:"
            getAllEntries db |> List.iter (fun (Name n, Phone p) -> printfn $"{n}: {p}")
            mainLoop db

        | [ "save"; path ] ->
            try
                saveToFile path db
                printfn $"Data saved to {path}."
            with ex ->
                printfn $"Save error: {ex.Message}"

            mainLoop db

        | [ "load"; path ] ->
            match loadFromFile path with
            | Ok newDb ->
                printfn $"Data loaded from {path}."
                mainLoop newDb
            | Error errorMsg ->
                printfn $"%s{errorMsg}"
                mainLoop db

        | [ "help" ] ->
            showMenu ()
            mainLoop db

        | _ ->
            printfn "Unknown command or invalid arguments. Type 'help' for usage."
            mainLoop db

mainLoop Map.empty
