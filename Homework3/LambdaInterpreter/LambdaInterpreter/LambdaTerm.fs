// <copyright file="LambdaTerm.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace LambdaInterpreter

type LambdaTerm =
    | Variable of string
    | Application of LambdaTerm * LambdaTerm
    | Abstraction of string * LambdaTerm
