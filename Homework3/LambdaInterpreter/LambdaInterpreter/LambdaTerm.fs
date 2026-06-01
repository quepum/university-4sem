// <copyright file="LambdaTerm.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module LambdaInterpreter.LambdaTerm

type LambdaTerm =
    | Variable of string
    | Application of LambdaTerm * LambdaTerm
    | Abstraction of string * LambdaTerm
