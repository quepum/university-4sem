// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace MapForTrees

module MapForTrees =
    let treeMap func tree =
        let rec treeMapRecursive =
            function
            | Empty -> Empty
            | Node(value, left, right) -> Node(func value, treeMapRecursive left, treeMapRecursive right)

        treeMapRecursive tree
