module animals

type symbol = char
type position = int * int
type neighbour = position * symbol

let mSymbol : symbol = 'm'
let wSymbol : symbol = 'w'
let eSymbol : symbol = ' '
let rnd = System.Random ()

let getNeighbourFields (pos : position) (boardWidth: int) : position [] =
  let xc = snd pos
  let yc = fst pos
  if xc = 0 && yc = 0 then
    [|(xc,yc+1);(xc+1,yc+1);(xc+1,yc)|]
  elif xc = boardWidth-1 && yc = boardWidth-1 then
    [|(yc-1,xc-1);(yc,xc-1);(yc-1,xc)|]
  elif xc = boardWidth-1 && yc = 0 then
    [|(yc,xc-1);(yc+1,xc-1);(yc+1,xc)|]
  elif xc = 0 && yc = boardWidth-1 then
    [|(yc-1,xc);(yc,xc+1);(yc-1,xc+1)|]

  elif xc = 0 && yc <> 0 && yc <> boardWidth-1 then
    [|(yc-1,xc);(yc-1,xc+1);(yc,xc+1);(yc+1,xc+1);(yc+1,xc)|]
  elif xc = boardWidth-1 && yc <> 0 && yc <> boardWidth-1 then
    [|(yc-1,xc);(yc-1,xc-1);(yc,xc-1);(yc+1,xc-1);(yc+1,xc)|]
  elif yc = 0 && xc <> 0 && xc <> boardWidth-1 then
    [|(yc,xc-1);(yc+1,xc-1);(yc+1,xc);(yc+1,xc+1);(yc,xc+1)|]
  elif yc = boardWidth-1 && xc <> 0 && xc <> boardWidth-1 then
    [|(yc,xc-1);(yc-1,xc-1);(yc-1,xc);(yc-1,xc+1);(yc,xc+1)|]
  else
    [|(xc-1,yc-1);(xc-1,yc);(xc-1,yc+1);(xc,yc+1);(xc+1,yc+1);(xc+1,yc);(xc+1,yc-1);(xc-1,yc)|]
  // TODO check om pos er på en kant af board og udfør funktion af hensyn til det

let getSymbolFromPosition (pos: position) (arr: symbol [,]) : symbol =
  arr.[fst pos, snd pos]

/// Returns an array of type neighbour with information about the surrounding fields of a given position in a 2D array
let getNeighbourSymbols (pos: position) (arr: symbol [,]) (boardWidth: int): neighbour [] =
  let neighbourFields = getNeighbourFields pos boardWidth
  let symbolArray = Array.map (fun x -> getSymbolFromPosition x arr) neighbourFields
  Array.zip neighbourFields symbolArray

/// Returns an optional array of neighbours that hold the symbol (sym)
let availableSymbolField (arr: neighbour []) (sym: symbol) : neighbour [] option =
  let symbolSpaceArray = Array.filter (fun x -> snd x = sym) arr
  if symbolSpaceArray.Length = 0 then
    None
  else
    Some symbolSpaceArray
    
/// An animal is a base class. It has a position and a reproduction counter.
type animal (symb : symbol, repLen : int) =
  let mutable _reproduction = rnd.Next(1,repLen)
  let mutable _pos : position option = None
  let _symbol : symbol = symb

  member this.symbol = _symbol
  member this.position
    with get () = _pos
    and set aPos = _pos <- aPos
  member this.reproduction = _reproduction
  member this.updateReproduction () =
    _reproduction <- _reproduction - 1
  member this.resetReproduction () =
    _reproduction <- repLen
  override this.ToString () =
    string this.symbol

/// A moose is an animal
type moose (repLen : int) =
  inherit animal (mSymbol, repLen)

  member this.tick (neighbours: neighbour []) : moose option =
    let emptyFields =
      if (availableSymbolField neighbours eSymbol).IsSome then
        (availableSymbolField neighbours eSymbol).Value
      else
        [||]
      
    if this.reproduction = 0 && emptyFields.Length <> 0 then
      let newMoose = new moose(repLen)
      newMoose.position<-Some (fst (emptyFields.[rnd.Next(0,(emptyFields.Length-1))]))
      this.resetReproduction ()
      Some newMoose
    elif emptyFields.Length <> 0 then
      this.position<-Some (fst (emptyFields.[rnd.Next(0,(emptyFields.Length-1))]))
      this.updateReproduction ()
      None
    else
      None
    // Rækkefølge:
    // Først reproducer hvis relevant
    // Ellers flyt til tilfældigt felt
    
    
    // Intentionally left blank. Insert code that updates the moose's age and optionally an offspring.
