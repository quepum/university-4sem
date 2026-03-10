module PhoneBook.Tests

open NUnit.Framework
open Swensen.Unquote
open Phonebook.Logic

[<Test>]
let ``Adding an entry makes the database non-empty`` () =
    let db = addEntry "Ivan" "123" Map.empty
    test <@ getAllEntries db = [ ("Ivan", "123") ] @>

[<Test>]
let ``Can find phone by name`` () =
    let db = addEntry "Bob" "456" Map.empty
    test <@ findPhoneByName "Bob" db = Some "456" @>

[<Test>]
let ``Cannot find non-existent name`` () =
    let db = addEntry "Ivan" "789" Map.empty
    test <@ findPhoneByName "Alex" db = None @>

[<Test>]
let ``Can find name by phone`` () =
    let db = addEntry "Eve" "000" Map.empty
    test <@ findNameByPhone "000" db = Some "Eve" @>

[<Test>]
let ``Cannot find non-existent phone`` () =
    let db = addEntry "Frank" "111" Map.empty
    test <@ findNameByPhone "222" db = None @>

[<Test>]
let ``Adding multiple entries preserves all data`` () =
    let db = Map.empty |> addEntry "X" "111" |> addEntry "Y" "222"
    test <@ List.contains ("X", "111") (getAllEntries db) @>
    test <@ List.contains ("Y", "222") (getAllEntries db) @>
    test <@ List.length (getAllEntries db) = 2 @>

[<Test>]
let ``Adding entry with existing name overwrites it`` () =
    let db = Map.empty |> addEntry "Z" "333" |> addEntry "Z" "444"
    test <@ findPhoneByName "Z" db = Some "444" @>
