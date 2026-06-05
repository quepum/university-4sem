// <copyright file="Parser.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module LambdaParser

open FParsec
open Ast

/// Helper combinator that applies a parser and then skips any following whitespace
let (!) p = p .>> spaces

/// Forward reference for the expression parser to handle recursive grammar rules
let pExpression, pExpressionRef = createParserForwardedToRef<Term, unit> ()

/// Parses a valid identifier, ensuring it is not a reserved keyword like 'let'
let pId =
    let isIdChar c = isLetter c || isDigit c || c = '_'

    many1SatisfyL isIdChar "identifier"
    >>= fun s ->
        if s = "let" then
            fail "keyword 'let' is reserved"
        else
            preturn s

/// Parses a basic term: either a variable or a parenthesized expression
let pTerm =
    (!pId |>> Var)
    <|> (pchar '(' >>. spaces >>. pExpression .>> pchar ')' .>> spaces)

/// Parses left-associative applications of terms
let pApplication = many1 pTerm |>> List.reduce (fun acc t -> App(acc, t))

/// Parses a lambda abstraction with support for multiple shorthand parameters
let pAbs =
    let constructAbs ids body =
        List.foldBack (fun id acc -> Abs(id, acc)) ids body

    pchar '\\' >>. spaces >>. (many1 !pId) .>> pchar '.' .>> spaces .>>. pExpression
    |>> fun (ids, body) -> constructAbs ids body

// Link the forward reference to the actual expression implementation
pExpressionRef := pAbs <|> pApplication

/// Parses a 'let' definition: "let name = expression"
let pDefinition =
    pstring "let" >>. spaces >>. !pId .>> pchar '=' .>> spaces .>>. pExpression
    |>> fun (name, value) -> { Name = name; Value = value }

/// Parses the entire program consisting of multiple definitions and one final expression
let pProgram =
    spaces >>. many (followedBy (pstring "let") >>. pDefinition) .>>. pExpression
    .>> eof
    |>> fun (defs, main) -> { Definitions = defs; Main = main }

/// Executes the program parser on a provided input string
let parseString (input: string) = run pProgram input
