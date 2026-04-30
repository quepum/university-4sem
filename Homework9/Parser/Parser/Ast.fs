// <copyright file="Ast.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module Ast

type Term =
    | Var of string
    | Abs of string * Term
    | App of Term * Term

type Definition = { Name: string; Value: Term }

type Program =
    { Definitions: Definition list
      Main: Term }
