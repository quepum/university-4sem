module PhoneBook.Tests

open System.IO
open NUnit.Framework
open Swensen.Unquote
open Phonebook.Logic

[<Test>]
let ``Adding an entry makes the database non-empty`` () =
    let db = addEntry (Name "Ivan") (Phone "123") Map.empty
    test <@ getAllEntries db = [ (Name "Ivan", Phone "123") ] @>

[<Test>]
let ``Can find phone by name`` () =
    let db = addEntry (Name "Bob") (Phone "456") Map.empty
    test <@ findPhoneByName (Name "Bob") db = Some(Phone "456") @>

[<Test>]
let ``Cannot find non-existent name`` () =
    let db = addEntry (Name "Ivan") (Phone "789") Map.empty
    test <@ findPhoneByName (Name "Alex") db = None @>

[<Test>]
let ``Can find name by phone`` () =
    let db = addEntry (Name "Eve") (Phone "000") Map.empty
    test <@ findNameByPhone (Phone "000") db = Some(Name "Eve") @>

[<Test>]
let ``Cannot find non-existent phone`` () =
    let db = addEntry (Name "Frank") (Phone "111") Map.empty
    test <@ findNameByPhone (Phone "222") db = None @>

[<Test>]
let ``Adding multiple entries preserves all data`` () =
    let db =
        Map.empty
        |> addEntry (Name "X") (Phone "111")
        |> addEntry (Name "Y") (Phone "222")

    let entries = getAllEntries db
    test <@ List.contains (Name "X", Phone "111") entries @>
    test <@ List.contains (Name "Y", Phone "222") entries @>
    test <@ List.length entries = 2 @>

[<Test>]
let ``Adding entry with existing name overwrites it`` () =
    let db =
        Map.empty
        |> addEntry (Name "Z") (Phone "333")
        |> addEntry (Name "Z") (Phone "444")

    test <@ findPhoneByName (Name "Z") db = Some(Phone "444") @>

[<Test>]
let ``Loading non-existent file returns Error`` () =
    let missingFile = Path.Combine(Path.GetTempPath(), "definitely_missing_file.txt")
    let result = loadFromFile missingFile

    test
        <@
            match result with
            | Error _ -> true
            | Ok _ -> false
        @>

[<Test>]
let ``Can save and successfully load a database from file`` () =
    let tempFile = Path.GetTempFileName()

    try
        let originalDb =
            Map.empty
            |> addEntry (Name "Alice") (Phone "555-1234")
            |> addEntry (Name "Bob") (Phone "555-5678")

        saveToFile tempFile originalDb
        let loadedResult = loadFromFile tempFile

        test <@ loadedResult = Ok originalDb @>
    finally
        if File.Exists(tempFile) then
            File.Delete(tempFile)
