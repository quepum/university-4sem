namespace Factorial

module Factorial =
    let factorial number: bigint option =
        if number < 0 then None
        else    
            let rec calc i (acc: bigint) =
                match i with
                | _ when i <= 1 -> acc
                | _ -> calc (i-1) (acc * bigint i)
            Some(calc number (bigint 1))