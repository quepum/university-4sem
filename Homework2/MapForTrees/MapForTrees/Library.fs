// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace MapForTrees

module MapForTrees =
    let rec treeMap func =
        function
        | Empty -> Empty
        | Node(value, left, right) -> Node(func value, treeMap func left, treeMap func right)

        treeMapRecursive tree
