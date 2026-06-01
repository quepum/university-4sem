// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

module LambdaInterpreter.Library

open LambdaInterpreter.LambdaTerm

let rec private freeVars term =
    match term with
    | Variable v -> Set.singleton v
    | Application(left, right) -> Set.union (freeVars left) (freeVars right)
    | Abstraction(v, body) -> Set.remove v (freeVars body)

let private freshVar usedVars =
    let baseVar = "x"

    let rec calc i =
        let candidate = baseVar + string i

        if Set.contains candidate usedVars then
            calc (i + 1)
        else
            candidate

    calc 1

let rec private substitute var value term =
    match term with
    | Variable v when v = var -> value
    | Variable _ -> term
    | Application(left, right) -> Application(substitute var value left, substitute var value right)
    | Abstraction(v, _) when v = var -> term
    | Abstraction(v, body) ->
        let freeInBody = freeVars body

        if not (Set.contains var freeInBody) then
            term
        else
            let freeV = freeVars value

            if Set.contains v freeV then
                let used = Set.union freeV freeInBody
                let newVar = freshVar used
                let renamedA = substitute v (Variable newVar) body
                Abstraction(newVar, substitute var value renamedA)
            else
                Abstraction(v, substitute var value body)

let rec private reduceOnce term =
    match term with
    | Variable _ -> None
    | Abstraction(v, body) -> reduceOnce body |> Option.map (fun reducedBody -> Abstraction(v, reducedBody))
    | Application(Abstraction(var, body), right) -> Some(substitute var right body)
    | Application(left, right) ->
        match reduceOnce left with
        | Some reducedLeft -> Some(Application(reducedLeft, right))
        | None ->
            reduceOnce right
            |> Option.map (fun reducedRight -> Application(left, reducedRight))

let normalize maxSteps term =
    let rec calc i term =
        if i > maxSteps then
            None
        else
            match reduceOnce term with
            | None -> Some term
            | Some nextTerm -> calc (i + 1) nextTerm

    calc 0 term
