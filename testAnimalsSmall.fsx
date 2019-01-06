let isle = animals.environment(10, 6, 5, 3, 5, 6, false)
printfn "%A" isle
let runSimulationTicks (n: int) : unit =
    for i = 0 to n do
        isle.tick()
        printfn "%A" isle
    
runSimulationTicks 20