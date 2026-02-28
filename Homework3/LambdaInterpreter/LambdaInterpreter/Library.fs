// <copyright file="Library.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace LambdaInterpreter

module LambdaInterpreter =

    let rec private freeVars term =
        match term with
        | Variable v -> Set.singleton v
        | Application(a, b) -> Set.union (freeVars a) (freeVars b)
        | Abstraction(v, a) -> Set.remove v (freeVars a)


    let rec private rename oldName newName term =
        match term with
        | Variable x -> if x = oldName then Variable newName else term
        | Application(a, b) -> Application(rename oldName newName a, rename oldName newName b)
        | Abstraction(v, a) ->
            if v = oldName then
                Abstraction(newName, rename oldName newName a)
            else
                Abstraction(v, rename oldName newName a)

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
        | Variable v -> if v = var then value else term
        | Application(a, b) -> Application(substitute var value a, substitute var value b)
        | Abstraction(v, a) ->
            if v = var then
                term
            else
                let freeV = freeVars value

                if Set.contains v freeV then
                    let used = Set.union freeV (freeVars a)
                    let newVar = freshVar used
                    let renamedA = rename v newVar a
                    Abstraction(newVar, substitute var value renamedA)
                else
                    Abstraction(v, substitute var value a)

    let rec private reduceOnce term =
        match term with
        | Variable _ -> term
        | Abstraction(v, a) -> Abstraction(v, reduceOnce a)
        | Application(Abstraction(var, body), b) -> substitute var b body
        | Application(a, b) ->
            let reducedA = reduceOnce a

            if reducedA <> a then
                Application(reducedA, b)
            else
                let reducedB = reduceOnce b
                if reducedB <> b then Application(a, reducedB) else term

    let normalize term =
        let rec calc i term =
            if i > 100 then
                term
            else
                let result = reduceOnce term
                if result = term then term else calc (i + 1) result

        calc 0 term
