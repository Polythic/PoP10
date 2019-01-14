open animals

let testPosition1 = (5,5)
let testPosition2 = (0,0)
let testPosition3 = (0,9)
let testPosition4 = (9,9)
let testPosition5 = (0,5)
let testPosition6 = (5,9)
let widthOfBoard = 10

printfn "Whitebox test af getNeighbourFields" 
printfn " Branch: 1a - %A" (getNeighbourFields testPosition1 widthOfBoard = [|(4, 4); (5, 4); (6, 4); (6, 5); (6, 6); (5, 6); (4, 6); (5, 4)|])
printfn " Branch: 1b - %A" (getNeighbourFields testPosition2 widthOfBoard = [|(0, 1); (1, 1); (1, 0)|])
printfn " Branch: 1c - %A" (getNeighbourFields testPosition3 widthOfBoard = [|(0, 8); (1, 8); (1, 9)|])
printfn " Branch: 1d - %A" (getNeighbourFields testPosition4 widthOfBoard = [|(8, 8); (9, 8); (8, 9)|])
printfn " Branch: 1e - %A" (getNeighbourFields testPosition5 widthOfBoard = [|(0, 4); (1, 4); (1, 5); (1, 6); (0, 6)|])
printfn " Branch: 1f - %A" (getNeighbourFields testPosition6 widthOfBoard = [|(4, 9); (4, 8); (5, 8); (6, 8); (6, 9)|])

printfn "Whitebox test af getSymbolFromPosition"
printfn " Branch: 1a - %b" (getSymbolFromPosition testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (getSymbolFromPosition testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (getSymbolFromPosition testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (getSymbolFromPosition testBoard1 Player1 7 = true)

printfn "Whitebox test af getNeighbourSymbols"
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition1 widthOfBoard = )
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition2 widthOfBoard = )
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition3 widthOfBoard = )
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition4 widthOfBoard = )
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition5 widthOfBoard = )
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition6 widthOfBoard = )

printfn "Whitebox test af availableSymbolField"
printfn " Branch: 1a - %b" (isHome testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (isHome testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (isHome testBoard1 Player1 7 = true)
printfn " Branch: 1a - %b" (isHome testBoard1 Player1 7 = true)
