// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace ListFlipper

module ListFlipper =
    let flip ls =
        let rec loop acc =
            function
            | [] -> acc
            | head :: tail -> loop (head :: acc) tail

        loop [] ls