/// A wolf is an animal with a hunger counter
type wolf (repLen : int, hungLen : int) =
  inherit animal (wSymbol, repLen)
  let mutable _hunger = hungLen

  member this.hunger = _hunger
  member this.updateHunger () =
    _hunger <- _hunger - 1
    if _hunger <= 0 then
      this.position <- None // Starve to death
  member this.resetHunger () =
    _hunger <- hungLen
  member this.tick (neighbours: neighbour []) : wolf option =
    let emptyFields =
      if (availableSymbolField neighbours eSymbol).IsSome then
        (availableSymbolField neighbours eSymbol).Value
      else
        [||]
    let mooseFields =
      if (availableSymbolField neighbours mSymbol).IsSome then
        (availableSymbolField neighbours mSymbol).Value
      else
        [||]
    
    if this.reproduction = 0 && emptyFields.Length <> 0 then
      let newWolf = new wolf(repLen, hungLen)
      newWolf.position<-Some (fst (emptyFields.[rnd.Next(0,(emptyFields.Length-1))]))
      newWolf.resetHunger ()
      newWolf.resetReproduction ()
      this.resetReproduction ()
      this.updateHunger ()
      Some newWolf

    elif this.reproduction = 0 && emptyFields.Length = 0 && mooseFields.Length <> 0 then
      let moosePosition = fst mooseFields.[rnd.Next(0,(mooseFields.Length-1))]

      
      None
    
    else
      None
    


    
    
    // Rækkefølge:
    // Først reproducer hvis relevant
    // Ellers spis elg hvis muligt
    // Ellers flyt
    // opdater sult og formeringstæller, hvis sult = 0 slet ulv
    
    
    // Intentionally left blank. Insert code that updates the wolf's age and optionally an offspring.


/// A board is a chess-like board implicitly representedy by its width and coordinates of the animals.
type board =
  {width : int;
   mutable moose : moose list;
   mutable wolves : wolf list;}

/// An environment is a chess-like board with all animals and implenting all rules.
type environment (boardWidth : int, NMooses : int, mooseRepLen : int, NWolves : int, wolvesRepLen : int, wolvesHungLen : int, verbose : bool) =
  let _board : board = {
    width = boardWidth;
    moose = List.init NMooses (fun i -> moose(mooseRepLen));
    wolves = List.init NWolves (fun i -> wolf(wolvesRepLen, wolvesHungLen));
  }

  /// Project the list representation of the board into a 2d array.
  let draw (b : board) : char [,] =
    let arr = Array2D.create<char> boardWidth boardWidth eSymbol
    for m in b.moose do
      Option.iter (fun p -> arr.[fst p, snd p] <- mSymbol) m.position
    for w in b.wolves do
      Option.iter (fun p -> arr.[fst p, snd p] <- wSymbol) w.position
    arr

  /// return the coordinates of any empty field on the board.
  let anyEmptyField (b : board) : position =
    let arr = draw b
    let mutable i = rnd.Next b.width
    let mutable j = rnd.Next b.width
    while arr.[i,j] <> eSymbol do
      i <- rnd.Next b.width
      j <- rnd.Next b.width
    (i,j)

  // populate the board with animals placed at random.
  do for m in _board.moose do
       m.position <- Some (anyEmptyField _board)
  do for w in _board.wolves do
       w.position <- Some (anyEmptyField _board)

  member this.size = boardWidth*boardWidth
  member this.count = _board.moose.Length + _board.wolves.Length
  member this.board = _board
  member this.array = draw _board
  member this.tick () = 
    for m in _board.moose do
      let position = m.position.Value
      let neighbours = getNeighbourSymbols position (draw _board) _board.width
      let mTick = m.tick(neighbours)
      if mTick.IsSome then
        _board.moose<- _board.moose @ [mTick.Value]


    // Intentionally left blank. Insert code that process animals here.
    // Udfør tick for moose og wolf for alle dyr - List.map eller sådan
    // 


  override this.ToString () =
    let arr = draw _board
    let mutable ret = "  "
    for j = 0 to _board.width-1 do
      ret <- ret + string (j % 10) + " "
    ret <- ret + "\n"
    for i = 0 to _board.width-1 do
      ret <- ret + string (i % 10) + " "
      for j = 0 to _board.width-1 do
        ret <- ret + string arr.[i,j] + " "
      ret <- ret + "\n"
    ret
