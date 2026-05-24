// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace RoundingWorkflow

open Microsoft.FSharp.Core

module RoundingWorkflow =
    /// Builder for performing mathematical computations with automatic rounding
    /// at each step of the workflow.
    type RoundingBuilder(accuracy: int) =

        /// It rounds the input value to the specified accuracy before passing it
        /// to the rest of the computation.
        member this.Bind(n: float, f) = f (System.Math.Round(n, accuracy))

        /// Ensures the final result is also rounded to the specified accuracy.
        member this.Return(value: float) = System.Math.Round(value, accuracy)
