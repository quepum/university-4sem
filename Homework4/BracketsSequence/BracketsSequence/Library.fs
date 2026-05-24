namespace BracketsSequence


module BracketsSequence =

    let (|OpenBracket|_|) =
        function
        | '('
        | '['
        | '{' as c -> Some c
        | _ -> None

    let (|CloseBracket|_|) =
        function
        | ')' -> Some '('
        | ']' -> Some '['
        | '}' -> Some '{'
        | _ -> None

    let isCorrectSeq str =
        let rec check chars stack =
            match chars, stack with
            | [], [] -> true
            | [], _ -> false
            | OpenBracket ch :: tail, stack -> check tail (ch :: stack)
            | CloseBracket ch :: rest, top :: tail when top = ch -> check rest tail
            | CloseBracket _ :: _, _ -> false
            | _ :: rest, stack -> check rest stack

        check (List.ofSeq str) []
