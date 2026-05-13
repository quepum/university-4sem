// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace CalculatingStrings

module CalculatingStrings =

    /// Builder for the 'calculate' computation expression.
    /// It allows chaining operations on strings that represent integers.
    type CalculatingStringsBuilder() =

        /// Binds a string value to the next step in the workflow.
        member this.Bind(input: string, f) =
            match System.Int32.TryParse input with
            | true, n -> f n
            | false, _ -> None

        /// Wraps the final result value into the Option context.
        member this.Return(value) = Some value
