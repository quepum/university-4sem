// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module PointFree

let mulEach x l = List.map (fun y -> y * x) l

let mulEach'1 x = List.map (fun y -> y * x)

let mulEach'2 x = List.map (fun y -> x * y)

let mulEach'3 x = List.map ((*) x)

let mulEach'4 x = (List.map << (*)) x

let mulEach'5 = List.map << (*)
