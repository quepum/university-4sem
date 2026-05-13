// <copyright file="TreeStructure.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace MapForTrees

type 'a TreeStructure =
    | Empty
    | Node of value: 'a * left: 'a TreeStructure * right: 'a TreeStructure
