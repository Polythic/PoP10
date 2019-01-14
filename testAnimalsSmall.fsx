open System
open System.IO
let runSimulationTicks (T: int) (filename: string) (n: int) (e: int) (felg: int) (u: int) (fulv: int) (s: int): unit =
    let isle = animals.environment(n, e, felg, u, fulv, s)
    let mutable x = 1
    if (File.Exists(filename)) then
        printfn "A file with the name %A already exists, do you wish to override? (Y/N)" filename
        let rec getButtonPress() = 
            let mutable buttonPress = Console.ReadKey()
            let pressedChar : char = buttonPress.KeyChar
            if pressedChar <> 'y' && pressedChar <> 'n' then
                printfn "\nPlease confirm. (Y/N)"
                getButtonPress()
            else
                pressedChar
        
        let userButtonPress = getButtonPress()
        if userButtonPress = 'y' then
            printfn "\nDeleting file..."
            File.Delete(filename)
        elif userButtonPress= 'n' then
            x<-0
            printfn "\nOverride aborted."

    printfn "Creating file %A." filename
    if x = 1 then
        for i = 0 to T-1 do
            let mooseCount = isle.board.moose.Length
            let wolfCount = isle.board.wolves.Length
            let textOutput = sprintf "Wolves: %A, Moose: %A" wolfCount mooseCount
            use file = File.AppendText filename
            file.WriteLine textOutput
            isle.tick()
    else
        printfn "Exiting program."

[<EntryPoint>]
let main args =
    runSimulationTicks (int args.[0]) args.[1] (int args.[2]) (int args.[3]) (int args.[4]) (int args.[5]) (int args.[6]) (int args.[7])
    0 //Returning 0 indicates program ran without issue

//40 "test.txt" 10 6 5 6 5 5 false
