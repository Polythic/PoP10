let isle = animals.environment(10, 6, 5, 3, 5, 6, false)
printfn "%A" isle // The inital board
isle.tick () // This is a mockup method for now...
printfn "%A" isle.array // The board after a tick
printfn "\n"
printfn "%A" isle.array