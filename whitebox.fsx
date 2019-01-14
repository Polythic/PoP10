open animals

let testPosition1 = (5,5)
let testPosition2 = (0,0)
let testPosition3 = (0,9)
let testPosition4 = (9,9)
let testPosition5 = (0,5)
let testPosition6 = (5,9)
let widthOfBoard = 10
let testNeighbour = [|((0, 0), 'a'); ((0, 1), 'a'); ((1, 1), 'b'); ((1, 2), 'b'); ((2, 2), 'c')|]

let mutable test2DArray = Array2D.create 10 10 ' '
test2DArray.[0,0]<-'a'
test2DArray.[0,9]<-'b'
test2DArray.[9,9]<-'c'
test2DArray.[0,5]<-'d'

printfn "Whitebox test af getNeighbourFields" 
printfn " Branch: 1a - %b" (getNeighbourFields testPosition1 widthOfBoard = [|(4, 4); (5, 4); (6, 4); (6, 5); (6, 6); (5, 6); (4, 6); (5, 4)|])
printfn " Branch: 1b - %b" (getNeighbourFields testPosition2 widthOfBoard = [|(0, 1); (1, 1); (1, 0)|])
printfn " Branch: 1c - %b" (getNeighbourFields testPosition3 widthOfBoard = [|(0, 8); (1, 8); (1, 9)|])
printfn " Branch: 1d - %b" (getNeighbourFields testPosition4 widthOfBoard = [|(8, 8); (9, 8); (8, 9)|])
printfn " Branch: 1e - %b" (getNeighbourFields testPosition5 widthOfBoard = [|(0, 4); (1, 4); (1, 5); (1, 6); (0, 6)|])
printfn " Branch: 1f - %b" (getNeighbourFields testPosition6 widthOfBoard = [|(4, 9); (4, 8); (5, 8); (6, 8); (6, 9)|])

printfn "Whitebox test af getSymbolFromPosition"
printfn " Branch: 1a - %b" (getSymbolFromPosition testPosition1 test2DArray = ' ')
printfn " Branch: 1b - %b" (getSymbolFromPosition testPosition2 test2DArray = 'a')
printfn " Branch: 1c - %b" (getSymbolFromPosition testPosition3 test2DArray = 'b')
printfn " Branch: 1d - %b" (getSymbolFromPosition testPosition4 test2DArray = 'c')
printfn " Branch: 1e - %b" (getSymbolFromPosition testPosition5 test2DArray = 'd')

printfn "Whitebox test af getNeighbourSymbols"
printfn " Branch: 1a - %b" (getNeighbourSymbols testPosition1 test2DArray widthOfBoard = [|((4, 4), ' '); ((5, 4), ' '); ((6, 4), ' '); ((6, 5), ' '); ((6, 6), ' '); ((5, 6), ' '); ((4, 6), ' '); ((5, 4), ' ')|])
printfn " Branch: 1b - %b" (getNeighbourSymbols testPosition2 test2DArray widthOfBoard = [|((0, 1), ' '); ((1, 1), ' '); ((1, 0), ' ')|])
printfn " Branch: 1c - %b" (getNeighbourSymbols testPosition3 test2DArray widthOfBoard = [|((0, 8), ' '); ((1, 8), ' '); ((1, 9), ' ')|])
printfn " Branch: 1d - %b" (getNeighbourSymbols testPosition4 test2DArray widthOfBoard = [|((8, 8), ' '); ((9, 8), ' '); ((8, 9), ' ')|])
printfn " Branch: 1e - %b" (getNeighbourSymbols testPosition5 test2DArray widthOfBoard = [|((0, 4), ' '); ((1, 4), ' '); ((1, 5), ' '); ((1, 6), ' '); ((0, 6), ' ')|])
printfn " Branch: 1f - %b" (getNeighbourSymbols testPosition6 test2DArray widthOfBoard = [|((4, 9), ' '); ((4, 8), ' '); ((5, 8), ' '); ((6, 8), ' '); ((6, 9), ' ')|])

printfn "Whitebox test af availableSymbolField"
printfn " Branch: 1a - %b" (availableSymbolField testNeighbour 'a' = Some [|((0, 0), 'a'); ((0, 1), 'a')|])
printfn " Branch: 1b - %b" (availableSymbolField testNeighbour 'b' = Some [|((1, 1), 'b'); ((1, 2), 'b')|])
printfn " Branch: 1c - %b" (availableSymbolField testNeighbour 'c' = Some [|((2, 2), 'c')|])
printfn " Branch: 1d - %b" (availableSymbolField testNeighbour 'd' = None)
