// <copyright file="Expression.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace ParsingTree

type Expression =
    | Const of value: int
    | Binary of left: Expression * operation: Operations * right: Expression
